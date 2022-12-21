using Solarvito.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.AppServices.User.Services
{
    /// <summary>
    /// Сервис для работы с пользователями.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Регистрация пользователя.
        /// </summary>
        /// <param name="userRegisterDto">Элемент <see cref="UserRegisterDto"/>.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Идентификатор нового пользователя.</returns>
        Task<string> Register(UserRegisterDto userRegisterDto, CancellationToken cancellationToken);

        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <param name="userLoginDto">Элемент <see cref="UserLoginDto"/>.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Токен.</returns>
        Task<string> Login(UserLoginDto userLoginDto, CancellationToken cancellationToken);

        /// <summary>
        /// Получить всех пользователей с пагинацией.
        /// </summary>
        /// <param name="take">Количество получаемых пользователей.</param>
        /// <param name="skip">Количество пропускаемых пользователей.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Коллекция элементов <see cref="UserDto"/>.</returns>
        Task<IReadOnlyCollection<UserDto>> GetAll(int take, int skip, CancellationToken cancellationToken);

        /// <summary>
        /// Получить пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Элемент <see cref="UserDto"/>.</returns>
        Task<UserDto> GetById(string id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить текущего пользователя.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Элемент <see cref="UserDto"/></returns>
        Task<UserDto> GetCurrent(CancellationToken cancellationToken);

        /// <summary>
        /// Изменить пользователя.
        /// </summary>
        /// <param name="request">Элемент <see cref="UserUpdateRequestDto"/>.</param>
        /// <param name="cancellationToken"></param>
        Task UpdateAsync(string id, UserUpdateRequestDto request, CancellationToken cancellationToken);

        /// <summary>
        /// Изменить пароль пользователя.
        /// </summary>
        /// <param name="userChangePasswordDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task ChangePasswordAsync(UserChangePasswordDto userChangePasswordDto, CancellationToken cancellationToken);

        Task ChangeEmailRequestAsync(string newEmail, string password, string changeLink, CancellationToken cancellationToken);

        Task ChangeEmailAsync(string newEmail, string token, CancellationToken cancellationToken);

        Task ResetPasswordRequestAsync(string email, string resetLink, CancellationToken cancellationToken);

        Task ResetPasswordAsync(string email, string newPassword, string token, CancellationToken cancellationToken);

        /// <summary>
        /// Удалить пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        Task DeleteAsync(string id, CancellationToken cancellationToken);



    }
}
