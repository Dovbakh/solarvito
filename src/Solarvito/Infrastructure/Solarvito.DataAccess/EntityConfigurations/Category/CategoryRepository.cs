using Solarvito.AppServices.Category.Repositories;
using Solarvito.Contracts.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.DataAccess.EntityConfigurations.Advertisement
{
    internal class CategoryRepository : ICategoryRepository
    {
        public Task<IReadOnlyCollection<CategoryDto>> GetAll(CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
