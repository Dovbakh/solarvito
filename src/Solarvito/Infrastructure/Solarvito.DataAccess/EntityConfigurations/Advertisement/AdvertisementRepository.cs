﻿using Solarvito.AppServices.Advertisement.Repositories;
using Solarvito.Contracts.Advertisement;
using Solarvito.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.DataAccess.EntityConfigurations.Advertisement
{
    public class AdvertisementRepository : IAdvertisementRepository
    {
        private readonly IRepository<Domain.Advertisement> _repository;

        public AdvertisementRepository(IRepository<Domain.Advertisement> repository)
        {
            _repository = repository;
        }

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

        public Task DeleteAsync(int id, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<AdvertisementDto>> GetAllAsync(int take, int skip, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<AdvertisementDto>> GetAllFilteredAsync(AdvertisementFilterRequest request, int take, int skip, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public Task<AdvertisementDto> GetByIdAsync(int id, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, AdvertisementDto advertisementDto, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
