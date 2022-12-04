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

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Получить всех пользователей с пагинацией.
        /// </summary>
        /// <param name="take">Количество получаемых пользователей.</param>
        /// <param name="skip">Количество пропускаемых пользователей.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Коллекция элементов <see cref="UserDto"/>.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<UserDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(int take, int skip, CancellationToken cancellation)
        {
            var result = await _userService.GetAllAsync(take, skip, cancellation);

            return Ok(result);
        }

        //// TODO
        //[HttpGet]
        //[ProducesResponseType(typeof(AdvertisementDto), (int)HttpStatusCode.OK)]
        //public async Task<IActionResult> GetAllFiltered(int id, CancellationToken cancellation)
        ////        public async Task<IActionResult> GetAllFiltered(string text, int categoryId, int userId, bool isName, bool isDescription, bool is CancellationToken cancellation)
        //{
        //    var result = await _advertisementService.GetByIdAsync(id, cancellation);

        //    return Ok(result);
        //}

        /// <summary>
        /// Получить пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns>Элемент <see cref="UserDto"/>.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellation)
        {
            var result = await _userService.GetByIdAsync(id, cancellation);

            return Ok(result);
        }


        /// <summary>
        /// Добавить новуого пользователя.
        /// </summary>
        /// <param name="name">Название пользователя.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Идентификатор нового пользователя.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add(string name, CancellationToken cancellation)
        {

            var userDto = new UserDto()
            {
                Name = name
            };

            var userId = await _userService.AddAsync(userDto, cancellation);
            return Created("", userId);
        }

        /// <summary>
        /// Изменить пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="name">Название пользователя.</param>
        /// <param name="cancellation">Токен отмены.</param>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(int id, string name, CancellationToken cancellation)
        {
            var userDto = new UserDto()
            {
                Name = name
            };
            await _userService.UpdateAsync(id, userDto, cancellation);

            return Ok();
        }

        /// <summary>
        /// Удалить пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellation">Токен отмены.</param>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellation)
        {
            await _userService.DeleteAsync(id, cancellation);

            return Ok();
        }


    }
}
