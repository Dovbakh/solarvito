using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.Infrastructure.Repository
{
    public interface ICachedRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetById(string id);

        Task SetWithId(string id, TEntity entity);
    }
}
