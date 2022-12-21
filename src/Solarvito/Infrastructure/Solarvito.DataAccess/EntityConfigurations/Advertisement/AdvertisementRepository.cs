using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Minio.DataModel;
using Newtonsoft.Json;
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
using System.Xml.Linq;

namespace Solarvito.DataAccess.EntityConfigurations.Advertisement
{
    /// <inheritdoc/>
    public class AdvertisementRepository : IAdvertisementRepository
    {
        private readonly IRepository<Domain.Advertisement> _repository;
        private readonly IObjectStorage _objectStorage;
        private readonly ILogger<AdvertisementRepository> _logger;
        private readonly ICachedRepository<List<int>> _cachedRepository;

        private readonly string CacheKey = "AdvertisementHistoryKey_";

        /// <summary>
        /// Инициализировать экземпляр <see cref="AdvertisementRepository"/>.
        /// </summary>
        /// <param name="repository">Базовый репозиторий.</param>
        public AdvertisementRepository(
            IRepository<Domain.Advertisement> repository, 
            IObjectStorage objectStorage, 
            ILogger<AdvertisementRepository> logger,
            ICachedRepository<List<int>> cachedRepository)
        {
            _repository = repository;
            _objectStorage = objectStorage;
            _logger = logger;
            _cachedRepository = cachedRepository;
        }

        /// <inheritdoc/>
        public async Task<int> AddAsync(AdvertisementDto advertisementDto, CancellationToken cancellation)
        {           
            var advertisement = advertisementDto.MapToEntity();

            try
            {
                _logger.LogInformation("Запрос в репозиторий на добавление нового обьявления от пользователя {UserID}.", advertisementDto.UserId);
                await _repository.AddAsync(advertisement);
            }
            catch(Exception e)
            {
                _logger.LogError("Ошибка при добавлении нового обьявления от пользователя #{UserID}: {ErrorMessage}.", advertisementDto.UserId, e.Message);
                throw;
            }

            return advertisement.Id;
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("Запрос в репозиторий на удаление обьявления с ID: {AdvertisementId}.", id);

                var advertisement = await _repository.GetByIdAsync(id);
                if (advertisement == null)
                {
                    _logger.LogError("Не найдено обьявление с идентификатором: {AdvertisementId}", id);
                    throw new KeyNotFoundException($"Не найдено обьявление с идентификатором '{id}'");
                }

                await _repository.DeleteAsync(advertisement);

            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при удалении обьявления: {ErrorMessage}.", e.Message);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<AdvertisementResponseDto>> GetAllAsync(int take, int skip, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("Запрос в репозиторий на получение {take} обьявлений, пропустив {skip} обьявлений.", take, skip);

                return await _repository.GetAll()
                    .Include(a => a.Category)
                    .Include(a => a.User)
                    .Include(a => a.AdvertisementImages)
                    .OrderByDescending(a => a.CreatedAt)
                    .Select(a => a.MapToDto())
                    .Skip(skip).Take(take).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при получении всего списка обьявлений: {ErrorMessage}.", e.Message);
                throw;
            }
        }


        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<AdvertisementResponseDto>> GetAllFilteredAsync(AdvertisementFilterRequest filter, int take, int skip, CancellationToken cancellation)
        {
            var query = _repository.GetAll();
                

            if (!filter.UserId.IsNullOrEmpty())
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
                query = query.Where(p => (p.User.CommentsFor.Sum(u => u.Rating) / p.User.CommentsFor.Count) >= 4);
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

            try
            {
                _logger.LogInformation("Запрос в репозиторий на получение {take} обьявлений, пропустив {skip} обьявлений с фильтром.", take, skip);
                return await query
                    .Include(a => a.Category)
                    .Include(a => a.User)
                    .Include(a => a.User.CommentsFor)
                    .Include(a => a.AdvertisementImages)
                    .Select(a => a.MapToDto())
                    .Skip(skip).Take(take).ToListAsync(cancellation);
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при получении списка обьявлений по фильтру: {ErrorMessage}.", e.Message);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<AdvertisementResponseDto> GetByIdAsync(int id, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("Запрос в репозиторий на получение обьявления c ID: {AdvertisementId}.", id);                     

                var advertisement = await _repository.GetAllFiltered(a => a.Id.Equals(id))
                    .Include(a => a.Category)
                    .Include(a => a.User)
                    .Include(a => a.AdvertisementImages)
                    .Select(a => a.MapToDto())
                    .FirstOrDefaultAsync();

                if (advertisement == null)
                {
                    throw new KeyNotFoundException($"Не найдено обьявление с идентификатором '{id}'");
                }

                _logger.LogInformation("Запрос на кэширование обьявления c ID: {AdvertisementId} в историю просмотров.", id);

                var history = await _cachedRepository.GetById(CacheKey);

                if (history == null)
                {
                    history = new List<int>();
                }
                if (history.Count > 99)
                {
                    history.RemoveAt(0);
                }
                if (!history.Contains(id))
                {
                    history.Add(id);
                    _cachedRepository.SetWithId(CacheKey, history);
                }
                

                return advertisement;
            }
            catch(Exception e)
            {
                _logger.LogError("Ошибка при получении обьявления c ID {AdvertisementId}: {ErrorMessage}.", id, e.Message);
                throw;
            }

        }

        public async Task<IReadOnlyCollection<AdvertisementResponseDto>> GetHistoryAsync(int take, int skip, CancellationToken cancellation)
        {
            var cachedId = await _cachedRepository.GetById(CacheKey);

            var history = new List<AdvertisementResponseDto>();
            foreach(var id in cachedId)
            {
                var advertisement = await _repository.GetAllFiltered(a => a.Id.Equals(id))
                    .Include(a => a.Category)
                    .Include(a => a.User)
                    .Include(a => a.AdvertisementImages)
                    .Select(a => a.MapToDto())
                    .FirstOrDefaultAsync();

                history.Add(advertisement);
            }

            return history.Skip(skip).Take(take).ToList();
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(int id, AdvertisementDto advertisementDto, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("Запрос в репозиторий на изменение обьявления с ID: {AdvertisementId}.", id);

                var advertisement = await _repository.GetByIdAsync(id);
                if (advertisement == null)
                {
                    _logger.LogError("Не найдено обьявление с идентификатором: {AdvertisementId}.", id);
                    throw new KeyNotFoundException($"Не найдено обьявление с идентификатором '{id}'");
                }

                advertisement.Name = advertisementDto.Name;
                advertisement.Description = advertisementDto.Description;
                advertisement.Price = advertisementDto.Price;
                advertisement.Address = advertisementDto.Address;
                advertisement.Phone = advertisementDto.Phone;
                advertisement.CreatedAt = advertisementDto.CreatedAt;
                advertisement.ExpireAt = advertisementDto.ExpireAt;
                advertisement.NumberOfViews = advertisementDto.NumberOfViews;
                advertisement.CategoryId = advertisementDto.CategoryId;
                advertisement.UserId = advertisementDto.UserId;
                advertisement.UserName = advertisementDto.UserName;


                await _repository.UpdateAsync(advertisement);
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при изменении обьявления c ID {AdvertisementId}: {ErrorMessage}.", id, e.Message);
                throw;
            }
        }
    }
}
