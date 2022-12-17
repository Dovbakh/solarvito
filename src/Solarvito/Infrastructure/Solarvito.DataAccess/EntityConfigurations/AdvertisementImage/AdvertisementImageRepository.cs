using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp.PixelFormats;
using Solarvito.AppServices.AdvertisementImage.Repositories;
using Solarvito.Contracts;
using Solarvito.Contracts.Advertisement;
using Solarvito.Contracts.AdvertisementImage;
using Solarvito.Domain;
using Solarvito.Infrastructure.ObjectStorage;
using Solarvito.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.DataAccess.EntityConfigurations.AdvertisementImage
{
    /// <inheritdoc/>
    public class AdvertisementImageRepository : IAdvertisementImageRepository
    {
        private readonly IRepository<Domain.AdvertisementImage> _repository;
        private readonly ILogger<AdvertisementImageRepository> _logger;

        /// <summary>
        /// Инициализировать экземпляр <see cref="AdvertisementRepository"/>.
        /// </summary>
        /// <param name="repository">Базовый репозиторий.</param>
        public AdvertisementImageRepository(IRepository<Domain.AdvertisementImage> repository, ILogger<AdvertisementImageRepository> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<int> AddAsync(AdvertisementImageDto advertisementImageDto, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("Запрос в репозиторий на добавление изображения {FileName} из обьявления c ID {AdvertisementId}.", advertisementImageDto.FileName, advertisementImageDto.AdvertisementId);

                var advertisementImage = advertisementImageDto.MapToEntity();

                await _repository.AddAsync(advertisementImage);

                return advertisementImage.Id;
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при добавлении изображения {FileName} из обьявления с ID {AdvertisementId}: {ErrorMessage}", advertisementImageDto.FileName, advertisementImageDto.AdvertisementId, e.Message);
                throw;
            }

            
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("Запрос в репозиторий на удаление изображения с ID: {AdvertisementImageId}.", id);

                var advertisementImage = await _repository.GetByIdAsync(id);
                if (advertisementImage == null)
                {
                    _logger.LogError("Не найдено изображение с ID: {AdvertisementImageId}.", id);
                    throw new KeyNotFoundException($"Не найдено изображение с ID '{id}'");
                }

                await _repository.DeleteAsync(advertisementImage);
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при удалении изображения с ID {AdvertisementImageId}: {ErrorMessage}", id, e.Message);
                throw;
            }
            
        }


        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<AdvertisementImageDto>> GetAllByAdvertisementId(int advertisementId, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("Запрос в репозиторий на получение всех изображений из обьявления с ID: {AdvertisementId}.", advertisementId);

                return await _repository.GetAll()
                .Where(ai => ai.AdvertisementId == advertisementId)
                .Select(ai => ai.MapToDto()).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при получении всех изображений из обьявления с ID {AdvertisementId}: {ErrorMessage}", advertisementId, e.Message);
                throw;
            }            
        }

        /// <inheritdoc/>
        public async Task<AdvertisementImageDto> GetByIdAsync(int id, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("Запрос в репозиторий на получение изображения с ID: {AdvertisementImageId}.", id);

                var advertisementImage = await _repository.GetByIdAsync(id);

                return advertisementImage.MapToDto();
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при получении изображения с ID {AdvertisementImageId}: {ErrorMessage}", id, e.Message);
                throw;
            }
            
        }

    }
}

