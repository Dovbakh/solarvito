using Microsoft.EntityFrameworkCore;
using Solarvito.AppServices.User.Repositories;
using Solarvito.Contracts.User;
using Solarvito.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.DataAccess.EntityConfigurations.User
{
    /// <inheritdoc />
    public class UserRepository : IUserRepository
    {
        private readonly IRepository<Domain.User> _repository;

        /// <summary>
        /// Инициализирует экземпляр <see cref="UserRepository"/>.
        /// </summary>
        /// <param name="repository">Базовый репозиторий.</param>
        public UserRepository(IRepository<Domain.User> repository)
        {
            _repository = repository;
        }

        public async Task<Domain.User> FindWhere(Expression<Func<Domain.User, bool>> predicate, CancellationToken cancellationToken)
        {
            var data = _repository.GetAllFiltered(predicate);

            return await data.Where(predicate).FirstOrDefaultAsync(cancellationToken);
        }

        public Task AddAsync(Domain.User model)
        {
            return _repository.AddAsync(model);
        }

        public async Task<Domain.User> FindById(int id, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
