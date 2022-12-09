using Solarvito.Contracts.User;
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
        Task<IReadOnlyCollection<UserDto>> GetAll(int take, int skip, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<UserDto>> GetAllFiltered(Expression<Func<Domain.User, bool>> predicate, CancellationToken cancellationToken);

        Task<UserDto> GetById(int id, CancellationToken cancellationToken);

        Task<UserHashDto> GetWithHashByEmail(string email, CancellationToken cancellationToken);

        Task<int> AddAsync(UserHashDto userHashDto, CancellationToken cancellationToken);

        Task DeleteAsync(int id, CancellationToken cancellationToken);

    }
}
