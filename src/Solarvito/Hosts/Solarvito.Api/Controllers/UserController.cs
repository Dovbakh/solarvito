using Microsoft.AspNetCore.Authorization;
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
    [AllowAnonymous]
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
        [HttpGet("{id:int}")]        
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellation)
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
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> Update(int id, UserUpdateRequestDto userUpdateRequestDto, CancellationToken cancellation)
        {
            await _userService.UpdateAsync(id, userUpdateRequestDto, cancellation);

            return Ok();
        }

        ///// <summary>
        ///// Изменить пользователя по идентификатору.
        ///// </summary>
        ///// <param name="id">Идентификатор пользователя.</param>
        ///// <param name="cancellation">Токен отмены.</param>
        //[HttpPut("email/{id:int}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize]
        //public async Task<IActionResult> ChangeEmail(UserChangeConfirmDto userChangeConfirmDto, CancellationToken cancellation)
        //{
        //    await _userService.ChangeEmailAsync(userChangeConfirmDto, cancellation);

        //    return Ok();
        //}

        ///// <summary>
        ///// Изменить пользователя по идентификатору.
        ///// </summary>
        ///// <param name="id">Идентификатор пользователя.</param>
        ///// <param name="cancellation">Токен отмены.</param>
        //[HttpPut("password/{id:int}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize]
        //public async Task<IActionResult> ChangePassword(UserChangePasswordDto userChangePasswordDto, CancellationToken cancellation)
        //{
        //    await _userService.ChangePasswordAsync(userChangePasswordDto, cancellation);

        //    return Ok();
        //}

        ///// <summary>
        ///// Изменить пользователя по идентификатору.
        ///// </summary>
        ///// <param name="id">Идентификатор пользователя.</param>
        ///// <param name="cancellation">Токен отмены.</param>
        //[HttpPut("password/reset/{id:int}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize]
        //public async Task<IActionResult> ResetPassword(string email, CancellationToken cancellation)
        //{
        //    await _userService.ResetPasswordAsync(email, cancellation);

        //    return Ok();
        //}



        /// <summary>
        /// Удалить пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellation">Токен отмены.</param>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellation)
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
