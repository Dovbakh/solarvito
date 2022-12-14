using FluentValidation;
using Microsoft.AspNetCore.Http;
using Minio.DataModel;
using Solarvito.AppServices.Advertisement.Repositories;
using Solarvito.AppServices.AdvertisementImage.Repositories;
using Solarvito.AppServices.File.Services;
using Solarvito.AppServices.User.Services;
using Solarvito.Contracts.Advertisement;
using Solarvito.Contracts.User;
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
        private readonly IFileService _fileService;
        private readonly IAdvertisementImageRepository _advertisementImageRepository;
        private const int numByPage = 20; // количество обьявлений на одной странице

        /// <summary>
        /// Инициализировать экземпляр <see cref="AdvertisementService"/>
        /// </summary>
        /// <param name="advertisementRepository">Репозиторий для работы с <see cref="AdvertisementRequestDto"/> и <see cref="AdvertisementResponseDto"/></param>
        public AdvertisementService(
            IAdvertisementRepository advertisementRepository,
            IUserService userService,
            IValidator<AdvertisementRequestDto> validator,
            IFileService fileService,
            IAdvertisementImageRepository advertisementImageRepository)
        {
            _advertisementRepository = advertisementRepository;
            _userService = userService;
            _validator = validator;
            _fileService = fileService;
            _advertisementImageRepository = advertisementImageRepository;
        }

        /// <inheritdoc/>
        public async Task<int> AddAsync(AdvertisementRequestDto advertisementRequestDto, CancellationToken cancellation)
        {
            // Валидация полученных данных
            var validationResult = _validator.Validate(advertisementRequestDto);
            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString("~"));
            }


            // Обновить информацию о пользователе новыми данными из обьявления
            var currentUser = await _userService.GetCurrent(cancellation);
            currentUser.Address = advertisementRequestDto.Address;
            currentUser.Phone = advertisementRequestDto.Phone;
            currentUser.Name = advertisementRequestDto.Name;

            var updatedUser = new UserUpdateRequestDto()
            {
                Id = currentUser.Id,
                Address = currentUser.Address,
                Phone = currentUser.Phone,
                Name = currentUser.Name
            };
            await _userService.UpdateAsync(updatedUser, cancellation);


            // Добавить обьявление в БД
            advertisementRequestDto.UserId = currentUser.Id;
            var advertisementId = await _advertisementRepository.AddAsync(advertisementRequestDto, cancellation);



            // Добавить все картинки в обьектное хранилище и их названия в БД
            foreach (IFormFile file in advertisementRequestDto.Images)
            {
                var imageName = await _fileService.UploadImage(file, cancellation);

                var advertisementImage = new Domain.AdvertisementImage()
                {
                    FileName = imageName,
                    AdvertisementId = advertisementId
                };
                await _advertisementImageRepository.AddAsync(advertisementImage, cancellation);
            }

            return advertisementId;
        }

        /// <inheritdoc/>
        public Task DeleteAsync(int id, CancellationToken cancellation)
        {
            return _advertisementRepository.DeleteAsync(id, cancellation);
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
            var advertisement = await _advertisementRepository.GetByIdAsync(id, cancellation);
            if (advertisement == null)
            {
                throw new KeyNotFoundException($"Обьявление с идентификатором '{id}' не найдено.");
            }


            return advertisement;
        }

        /// <inheritdoc/>
        public Task UpdateAsync(int id, AdvertisementRequestDto advertisementRequestDto, CancellationToken cancellation)
        {
            var validationResult = _validator.Validate(advertisementRequestDto);
            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString("~"));
            }

            return _advertisementRepository.UpdateAsync(id, advertisementRequestDto, cancellation);
        }

        public Task IncreaseViewNumbers(int id, CancellationToken cancellation)
        {
            return null;
        }
    }
}
