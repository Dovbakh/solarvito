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
        /// <summary>
        /// Получить всех пользователей с пагинацией.
        /// </summary>
        /// <param name="take">Количество получаемых пользователей.</param>
        /// <param name="skip">Количество пропускаемых пользователей.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Коллекция элементов <see cref="UserDto"/>.</returns>
        Task<IReadOnlyCollection<UserDto>> GetAll(int take, int skip, CancellationToken cancellationToken);

        /// <summary>
        /// Получить всех пользователей по предикату.
        /// </summary>
        /// <param name="predicate">Предикат.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Коллекция элементов <see cref="UserDto"/>.</returns>
        Task<IReadOnlyCollection<UserDto>> GetAllFiltered(Expression<Func<Domain.User, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Получить пользователя по идентификатору.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Элемент <see cref="UserDto"/>.</returns>
        Task<UserDto> GetById(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить пользователя с хэшем пароля по электронной почте.
        /// </summary>
        /// <param name="email">Электронная почта.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Элемент <see cref="UserHashDto"/>.</returns>
        Task<UserHashDto> GetWithHashByEmail(string email, CancellationToken cancellationToken);

        /// <summary>
        /// Добавить пользователя с хэшем пароля.
        /// </summary>
        /// <param name="userHashDto">Элемент <see cref="UserHashDto"/>.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Идентификатор нового пользователя.</returns>
        Task<int> AddAsync(UserHashDto userHashDto, CancellationToken cancellationToken);

        /// <summary>
        /// Изменить пользователя.
        /// </summary>
        /// <param name="request">Элемент <see cref="UserUpdateRequestDto"/>.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        Task UpdateAsync(UserUpdateRequestDto request, CancellationToken cancellationToken);

        /// <summary>
        /// Удалить пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        Task DeleteAsync(int id, CancellationToken cancellationToken);

    }
}
