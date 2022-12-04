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
        Task<int> Register(string login, string password, CancellationToken cancellationToken);

        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="password">Пароль.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Токен.</returns>
        Task<string> Login(string login, string password, CancellationToken cancellationToken);

        /// <summary>
        /// Получить текущего пользователя.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Текущий пользователь.</returns>
        Task<Domain.User> GetCurrent(CancellationToken cancellationToken);
    }
}
