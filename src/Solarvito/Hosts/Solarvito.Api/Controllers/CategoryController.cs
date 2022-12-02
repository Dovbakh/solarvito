using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Solarvito.AppServices.Advertisement.Services;
using Solarvito.AppServices.Category.Services;
using Solarvito.Contracts.Category;
using System.Net;

namespace Solarvito.Api.Controllers
{
    /// <summary>
    /// Работа с категориями.
    /// </summary>
    [ApiController]
    [Route("v1/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Получить все категории с пагинацией.
        /// </summary>
        /// <param name="take">Количество получаемых категорий.</param>
        /// <param name="skip">Количество пропускаемых категорий.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Коллекция элементов <see cref="CategoryDto"/>.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<CategoryDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(int take, int skip, CancellationToken cancellation)
        {
            var result = await _categoryService.GetAllAsync(take, skip, cancellation);

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
        /// Получить категорию по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns>Элемент <see cref="CategoryDto"/>.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CategoryDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellation)
        {
            var result = await _categoryService.GetByIdAsync(id, cancellation);

            return Ok(result);
        }


        /// <summary>
        /// Добавить новую категорию.
        /// </summary>
        /// <param name="name">Название категории.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Идентификатор новой категории.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add(string name, CancellationToken cancellation)
        {

            var categoryDto = new CategoryDto()
            {
                Name = name
            };

            var categoryId = await _categoryService.AddAsync(categoryDto, cancellation);
            return Created("", categoryId);
        }

        /// <summary>
        /// Изменить категорию.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="name">Название категории.</param>
        /// <param name="cancellation">Токен отмены.</param>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(int id, string name, CancellationToken cancellation)
        {
            var categoryDto = new CategoryDto()
            {
                Name = name
            };
            await _categoryService.UpdateAsync(id, categoryDto, cancellation);

            return Ok();
        }

        /// <summary>
        /// Удалить категорию.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="cancellation">Токен отмены.</param>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellation)
        {
            await _categoryService.DeleteAsync(id, cancellation);

            return Ok();
        }


    }
}
