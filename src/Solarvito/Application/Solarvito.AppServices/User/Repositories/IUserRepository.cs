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
        /// Получить пользователя по идентификатору.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Элемент <see cref="UserDto"/>.</returns>
        Task<UserDto> GetById(string id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить пользователя по почте.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Элемент <see cref="UserDto"/>.</returns>
        Task<UserDto> GetByEmail(string email, CancellationToken cancellationToken);

        /// <summary>
        /// Добавить пользователя с хэшем пароля.
        /// </summary>
        /// <param name="userDto">Элемент <see cref="UserDto"/>.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Идентификатор нового пользователя.</returns>
        Task<string> AddAsync(UserRegisterDto userRegisterDto, CancellationToken cancellationToken);
        
        /// <summary>
        /// Проверить пароль пользователя.
        /// </summary>
        /// <param name="userDto"></param>
        /// <param name="password"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> CheckPasswordAsync(string email, string password, CancellationToken cancellationToken);

        Task ChangePasswordAsync(string email, string currentPassword, string newPassword, CancellationToken cancellationToken);

        Task ChangeEmailAsync(string currentEmail, string newEmail, string token, CancellationToken cancellationToken);

        Task<string> ChangeEmailRequestAsync(string currentEmail, string newEmail, CancellationToken cancellationToken);

        Task<string> ResetPasswordRequestAsync(string email, CancellationToken cancellationToken);

        Task ResetPasswordAsync(string email, string token, string newPassword, CancellationToken cancellationToken);

        /// <summary>
        /// Изменить пользователя.
        /// </summary>
        /// <param name="request">Элемент <see cref="UserUpdateRequestDto"/>.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        Task UpdateAsync(string id, UserUpdateRequestDto request, CancellationToken cancellationToken);


        /// <summary>
        /// Удалить пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        Task DeleteAsync(string id, CancellationToken cancellationToken);

    }
}
