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

        /// <summary>
        /// Получить все обьявления с пагинацией.
        /// </summary>
        /// <param name="take">Количество получаемых обьявлений.</param>
        /// <param name="skip">Количество пропускаемых обьявлений.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Коллекция элементов <see cref="AdvertisementDto"/>.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<AdvertisementDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(int take, int skip, CancellationToken cancellation)
        {
            var result = await _advertisementService.GetAllAsync(take, skip, cancellation);

            return Ok(result);
        }

        //// TODO
        //[HttpGet("userId")]
        //[ProducesResponseType(typeof(AdvertisementDto), (int)HttpStatusCode.OK)]
        //public async Task<IActionResult> GetAllFiltered(int id, CancellationToken cancellation)
        ////        public async Task<IActionResult> GetAllFiltered(string text, int categoryId, int userId, bool isName, bool isDescription, bool is CancellationToken cancellation)
        //{
        //    var result = await _advertisementService.GetByIdAsync(id, cancellation);

        //    return Ok(result);
        //}

        /// <summary>
        /// Получить все обьявления по идентификатору пользователя с пагинацией.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="take">Количество получаемых обьявлений.</param>
        /// <param name="skip">Количество пропускаемых обьявлений.</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns>Элемент <see cref="AdvertisementDto"/>.</returns>
        [HttpGet("userId/{userId:int}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<AdvertisementDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllByUserId(int userId, int take, int skip, CancellationToken cancellation)
        {
            var result = await _advertisementService.GetAllByUserIdAsync(userId, take, skip, cancellation);

            return Ok(result);
        }

        /// <summary>
        /// Получить все обьявления по идентификатору категории с пагинацией.
        /// </summary>
        /// <param name="categoryId">Идентификатор категории.</param>
        /// <param name="take">Количество получаемых обьявлений.</param>
        /// <param name="skip">Количество пропускаемых обьявлений.</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns>Элемент <see cref="AdvertisementDto"/>.</returns>
        [HttpGet("categoryId/{categoryId:int}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<AdvertisementDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllByCategoryId(int categoryId, int take, int skip, CancellationToken cancellation)
        {
            var result = await _advertisementService.GetAllByCategoryIdAsync(categoryId, take, skip, cancellation);

            return Ok(result);
        }

        /// <summary>
        /// Получить обьявление по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор обьявления.</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns>Элемент <see cref="AdvertisementDto"/>.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(AdvertisementDto), (int)HttpStatusCode.OK)]
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
        public async Task<IActionResult> Add(string name, string description, int categoryId, string imagePath, CancellationToken cancellation)
        {

            var advertisementDto = new AdvertisementDto()
            {
                Name = name,
                Description = description,
                UserId = 1,
                CategoryId = categoryId,
                ImagePath = imagePath,
                NumberOfViews = 0
            };

            var advertisementId = await _advertisementService.AddAsync(advertisementDto, cancellation);
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
        public async Task<IActionResult> Update(int id, string name, string description, int categoryId, string imagePath, CancellationToken cancellation)
        {
            var advertisementDto = new AdvertisementDto()
            {
                Name = name,
                Description = description,
                CategoryId = categoryId,
                ImagePath = imagePath,
            };
            await _advertisementService.UpdateAsync(id, advertisementDto, cancellation);

            return Ok();
        }

        /// <summary>
        /// Удалить обьявление.
        /// </summary>
        /// <param name="id">Идентификатор обьявления.</param>
        /// <param name="cancellation">Токен отмены.</param>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellation)
        {
            await _advertisementService.DeleteAsync(id, cancellation);

            return Ok();
        }


    }
}
