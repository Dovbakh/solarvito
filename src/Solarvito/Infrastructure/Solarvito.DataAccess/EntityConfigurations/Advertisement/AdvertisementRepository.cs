using Solarvito.AppServices.Advertisement.Repositories;
using Solarvito.Contracts.Advertisement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.DataAccess.EntityConfigurations.Advertisement
{
    internal class AdvertisementRepository : IAdvertisementRepository
    {
        public Task<int> CreateAsync(AdvertisementDto advertisementDto, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<AdvertisementDto>> GetAll(int take, int skip, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, AdvertisementDto advertisementDto, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
