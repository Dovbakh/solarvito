using Microsoft.AspNetCore.Authorization;
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
        /// <param name="userService"></param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Зарегистрировать пользователя.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Register(string login, string password, CancellationToken cancellation)
        {
            var user = await _userService.Register(login, password, cancellation);

            return Created("", new { });
        }

        /// <summary>
        /// Залогинить пользователя.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Login(string login, string password, CancellationToken cancellation)
        {
            var token = await _userService.Login(login, password, cancellation);

            return Ok(token);
        }
    }
}
