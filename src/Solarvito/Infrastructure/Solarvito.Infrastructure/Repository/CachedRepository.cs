using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Solarvito.Contracts.User;
using Solarvito.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Solarvito.Infrastructure.Repository
{
    public class CachedRepository<TEntity> : ICachedRepository<TEntity> where TEntity : class
    {
        private readonly IDistributedCache _distributedCache;

        public CachedRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<TEntity> GetById(string id)
        {
            var cache = await _distributedCache.GetStringAsync(id);

            if (!string.IsNullOrEmpty(cache))
            {
                var cachedEntity = JsonConvert.DeserializeObject<TEntity>(cache);
                return cachedEntity;
            }

            return null;
        }

        public async Task SetWithId(string id, TEntity entity)
        {
            var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(7));

            await _distributedCache.SetStringAsync(id, JsonConvert.SerializeObject(entity), options);
        }
    }
}
