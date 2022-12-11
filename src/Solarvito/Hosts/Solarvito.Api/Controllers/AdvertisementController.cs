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

        public AdvertisementController(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        [HttpGet("image")]
        [ProducesResponseType(typeof(IReadOnlyCollection<AdvertisementResponseDto>), (int)HttpStatusCode.OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetImage(CancellationToken cancellation)
        {
            var result = await _advertisementService.GetImage(cancellation);
            return File(result, "image/png");

            return Ok(result);
            //Byte[] b = System.IO.File.ReadAllBytes(@"E:\\Test.jpg");
            //Byte[] b = System.IO.File.ReadAllBytes()
            //return File(b, "image/jpeg");

        }


        /// <summary>
        /// Получить все обьявления отсортированные по дате добавления по убыванию и с пагинацией.
        /// </summary>
        /// <param name="take">Количество получаемых обьявлений.</param>
        /// <param name="skip">Количество пропускаемых обьявлений.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Коллекция элементов <see cref="AdvertisementDto"/>.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<AdvertisementResponseDto>), (int)HttpStatusCode.OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(int? page, CancellationToken cancellation)
        {
            var result = await _advertisementService.GetAllAsync(cancellation, page);

            return Ok(result);
        }

        // TODO
        [HttpGet("filter")]
        [ProducesResponseType(typeof(AdvertisementResponseDto), (int)HttpStatusCode.OK)]
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
        [ProducesResponseType(typeof(AdvertisementResponseDto), (int)HttpStatusCode.OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellation)
        {
            var result = await _advertisementService.GetByIdAsync(id, cancellation);

            return Ok(result);
        }


        /// <summary>
        /// Добавить новое обьявление.
        /// </summary>
        /// <param name="name">Название обьявления.</param>
        /// <param name="description">Описание обьявления.</param>
        /// <param name="categoryId">Идентификатор категории обьявления.</param>
        /// <param name="imagePath">Путь к картинке в обьявлении.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Идентификатор нового обьявления.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize]
        public async Task<IActionResult> Add(AdvertisementRequestDto advertisementRequestDto, CancellationToken cancellation)
        {
            var advertisementId = await _advertisementService.AddAsync(advertisementRequestDto, cancellation);
            return Created("", advertisementId);
        }

        /// <summary>
        /// Изменить обьявление.
        /// </summary>
        /// <param name="id">Идентификатор обьявления.</param>
        /// <param name="name">Название обьявления.</param>
        /// <param name="description">Описание обьявления.</param>
        /// <param name="categoryId">Идентификатор категории обьявления.</param>
        /// <param name="imagePath">Путь к картинке в обьявлении.</param>
        /// <param name="cancellation">Токен отмены.</param>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] AdvertisementRequestDto advertisementRequestDto, int id, CancellationToken cancellation)
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
        [Authorize]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellation)
        {
            await _advertisementService.DeleteAsync(id, cancellation);

            return Ok();
        }


    }
}
