using Microsoft.EntityFrameworkCore;
using Minio.DataModel;
using Solarvito.AppServices.Advertisement.Repositories;
using Solarvito.Contracts;
using Solarvito.Contracts.Advertisement;
using Solarvito.Domain;
using Solarvito.Infrastructure.ObjectStorage;
using Solarvito.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.DataAccess.EntityConfigurations.Advertisement
{
    /// <inheritdoc/>
    public class AdvertisementRepository : IAdvertisementRepository
    {
        private readonly IRepository<Domain.Advertisement> _repository;
        private readonly IObjectStorage _objectStorage;

        /// <summary>
        /// Инициализировать экземпляр <see cref="AdvertisementRepository"/>.
        /// </summary>
        /// <param name="repository">Базовый репозиторий.</param>
        public AdvertisementRepository(IRepository<Domain.Advertisement> repository, IObjectStorage objectStorage)
        {
            _repository = repository;
            _objectStorage = objectStorage;
        }
        public async Task<byte[]> GetImage(CancellationToken cancellation)
        {
            return await _objectStorage.Create();
        }

        /// <inheritdoc/>
        public async Task<int> AddAsync(AdvertisementRequestDto advertisementRequestDto, CancellationToken cancellation)
        {
            var advertisement = advertisementRequestDto.MapToEntity();

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
        public async Task<IReadOnlyCollection<AdvertisementResponseDto>> GetAllAsync(int take, int skip, CancellationToken cancellation)
        {           

            return await _repository.GetAll()
                .Include(a => a.Category)
                .Include(a => a.User)
                .OrderByDescending(a => a.CreatedAt)
                .Select(a => a.MapToDto())
                .Skip(skip).Take(take).ToListAsync();
        }


        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<AdvertisementResponseDto>> GetAllFilteredAsync(AdvertisementFilterRequest filter, int take, int skip, CancellationToken cancellation)
        {
            var query = _repository.GetAll();

            if (filter.UserId.HasValue)
            {
                query = query.Where(a => a.UserId == filter.UserId);
            }

            if (filter.CategoryId.HasValue)
            {
                query = query.Where(a => a.CategoryId == filter.CategoryId);
            }

            if (!string.IsNullOrWhiteSpace(filter.Text))
            {
                query = query.Where(p => p.Name.ToLower().Contains(filter.Text.ToLower()) || p.Description.ToLower().Contains(filter.Text.ToLower()));
            }

            if (filter.minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= filter.minPrice);
            }

            if (filter.maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= filter.maxPrice);
            }

            if (filter.highRating.HasValue)
            {
                query = query.Where(p => p.User.Rating >= 4);
            }

            if (filter.SortBy.HasValue)
            {
                switch (filter.SortBy)
                {
                    case 1:
                        query = filter.OrderDesc == 1 ? query.OrderByDescending(p => p.CreatedAt) : query.OrderBy(p => p.CreatedAt);
                        break;
                    case 2:
                        query = filter.OrderDesc == 1 ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price);
                        break;
                    default:
                        break;
                }
            }


            return await query
                .Include(a => a.Category)
                .Include(a => a.User)
                .Select(a => a.MapToDto())
                .Skip(skip).Take(take).ToListAsync(cancellation);
        }

        /// <inheritdoc/>
        public async Task<AdvertisementResponseDto> GetByIdAsync(int id, CancellationToken cancellation)
        {
            var advertisement = await _repository.GetAllFiltered(a => a.Id.Equals(id))
                .Include(a => a.Category)
                .Include(a => a.User)
                .Select(a => a.MapToDto())
                .FirstOrDefaultAsync();

            if (advertisement == null)
            {
                throw new Exception($"Не найдено обьявление с идентификатором '{id}'");
            }

            return advertisement;
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(int id, AdvertisementRequestDto advertisementRequestDto, CancellationToken cancellation)
        {
            var advertisement = await _repository.GetByIdAsync(id);  
            if (advertisement == null)
            {
                throw new Exception($"Не найдено обьявление с идентификатором '{id}'");
            }

            advertisement.Name = advertisementRequestDto.Name;
            advertisement.Description = advertisementRequestDto.Description;
            advertisement.Price = advertisementRequestDto.Price;
            advertisement.Address = advertisementRequestDto.Address;
            advertisement.Phone = advertisementRequestDto.Phone;
            advertisement.ImagePath = advertisementRequestDto.ImagePath;
            advertisement.CategoryId = advertisementRequestDto.CategoryId;               

            await _repository.UpdateAsync(advertisement);
        }
    }
}
