using Microsoft.EntityFrameworkCore;
using Solarvito.AppServices.AdvertisementImage.Repositories;
using Solarvito.Contracts.Advertisement;
using Solarvito.Infrastructure.ObjectStorage;
using Solarvito.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.DataAccess.EntityConfigurations.AdvertisementImage
{
    /// <inheritdoc/>
    public class AdvertisementImageRepository : IAdvertisementImageRepository
    {
        private readonly IRepository<Domain.AdvertisementImage> _repository;

        /// <summary>
        /// Инициализировать экземпляр <see cref="AdvertisementRepository"/>.
        /// </summary>
        /// <param name="repository">Базовый репозиторий.</param>
        public AdvertisementImageRepository(IRepository<Domain.AdvertisementImage> repository)
        {
            _repository = repository;
        }

        /// <inheritdoc/>
        public async Task<int> AddAsync(Domain.AdvertisementImage advertisementImage, CancellationToken cancellation)
        {
            await _repository.AddAsync(advertisementImage);

            return advertisementImage.Id;
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id, CancellationToken cancellation)
        {
            var advertisementImage = await _repository.GetByIdAsync(id);
            if (advertisementImage == null)
            {
                throw new Exception($"Не найдено обьявление с идентификатором '{id}'");
            }

            await _repository.DeleteAsync(advertisementImage);
        }


        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<Domain.AdvertisementImage>> GetAllByAdvertisementId(int advertisementId, CancellationToken cancellation)
        {
            return await _repository.GetAll().Where(ai => ai.AdvertisementId == advertisementId).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Domain.AdvertisementImage> GetByIdAsync(int id, CancellationToken cancellation)
        {
            return await _repository.GetByIdAsync(id);
        }

    }
}

