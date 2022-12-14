using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Solarvito.AppServices.Advertisement.Services;
using Solarvito.Contracts.Advertisement;
using System.Net;

namespace Solarvito.Api.Controllers
{
    /// <summary>
    /// Работа с обьявлениями.
    /// </summary>
    [ApiController]
    [Route("v1/[controller]")]
    public class AdvertisementController : ControllerBase
    {
        private readonly IAdvertisementService _advertisementService;
        private readonly ILogger<AdvertisementController> _logger;

        /// <summary>
        /// Работа с обьявлениями.
        /// </summary>
        /// <param name="advertisementService">Сервис для работы с обьявлениями.</param>
        /// <param name="logger">Логгер.</param>
        public AdvertisementController(IAdvertisementService advertisementService, ILogger<AdvertisementController> logger)
        {
            _advertisementService = advertisementService;
            _logger = logger;
        }

        /// <summary>
        /// Получить все обьявления отсортированные по дате добавления по убыванию и с пагинацией.
        /// </summary>
        /// <param name="page">Номер страницы.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Коллекция элементов <see cref="AdvertisementResponseDto"/>.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<AdvertisementResponseDto>), (int)HttpStatusCode.OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(int? page, CancellationToken cancellation)
        {
            _logger.LogInformation("Log: information");
            _logger.LogWarning("Log: warning");
            var result = await _advertisementService.GetAllAsync(cancellation, page);

            return Ok(result);
        }

        /// <summary>
        /// Получить все обьявления по фильтру и с пагинацией.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="page"></param>
        /// <param name="cancellation"></param>
        /// <returns>Элемент <see cref="AdvertisementResponseDto"/>.</returns>
        [HttpGet("filter")]
        [ProducesResponseType(typeof(AdvertisementResponseDto), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllFiltered([FromQuery] AdvertisementFilterRequest filter, int? page, CancellationToken cancellation)
        {
            var result = await _advertisementService.GetAllFilteredAsync(filter, cancellation, page);

            return Ok(result);
        }


        /// <summary>
        /// Получить обьявление по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор обьявления.</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns>Элемент <see cref="AdvertisementResponseDto"/>.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(AdvertisementResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellation)
        {
            var result = await _advertisementService.GetByIdAsync(id, cancellation);

            return Ok(result);
        }


        /// <summary>
        /// Добавить новое обьявление.
        /// </summary>
        /// <param name="advertisementRequestDto">Элемент <see cref="AdvertisementRequestDto"/>.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Идентификатор нового обьявления.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [Authorize]
        public async Task<IActionResult> Add([FromForm] AdvertisementRequestDto advertisementRequestDto, CancellationToken cancellation)
        {
            var advertisementId = await _advertisementService.AddAsync(advertisementRequestDto, cancellation);
            return Created("", advertisementId);
        }

        /// <summary>
        /// Изменить обьявление.
        /// </summary>
        /// <param name="id">Идентификатор обьявления.</param>
        /// <param name="advertisementRequestDto">Элемент <see cref="AdvertisementRequestDto"/>.</param>
        /// <param name="cancellation">Токен отмены.</param>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [Authorize]
        public async Task<IActionResult> Update([FromForm] AdvertisementRequestDto advertisementRequestDto, int id, CancellationToken cancellation)
        {
            await _advertisementService.UpdateAsync(id, advertisementRequestDto, cancellation);

            return Ok();
        }

        /// <summary>
        /// Удалить обьявление.
        /// </summary>
        /// <param name="id">Идентификатор обьявления.</param>
        /// <param name="cancellation">Токен отмены.</param>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellation)
        {
            await _advertisementService.DeleteAsync(id, cancellation);

            return Ok();
        }


    }
}
