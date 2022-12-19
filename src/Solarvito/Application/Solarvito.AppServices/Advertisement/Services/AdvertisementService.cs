using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Minio.DataModel;
using SixLabors.ImageSharp.Advanced;
using Solarvito.AppServices.Advertisement.Repositories;
using Solarvito.AppServices.AdvertisementImage.Repositories;
using Solarvito.AppServices.File.Services;
using Solarvito.AppServices.User.Services;
using Solarvito.Contracts;
using Solarvito.Contracts.Advertisement;
using Solarvito.Contracts.AdvertisementImage;
using Solarvito.Contracts.User;
using Solarvito.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.AppServices.Advertisement.Services
{
    /// <inheritdoc/>
    public class AdvertisementService : IAdvertisementService
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IUserService _userService;
        private readonly IValidator<AdvertisementRequestDto> _validator;
        private readonly IValidator<AdvertisementUpdateRequestDto> _validatorUpdate;
        private readonly IFileService _fileService;
        private readonly IAdvertisementImageRepository _advertisementImageRepository;
        private readonly ILogger<AdvertisementService> _logger;
        private const int numByPage = 20; // количество обьявлений на одной странице

        /// <summary>
        /// Инициализировать экземпляр <see cref="AdvertisementService"/>
        /// </summary>
        /// <param name="advertisementRepository">Репозиторий для работы с <see cref="AdvertisementRequestDto"/> и <see cref="AdvertisementResponseDto"/></param>
        public AdvertisementService(
            IAdvertisementRepository advertisementRepository,
            IUserService userService,
            IValidator<AdvertisementRequestDto> validator,
            IValidator<AdvertisementUpdateRequestDto> validatorUpdate,
            IFileService fileService,
            IAdvertisementImageRepository advertisementImageRepository,
            ILogger<AdvertisementService> logger)
        {
            _advertisementRepository = advertisementRepository;
            _userService = userService;
            _validator = validator;
            _validatorUpdate = validatorUpdate;
            _fileService = fileService;
            _advertisementImageRepository = advertisementImageRepository;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<int> AddAsync(AdvertisementRequestDto advertisementRequestDto, CancellationToken cancellation)
        {
            // Валидация полученных данных
            _logger.LogInformation("Валидация полученных данных из фильтра {FilterType}.", advertisementRequestDto.ToString());

            var validationResult = _validator.Validate(advertisementRequestDto);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Полученные данные не прошли валидацию со следующими ошибками: {ErrorMessage}", string.Join("~", validationResult.Errors.ToList()));
                throw new ArgumentException(validationResult.ToString("~"));
            }


            // Обновить информацию о пользователе новыми данными из обьявления            
            var currentUser = await _userService.GetCurrent(cancellation);            
            currentUser.Address = advertisementRequestDto.Address;
            currentUser.Phone = advertisementRequestDto.Phone;
            currentUser.Name = advertisementRequestDto.UserName;

            var updatedUser = new UserUpdateRequestDto()
            {
                Address = currentUser.Address,
                Phone = currentUser.Phone,
                Name = currentUser.Name
            };
           
            await _userService.UpdateAsync(currentUser.Id, updatedUser, cancellation);


            // Добавить обьявление в БД           
            var advertisementDto = advertisementRequestDto.MapToDto();
            advertisementDto.CreatedAt = DateTime.UtcNow;
            advertisementDto.ExpireAt = DateTime.UtcNow.AddDays(30);
            advertisementDto.NumberOfViews = 0;
            advertisementDto.UserId = currentUser.Id;
                    
            var advertisementId = await _advertisementRepository.AddAsync(advertisementDto, cancellation);


            // Добавить все картинки в обьектное хранилище и их названия в БД
            foreach (IFormFile file in advertisementRequestDto.Images)
            {
                var imageName = await _fileService.UploadImage(file, cancellation);

                var advertisementImage = new AdvertisementImageDto()
                {
                    FileName = imageName,
                    AdvertisementId = advertisementId
                };
                await _advertisementImageRepository.AddAsync(advertisementImage, cancellation);
            }

            return advertisementId;
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id, CancellationToken cancellation)
        {
            var currentUser = await _userService.GetCurrent(cancellation);
            var advertisement = await _advertisementRepository.GetByIdAsync(id, cancellation);

            if (advertisement.UserId == currentUser.Id || currentUser.RoleId != 1)
            {
                await _advertisementRepository.DeleteAsync(id, cancellation);
            }
        }

        /// <inheritdoc/>
        public Task<IReadOnlyCollection<AdvertisementResponseDto>> GetAllAsync(CancellationToken cancellation, int? page)
        {
            int take = numByPage;
            int skip = page.GetValueOrDefault() * take - take;

            return _advertisementRepository.GetAllAsync(take, skip, cancellation);
        }

        /// <inheritdoc/>
        public Task<IReadOnlyCollection<AdvertisementResponseDto>> GetAllFilteredAsync(AdvertisementFilterRequest filter, CancellationToken cancellation, int? page)
        {
            int take = numByPage;
            int skip = page.GetValueOrDefault() * take - take;

            return _advertisementRepository.GetAllFilteredAsync(filter, take, skip, cancellation);
        }

        /// <inheritdoc/>
        public async Task<AdvertisementResponseDto> GetByIdAsync(int id, CancellationToken cancellation)
        {
            var advertisementResponseDto = await _advertisementRepository.GetByIdAsync(id, cancellation);

            _logger.LogInformation("Изменение количества просмотров обьявления с идентификатором {AdvertisementId}.", id);
            var currentUser = await _userService.GetCurrent(cancellation);

            if (advertisementResponseDto.UserId != currentUser.Id)
            {
                advertisementResponseDto.NumberOfViews += 1;
            }

            var advertisementDto = advertisementResponseDto.MapToDto();
            await _advertisementRepository.UpdateAsync(id, advertisementDto, cancellation);

            return advertisementResponseDto;
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(int id, AdvertisementUpdateRequestDto advertisementUpdateRequestDto, CancellationToken cancellation)
        {

            var validationResult = _validatorUpdate.Validate(advertisementUpdateRequestDto);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Полученные данные не прошли валидацию со следующими ошибками: {ErrorMessage}", validationResult.Errors.ToList().ToString());
                throw new ArgumentException(validationResult.ToString("~"));
            }

            var existingImages = await _advertisementImageRepository.GetAllByAdvertisementId(id, cancellation);

            // добавить новые изображения
            if (advertisementUpdateRequestDto.Images != null)
            {
                foreach (var image in advertisementUpdateRequestDto.Images)
                {
                    var imageName = await _fileService.UploadImage(image, cancellation);

                    var advertisementImageDto = new AdvertisementImageDto() { 
                        AdvertisementId = id, 
                        FileName = Guid.NewGuid().ToString() 
                    };
                    await _advertisementImageRepository.AddAsync(advertisementImageDto, cancellation);
                }
            }

            // удалить лишние изображения
            
           
                foreach (var image in existingImages)
                {
                    if (!advertisementUpdateRequestDto.ImagePathes.Contains(image.FileName))
                    {
                        await _advertisementImageRepository.DeleteAsync(image.Id, cancellation);
                        await _fileService.DeleteImage(image.FileName);
                    }
                }
           




            var advertisement = await _advertisementRepository.GetByIdAsync(id, cancellation);

            var advertisementDto = advertisementUpdateRequestDto.MapToDto(id);

            advertisementDto.CreatedAt = advertisement.CreatedAt;
            advertisementDto.ExpireAt = advertisement.ExpireAt;
            advertisementDto.NumberOfViews = advertisement.NumberOfViews;
            advertisementDto.UserId = advertisement.UserId;

            await _advertisementRepository.UpdateAsync(id, advertisementDto, cancellation);
        }

    }
}
