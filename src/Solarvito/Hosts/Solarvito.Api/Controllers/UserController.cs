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
        /// <param name="userService"></param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(int take, int skip, CancellationToken cancellation)
        {
            var users = await _userService.GetAll(take, skip, cancellation);

            return Ok(users);
        }


        [HttpGet("{id:int}")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellation)
        {
            var user = await _userService.GetById(id, cancellation);

            return Ok(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpGet("current")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCurrent(CancellationToken cancellation)
        {
            var user = await _userService.GetCurrent(cancellation);

            return Ok(user);
        }

        /// <summary>
        /// Зарегистрировать пользователя.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register(UserLoginDto userLoginDto, CancellationToken cancellation)
        {
            var userId = await _userService.Register(userLoginDto, cancellation);

            return Ok(userId);
        }

        /// <summary>
        /// Залогинить пользователя.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto, CancellationToken cancellation)
        {
            var token = await _userService.Login(userLoginDto, cancellation);

            return Ok(token);
        }



    }
}
