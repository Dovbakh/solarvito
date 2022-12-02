using Microsoft.EntityFrameworkCore;
using Solarvito.AppServices.Advertisement.Repositories;
using Solarvito.Contracts.Advertisement;
using Solarvito.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.DataAccess.EntityConfigurations.Advertisement
{
    /// <inheritdoc/>
    public class AdvertisementRepository : IAdvertisementRepository
    {
        private readonly IRepository<Domain.Advertisement> _repository;

        /// <summary>
        /// Инициализировать экземпляр <see cref="AdvertisementRepository"/>.
        /// </summary>
        /// <param name="repository">Базовый репозиторий.</param>
        public AdvertisementRepository(IRepository<Domain.Advertisement> repository)
        {
            _repository = repository;
        }

        /// <inheritdoc/>
        public async Task<int> AddAsync(AdvertisementDto advertisementDto, CancellationToken cancellation)
        {
            var advertisement = new Domain.Advertisement {
                Name = advertisementDto.Name,
                Description = advertisementDto.Description,
                UserId = advertisementDto.UserId,
                CategoryId = advertisementDto.CategoryId,
                ImagePath = advertisementDto.ImagePath,
                CreatedAt = DateTime.UtcNow,
                ExpireAt = DateTime.UtcNow.AddDays(30),
                NumberOfViews = 0
            };
            await _repository.AddAsync(advertisement);
            return advertisement.Id;
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id, CancellationToken cancellation)
        {
            var advertisement = await _repository.GetByIdAsync(id);
            if (advertisement == null)
            {
                throw new Exception($"Не найдено обьявление с идентификатором '{id}'");
            }

            await _repository.DeleteAsync(advertisement);
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<AdvertisementDto>> GetAllAsync(int take, int skip, CancellationToken cancellation)
        {
            return await _repository.GetAll().
                Select(a => new AdvertisementDto()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    UserId = a.UserId,
                    CategoryId = a.CategoryId,
                    ImagePath = a.ImagePath,
                    CreatedAt = a.CreatedAt,
                    ExpireAt = a.ExpireAt,
                    NumberOfViews = a.NumberOfViews

                }).Take(take).Skip(skip).ToListAsync();
        }

        /// <inheritdoc/>
        public Task<IReadOnlyCollection<AdvertisementDto>> GetAllFilteredAsync(AdvertisementFilterRequest request, int take, int skip, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<AdvertisementDto> GetByIdAsync(int id, CancellationToken cancellation)
        {
            var advertisement = await _repository.GetByIdAsync(id);
            if (advertisement == null)
            {
                throw new Exception($"Не найдено обьявление с идентификатором '{id}'");
            }

            var advertisementDto = new AdvertisementDto()
            {
                Id = advertisement.Id,
                Name = advertisement.Name,
                Description = advertisement.Description,
                UserId = advertisement.UserId,
                CategoryId = advertisement.CategoryId,
                ImagePath = advertisement.ImagePath,
                CreatedAt = advertisement.CreatedAt,
                ExpireAt = advertisement.ExpireAt,
                NumberOfViews = advertisement.NumberOfViews
            };

            return advertisementDto;
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(int id, AdvertisementDto advertisementDto, CancellationToken cancellation)
        {
            var advertisement = await _repository.GetByIdAsync(id);  
            if (advertisement == null)
            {
                throw new Exception($"Не найдено обьявление с идентификатором '{id}'");
            }

            advertisement.Name = advertisementDto.Name;
            advertisement.Description = advertisementDto.Description;
            advertisement.ImagePath = advertisementDto.ImagePath;
            advertisement.CategoryId = advertisementDto.CategoryId;

            await _repository.UpdateAsync(advertisement);
        }
    }
}
