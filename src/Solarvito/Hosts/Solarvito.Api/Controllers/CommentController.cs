using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Solarvito.AppServices.Advertisement.Services;
using Solarvito.AppServices.Category.Services;
using Solarvito.Contracts.Category;
using Solarvito.Contracts.Comment;
using System.Net;

namespace Solarvito.Api.Controllers
{
    /// <summary>
    /// Работа с комментариями.
    /// </summary>
    [ApiController]
    [Route("v1/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        /// <summary>
        /// Работа с комментариями.
        /// </summary>
        /// <param name="commentService">Сервис для работы с комментариями.</param>
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        /// <summary>
        /// Получить все комментарии с пагинацией.
        /// </summary>
        /// <param name="page">Номер страницы с комментариями.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Коллекция элементов <see cref="CommentDto"/>.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<CommentDto>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(int? page, CancellationToken cancellation)
        {
            var result = await _commentService.GetAllAsync(page, cancellation);

            return Ok(result);
        }

        /// <summary>
        /// Получить все комментарии по фильтру и с пагинацией.
        /// </summary>
        /// <param name="page">Номер страницы с комментариями.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Коллекция элементов <see cref="CommentDto"/>.</returns>
        [HttpGet("filter")]
        [ProducesResponseType(typeof(IReadOnlyCollection<CommentDto>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllFiltered([FromQuery] CommentFilterRequest filter, int? page, CancellationToken cancellation)
        {
            var result = await _commentService.GetAllFilteredAsync(filter, page, cancellation);

            return Ok(result);
        }


        /// <summary>
        /// Получить комментарий по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор комментария.</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns>Элемент <see cref="CommentDto"/>.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CommentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellation)
        {
            var result = await _commentService.GetByIdAsync(id, cancellation);

            return Ok(result);
        }


        /// <summary>
        /// Добавить новый комментарий.
        /// </summary>
        /// <param name="commentRequestDto">Элемент <see cref="CommentDto"/>.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Идентификатор нового комментария.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]

        [Authorize]
        public async Task<IActionResult> Add(CommentRequestDto commentRequestDto, CancellationToken cancellation)
        {
            var categoryId = await _commentService.AddAsync(commentRequestDto, cancellation);
            return Created("", categoryId);
        }

        /// <summary>
        /// Изменить комментарий.
        /// </summary>
        /// <param name="id">Идентификатор комментария.</param>
        /// <param name="сommentUpdateRequestDto">Элемент <see cref="CommentDto"/>.</param>
        /// <param name="cancellation">Токен отмены.</param>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [Authorize]
        public async Task<IActionResult> Update(int id, CommentUpdateRequestDto сommentUpdateRequestDto, CancellationToken cancellation)
        {
            await _commentService.UpdateAsync(id, сommentUpdateRequestDto, cancellation);

            return Ok();
        }

        /// <summary>
        /// Удалить комментарий.
        /// </summary>
        /// <param name="id">Идентификатор комментария.</param>
        /// <param name="cancellation">Токен отмены.</param>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellation)
        {
            await _commentService.DeleteAsync(id, cancellation);

            return Ok();
        }


    }
}
