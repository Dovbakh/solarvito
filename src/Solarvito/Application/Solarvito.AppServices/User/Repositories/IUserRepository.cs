using Solarvito.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.AppServices.User.Repositories
{
    /// <summary>
    /// Репозиторий чтения/записи для работы с пользователями.
    /// </summary>
    public interface IUserRepository
    {
        Task<Domain.User> FindWhere(Expression<Func<Domain.User, bool>> predicate, CancellationToken cancellationToken);

        Task<Domain.User> FindById(int id, CancellationToken cancellationToken);

        Task AddAsync(Domain.User model);

    }
}
