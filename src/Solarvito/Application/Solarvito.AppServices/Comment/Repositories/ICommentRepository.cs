using Solarvito.Contracts.Advertisement;
using Solarvito.Contracts.Category;
using Solarvito.Contracts.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.AppServices.Category.Repositories
{
    /// <summary>
    /// Репозиторий чтения/записи для работы с категориями.
    /// </summary>
    public interface ICommentRepository
    {
        /// <summary>
        /// Получить все комментарии с пагинацией.
        /// </summary>
        /// <param name="take">Количество получаемых комментариев.</param>
        /// <param name="skip">Количество пропускаемых комментариев.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Коллекция элементов <see cref="CommentDto"/>.</returns>
        Task<IReadOnlyCollection<CommentDto>> GetAllAsync(int take, int skip, CancellationToken cancellation);

        /// <summary>
        /// Получить комментарий по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор комментария.</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns>Элемент <see cref="CategoryDto"/>.</returns>
        Task<CommentDto> GetByIdAsync(int id, CancellationToken cancellation);

        /// <summary>
        /// Получить все комментарии по фильтру с пагинацией.
        /// </summary>
        /// <param name="filterRequest">Фильтр для поиска.</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns>Элемент <see cref="CategoryDto"/>.</returns>
        Task<IReadOnlyCollection<CommentDto>> GetAllFilteredAsync(CommentFilterRequest filterRequest, int take, int skip, CancellationToken cancellation);

        /// <summary>
        /// Получить все комментарии по фильтру.
        /// </summary>
        /// <param name="filterRequest">Фильтр для поиска.</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns>Элемент <see cref="CategoryDto"/>.</returns>
        Task<IReadOnlyCollection<CommentDto>> GetAllFilteredAsync(CommentFilterRequest filterRequest, CancellationToken cancellation);

        /// <summary>
        /// Добавить новую комментарий.
        /// </summary>
        /// <param name="commentDto">Элемент <see cref="CategoryDto"/>.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Идентификатор новой комментария.</returns>
        Task<int> AddAsync(CommentDto commentDto, CancellationToken cancellation);

        /// <summary>
        /// Изменить комментарий.
        /// </summary>
        /// <param name="id">Идентификатор комментария.</param>
        /// <param name="commentDto">Элемент <see cref="CommentDto"/>.</param>
        Task UpdateAsync(int id, CommentDto commentDto, CancellationToken cancellation);

        /// <summary>
        /// Удалить комментарий.
        /// </summary>
        /// <param name="id">Идентификатор комментария.</param>
        Task DeleteAsync(int id, CancellationToken cancellation);
    }
}
