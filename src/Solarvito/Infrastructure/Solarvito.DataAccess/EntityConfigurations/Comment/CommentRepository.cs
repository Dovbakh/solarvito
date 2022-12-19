using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Minio.DataModel;
using Solarvito.AppServices.Category.Repositories;
using Solarvito.Contracts;
using Solarvito.Contracts.Category;
using Solarvito.Contracts.Comment;
using Solarvito.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.DataAccess.EntityConfigurations.Category
{
    /// <inheritdoc/>
    public class CommentRepository : ICommentRepository
    {
        private readonly IRepository<Domain.Comment> _repository;
        private readonly ILogger<CommentRepository> _logger;

        /// <summary>
        /// Инициализировать экземпляр <see cref="CommentRepository"/>.
        /// </summary>
        /// <param name="repository">Базовый репозиторий.</param>
        public CommentRepository(IRepository<Domain.Comment> repository, ILogger<CommentRepository> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<int> AddAsync(CommentDto commentDto, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("Запрос в репозиторий на добавление нового комментария от пользователя с идентификатором {AuthorId}.", commentDto.AuthorId);

                var comment = commentDto.MapToEntity();
                await _repository.AddAsync(comment);

                return comment.Id;
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при добавлении нового комментария от пользователя с идентификатором {AuthorId}: {ErrorMessage}", commentDto.AuthorId, e.Message);
                throw;
            }
                       
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("Запрос в репозиторий на удаление комментария с идентификатором {CommentId}.", id);

                var comment = await _repository.GetByIdAsync(id);
                if (comment == null)
                {
                    _logger.LogError("Не найден комментарий с идентификатором {CommentId}.", id);
                    throw new KeyNotFoundException($"Не найден комментарий с идентификатором '{id}'");
                }
                await _repository.DeleteAsync(comment);
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при удалении комментария с идентификатором {CommentId}: {ErrorMessage}", id, e.Message);
                throw;
            }                       
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<CommentDto>> GetAllAsync(int take, int skip, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("Запрос в репозиторий на получение {take} комментариев, пропустив {skip} комментариев.", take, skip);

                return await _repository.GetAll()
                    .Include(c => c.Author)
                    .Include(c => c.User)
                    .Include(c => c.Advertisement)
                    .Select(c => c.MapToDto())
                    .Skip(skip).Take(take).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при получении всех комментариев: {ErrorMessage}", e.Message);
                throw;
            }            
        }

        public async Task<IReadOnlyCollection<CommentDto>> GetAllFilteredAsync(CommentFilterRequest filter, int take, int skip, CancellationToken cancellation)
        {
            var query = _repository.GetAll();

            if (filter.UserId.HasValue)
            {
                query = query.Where(a => a.UserId == filter.UserId);
            }

            if (filter.AuthorId.HasValue)
            {
                query = query.Where(a => a.AuthorId == filter.AuthorId);
            }

            if (filter.AdvertisementId.HasValue)
            {
                query = query.Where(a => a.AdvertisementId == filter.AdvertisementId);
            }

            if (!string.IsNullOrWhiteSpace(filter.Text))
            {
                query = query.Where(p => p.Text.ToLower().Contains(filter.Text.ToLower()));
            }

            if (filter.SortBy.HasValue)
            {
                switch (filter.SortBy)
                {
                    case 1:
                        query = filter.OrderDesc == 1 ? query.OrderByDescending(p => p.CreatedAt) : query.OrderBy(p => p.CreatedAt);
                        break;
                    case 2:
                        query = filter.OrderDesc == 1 ? query.OrderByDescending(p => p.Rating) : query.OrderBy(p => p.Rating);
                        break;
                    default:
                        break;
                }
            }

            try
            {
                _logger.LogInformation("Запрос в репозиторий на получение {take} комментариев, пропустив {skip} комментариев с фильтром.", take, skip);
                return await query
                    .Include(c => c.Author)
                    .Include(c => c.User)
                    .Include(c => c.Advertisement)
                    .Select(c => c.MapToDto())
                    .Skip(skip).Take(take).ToListAsync(cancellation);
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при получении списка комментариев по фильтру: {ErrorMessage}.", e.Message);
                throw;
            }
        }
        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<CommentDto>> GetAllFilteredAsync(CommentFilterRequest filter, CancellationToken cancellation)
        {
            var query = _repository.GetAll();

            if (filter.UserId.HasValue)
            {
                query = query.Where(a => a.UserId == filter.UserId);
            }

            if (filter.AuthorId.HasValue)
            {
                query = query.Where(a => a.AuthorId == filter.AuthorId);
            }

            if (filter.AdvertisementId.HasValue)
            {
                query = query.Where(a => a.AdvertisementId == filter.AdvertisementId);
            }

            if (!string.IsNullOrWhiteSpace(filter.Text))
            {
                query = query.Where(p => p.Text.ToLower().Contains(filter.Text.ToLower()));
            }

            if (filter.SortBy.HasValue)
            {
                switch (filter.SortBy)
                {
                    case 1:
                        query = filter.OrderDesc == 1 ? query.OrderByDescending(p => p.CreatedAt) : query.OrderBy(p => p.CreatedAt);
                        break;
                    case 2:
                        query = filter.OrderDesc == 1 ? query.OrderByDescending(p => p.Rating) : query.OrderBy(p => p.Rating);
                        break;
                    default:
                        break;
                }
            }

            try
            {
                _logger.LogInformation("Запрос в репозиторий на получение комментариев с фильтром.");
                return await query
                    .Include(c => c.Author)
                    .Include(c => c.User)
                    .Include(c => c.Advertisement)
                    .Select(c => c.MapToDto())
                    .ToListAsync(cancellation);
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при получении списка комментариев по фильтру: {ErrorMessage}.", e.Message);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<CommentDto> GetByIdAsync(int id, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("Запрос в репозиторий на получение комментария с идентификатором {CommentId}.", id);


                var comment = await _repository.GetAllFiltered(c => c.Id.Equals(id))
                    .Include(c => c.Author)
                    .Include(c => c.User)
                    .Include(c => c.Advertisement)
                    .Select(c => c.MapToDto())
                    .FirstOrDefaultAsync();

                if (comment == null)
                {
                    _logger.LogError("Не найден комментарий с идентификатором {CommentId}", id);
                    throw new KeyNotFoundException($"Не найден комментарий с идентификатором '{id}'");
                }

                return comment;
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при получении комментария с идентификатором {CommentId}: {ErrorMessage}", id, e.Message);
                throw;
            }            
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(int id, CommentDto commentDto, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("Запрос в репозиторий на изменение комментария с идентификатором {CommentId}.", id);

                var comment = await _repository.GetByIdAsync(id);
                if (comment == null)
                {
                    _logger.LogError("Не найден комментарий с идентификатором {CommentId}", id);
                    throw new KeyNotFoundException($"Не найден комментарий с идентификатором '{id}'");
                }

                comment.Text = commentDto.Text;
                comment.Rating = commentDto.Rating;


                await _repository.UpdateAsync(comment);
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при изменении комментария с идентификатором {CommentId}: {ErrorMessage}", id, e.Message);
                throw;
            }            
        }
    }
}
