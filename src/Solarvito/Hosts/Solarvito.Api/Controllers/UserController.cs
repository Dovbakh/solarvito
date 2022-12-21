﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Solarvito.AppServices.User.Services;
using Solarvito.Contracts.User;
using System.Net;

namespace Solarvito.Api.Controllers
{
    /// <summary>
    /// Работа с пользователями.
    /// </summary>
    [ApiController]
    [Route("v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Работа с пользователями.
        /// </summary>
        /// <param name="userService">Сервис для работы с пользователями.</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Получить список всех пользователей с пагинацией.
        /// </summary>
        /// <param name="take">Количество получаемых пользователей.</param>
        /// <param name="skip">Количество пропускаемых пользователей.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Коллекция элементов <see cref="UserDto"/>.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<UserDto>), StatusCodes.Status200OK)]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAll(int take, int skip, CancellationToken cancellation)
        {
            var users = await _userService.GetAll(take, skip, cancellation);

            return Ok(users);
        }

        /// <summary>
        /// Получить пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Элемент <see cref="UserDto"/>.</returns>
        [HttpGet("{id}")]        
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellation)
        {
            var user = await _userService.GetById(id, cancellation);

            return Ok(user);
        }

        /// <summary>
        /// Получить текущего пользователя.
        /// </summary>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Элемент <see cref="UserDto"/>.</returns>
        [HttpGet("current")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> GetCurrent(CancellationToken cancellation)
        {
            var user = await _userService.GetCurrent(cancellation);

            return Ok(user);
        }

        /// <summary>
        /// Изменить пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellation">Токен отмены.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> Update(string id, UserUpdateRequestDto userUpdateRequestDto, CancellationToken cancellation)
        {
            await _userService.UpdateAsync(id, userUpdateRequestDto, cancellation);

            return Ok();
        }

        /// <summary>
        /// Изменить пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellation">Токен отмены.</param>
        [HttpGet("change-email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> ChangeEmail([FromQuery] UserEmailDto newEmail, string token, CancellationToken cancellation)
        {
            await _userService.ChangeEmailAsync(newEmail, token, cancellation);

            return Ok();
        }

        /// <summary>
        /// Изменить пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellation">Токен отмены.</param>
        [HttpPost("change-email-request")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> ChangeEmailRequest([FromBody] UserChangeEmailDto request, CancellationToken cancellation)
        {
            var changeLink = Url.Action(nameof(ChangeEmail), "User", new { newEmail = request.newEmail, token = "tokenValue" }, Request.Scheme);

            await _userService.ChangeEmailRequestAsync(request, changeLink, cancellation);

            return Ok();
        }

        /// <summary>
        /// Изменить пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellation">Токен отмены.</param>
        [HttpPut("change-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] UserChangePasswordDto userChangePasswordDto, CancellationToken cancellation)
        {
            await _userService.ChangePasswordAsync(userChangePasswordDto, cancellation);

            return Ok();
        }

        /// <summary>
        /// Изменить пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellation">Токен отмены.</param>
        [HttpGet("reset-password-request")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPasswordRequest([FromQuery] UserEmailDto email, CancellationToken cancellation)
        {
            var resetLink  = Url.Action(nameof(ResetPasswordConfirm), "User", new { email = email.Value, token = "tokenValue" }, Request.Scheme);

            await _userService.ResetPasswordRequestAsync(email, resetLink, cancellation);          

            return Ok();
        }

        /// <summary>
        /// Изменить пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellation">Токен отмены.</param>
        [HttpGet("reset-password-confirm")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPasswordConfirm(string email, string token, CancellationToken cancellation)
        {           
            return Ok(new { email, token });
        }

        /// <summary>
        /// Изменить пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellation">Токен отмены.</param>
        [HttpPut("reset-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(UserResetPasswordDto request, string token, CancellationToken cancellation)
        {
            await _userService.ResetPasswordAsync(request, token, cancellation);

            return Ok();
        }



        /// <summary>
        /// Удалить пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellation">Токен отмены.</param>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellation)
        {
            await _userService.DeleteAsync(id, cancellation);

            return Ok();
        }



        /// <summary>
        /// Зарегистрировать пользователя.
        /// </summary>
        /// <param name="userRegisterDto">Элемент <see cref="UserRegisterDto"/>.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Идентификатор нового пользователя.</returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto, CancellationToken cancellation)
        {
            var userId = await _userService.Register(userRegisterDto, cancellation);

            return Ok(userId);
        }

        /// <summary>
        /// Залогинить пользователя.
        /// </summary>
        /// <param name="userLoginDto">Элемент <see cref="UserLoginDto"/>.</param>
        /// <param name="cancellation"></param>
        /// <returns>Токен аутентификации.</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto, CancellationToken cancellation)
        {
            var token = await _userService.Login(userLoginDto, cancellation);

            return Ok(token);
        }



    }
}
