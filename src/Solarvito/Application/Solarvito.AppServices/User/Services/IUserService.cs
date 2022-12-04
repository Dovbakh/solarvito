using Solarvito.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.AppServices.User.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Получить всех пользователей с пагинацией.
        /// </summary>
        /// <param name="take">Количество получаемых пользователей.</param>
        /// <param name="skip">Количество пропускаемых пользователей.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Коллекция элементов <see cref="UserDto"/>.</returns>
        Task<IReadOnlyCollection<UserDto>> GetAllAsync(int take, int skip, CancellationToken cancellation);

        /// <summary>
        /// Получить пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns>Элемент <see cref="UserDto"/>.</returns>
        Task<UserDto> GetByIdAsync(int id, CancellationToken cancellation);

        /// <summary>
        /// Добавить нового пользователя.
        /// </summary>
        /// <param name="userDto">Элемент <see cref="UserDto"/>.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Идентификатор нового пользователя.</returns>
        Task<int> AddAsync(UserDto userDto, CancellationToken cancellation);

        /// <summary>
        /// Изменить пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="userDto">Элемент <see cref="UserDto"/>.</param>
        Task UpdateAsync(int id, UserDto userDto, CancellationToken cancellation);

        /// <summary>
        /// Удалить пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        Task DeleteAsync(int id, CancellationToken cancellation);
    }
}
