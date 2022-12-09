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
        /// Регистрация пользователя.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="password">Пароль.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Идентификатор пользователя.</returns>
        Task<int> Register(UserLoginDto userLoginDto, CancellationToken cancellationToken);

        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="password">Пароль.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Токен.</returns>
        Task<string> Login(UserLoginDto userLoginDto, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<UserDto>> GetAll(int take, int skip, CancellationToken cancellationToken);

        Task<UserDto> GetById(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить текущего пользователя.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Текущий пользователь.</returns>
        Task<UserDto> GetCurrent(CancellationToken cancellationToken);



        
    }
}
