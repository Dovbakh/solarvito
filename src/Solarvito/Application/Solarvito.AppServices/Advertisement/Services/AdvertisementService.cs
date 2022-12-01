using Solarvito.AppServices.Advertisement.Repositories;
using Solarvito.Contracts.Advertisement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.AppServices.Advertisement.Services
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly IAdvertisementRepository _advertisementRepository;

        public AdvertisementService(IAdvertisementRepository repository)
        {
            _advertisementRepository = repository;
        }

        public Task<int> AddAsync(AdvertisementDto advertisementDto, CancellationToken cancellation)
        {
            return _advertisementRepository.AddAsync(advertisementDto, cancellation);
        }

        public Task DeleteAsync(int id, CancellationToken cancellation)
        {
            return _advertisementRepository.DeleteAsync(id, cancellation);
        }

        public Task<IReadOnlyCollection<AdvertisementDto>> GetAllAsync(int take, int skip, CancellationToken cancellation)
        {
            return _advertisementRepository.GetAllAsync(take, skip, cancellation);
        }

        public Task<IReadOnlyCollection<AdvertisementDto>> GetAllFilteredAsync(AdvertisementFilterRequest request, int take, int skip, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public Task<AdvertisementDto> GetByIdAsync(int id, CancellationToken cancellation)
        {
            return _advertisementRepository.GetByIdAsync(id, cancellation);
        }

        public Task UpdateAsync(int id, AdvertisementDto advertisementDto, CancellationToken cancellation)
        {
            return _advertisementRepository.UpdateAsync(id, advertisementDto, cancellation);
        }
    }
}
