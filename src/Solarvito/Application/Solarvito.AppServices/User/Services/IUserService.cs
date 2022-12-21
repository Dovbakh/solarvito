﻿using Solarvito.Contracts.User;
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
        /// Удалить пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        Task DeleteAsync(string id, CancellationToken cancellationToken);

        /// <summary>
        /// Изменить пароль у пользователя.
        /// </summary>
        /// <param name="userChangePasswordDto">Элемент <see cref="UserChangePasswordDto"/>.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        Task ChangePasswordAsync(UserChangePasswordDto userChangePasswordDto, CancellationToken cancellationToken);

        /// <summary>
        /// Получение токена для изменения почты пользователя и его отправка на новую почту.
        /// </summary>
        /// <param name="request">Элемент <see cref="UserChangeEmailDto"/>.</param>
        /// /// <param name="changeLink">Шаблон ссылки на изменение почты.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Токен для изменения почты пользователя.</returns>
        Task ChangeEmailRequestAsync(UserChangeEmailDto request, string changeLink, CancellationToken cancellationToken);

        /// <summary>
        /// Изменить электронную почту у пользователя.
        /// </summary>
        /// <param name="newEmail">Элемент <see cref="UserEmailDto"/>.</param>
        /// <param name="token">Сгенерированный токен смены почты.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        Task ChangeEmailAsync(UserEmailDto newEmail, string token, CancellationToken cancellationToken);

        /// <summary>
        /// Получение токена для сброса пароля пользователя и его отправка на почту.
        /// </summary>
        /// <param name="email">Почта пользователя.</param>
        /// <param name="resetLink">Шаблон для ссылки на сброс пароля.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        Task ResetPasswordRequestAsync(UserEmailDto email, string resetLink, CancellationToken cancellationToken);

        /// <summary>
        /// Сброс пароля пользователя.
        /// </summary>
        /// <param name="request">Элемент <see cref="UserResetPasswordDto"/>.</param>
        /// <param name="token">Сгенерированный токен на сброс пароля.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        Task ResetPasswordAsync(UserResetPasswordDto request, string token, CancellationToken cancellationToken);



    }
}
