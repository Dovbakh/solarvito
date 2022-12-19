using Solarvito.Contracts.Advertisement;
using Solarvito.Contracts.Category;
using Solarvito.Contracts.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.AppServices.Category.Services
{
    /// <summary>
    /// Сервис для работы с категориями.
    /// </summary>
    public interface ICommentService
    {
        /// <summary>
        /// Получить все категории с пагинацией.
        /// </summary>
        /// <param name="take">Количество получаемых категорий.</param>
        /// <param name="skip">Количество пропускаемых категорий.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Коллекция элементов <see cref="CommentDto"/>.</returns>
        Task<IReadOnlyCollection<CommentDto>> GetAllAsync(int page, CancellationToken cancellation);

        /// <summary>
        /// Получить все категории с пагинацией.
        /// </summary>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Коллекция элементов <see cref="CommentDto"/>.</returns>
        Task<IReadOnlyCollection<CommentDto>> GetAllFilteredAsync(CommentFilterRequest filterRequest, int page, CancellationToken cancellation);

        /// <summary>
        /// Получить категорию по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns>Элемент <see cref="CategoryDto"/>.</returns>
        Task<CommentDto> GetByIdAsync(int id, CancellationToken cancellation);

        /// <summary>
        /// Добавить новый комментарий.
        /// </summary>
        /// <param name="commentRequestDto">Элемент <see cref="CategoryDto"/>.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Идентификатор новой категории.</returns>
        Task<int> AddAsync(CommentRequestDto commentRequestDto, CancellationToken cancellation);

        /// <summary>
        /// Изменить категорию.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="commentUpdateRequestDto">Элемент <see cref="CategoryDto"/>.</param>
        /// <param name="cancellation">Токен отмены.</param>
        Task UpdateAsync(int id, CommentUpdateRequestDto commentUpdateRequestDto, CancellationToken cancellation);

        /// <summary>
        /// Удалить категорию.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="cancellation">Токен отмены.</param>
        Task DeleteAsync(int id, CancellationToken cancellation);
    }
}
