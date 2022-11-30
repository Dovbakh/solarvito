using Solarvito.AppServices.User.Repositories;
using Solarvito.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.DataAccess.EntityConfigurations.Advertisement
{
    internal class UserRepository : IUserRepository
    {
        public Task<IReadOnlyCollection<UserDto>> GetAll(int take, int skip, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
