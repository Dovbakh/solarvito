using FluentValidation;
using Minio.DataModel;
using Solarvito.AppServices.Advertisement.Repositories;
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

        private const int numByPage = 20;

        /// <summary>
        /// Инициализировать экземпляр <see cref="AdvertisementService"/>
        /// </summary>
        /// <param name="advertisementRepository">Репозиторий для работы с <see cref="AdvertisementDto"/></param>
        public AdvertisementService(IAdvertisementRepository advertisementRepository, IUserService userService, IValidator<AdvertisementRequestDto> validator)
        {
            _advertisementRepository = advertisementRepository;
            _userService = userService;
            _validator = validator;
        }

        public async Task<byte[]> GetImage(CancellationToken cancellation)
        {
            return await _advertisementRepository.GetImage(cancellation);
        }

        /// <inheritdoc/>
        public async Task<int> AddAsync(AdvertisementRequestDto advertisementRequestDto, CancellationToken cancellation)
        {
            var validationResult = _validator.Validate(advertisementRequestDto);
            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString("~"));
            }

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

            advertisementRequestDto.UserId = currentUser.Id;
            return await _advertisementRepository.AddAsync(advertisementRequestDto, cancellation);
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
        public Task<AdvertisementResponseDto> GetByIdAsync(int id, CancellationToken cancellation)
        {
            return _advertisementRepository.GetByIdAsync(id, cancellation);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(int id, AdvertisementRequestDto advertisementRequestDto, CancellationToken cancellation)
        {
            return _advertisementRepository.UpdateAsync(id, advertisementRequestDto, cancellation);
        }
    }
}
