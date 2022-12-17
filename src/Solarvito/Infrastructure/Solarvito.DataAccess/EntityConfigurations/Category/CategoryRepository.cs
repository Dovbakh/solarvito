using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Solarvito.AppServices.Category.Repositories;
using Solarvito.Contracts;
using Solarvito.Contracts.Category;
using Solarvito.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.DataAccess.EntityConfigurations.Category
{
    /// <inheritdoc/>
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IRepository<Domain.Category> _repository;
        private readonly ILogger<CategoryRepository> _logger;

        /// <summary>
        /// Инициализировать экземпляр <see cref="CategoryRepository"/>.
        /// </summary>
        /// <param name="repository">Базовый репозиторий.</param>
        public CategoryRepository(IRepository<Domain.Category> repository, ILogger<CategoryRepository> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<int> AddAsync(CategoryDto categoryDto, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("Запрос в репозиторий на добавление новой категории с именем {CategoryName}.", categoryDto.Name);

                var category = categoryDto.MapToEntity();
                await _repository.AddAsync(category);

                return category.Id;
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при добавлении новой категории с именем {CategoryName}: {ErrorMessage}", categoryDto.Name, e.Message);
                throw;
            }
                       
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("Запрос в репозиторий на удаление категории с ID {CategoryId}.", id);

                var category = await _repository.GetByIdAsync(id);
                if (category == null)
                {
                    _logger.LogError("Не найдена категория с идентификатором {CategoryId}.", id);
                    throw new KeyNotFoundException($"Не найдена категория с идентификатором '{id}'");
                }
                await _repository.DeleteAsync(category);
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при удалении категории с идентификатором {CategoryId}: {ErrorMessage}", id, e.Message);
                throw;
            }                       
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<CategoryDto>> GetAllAsync(int take, int skip, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("Запрос в репозиторий на получение всех категорий.");

                return await _repository.GetAll()
                .Select(c => c.MapToDto())
                .Skip(skip).Take(take).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при получении всех категорий: {ErrorMessage}", e.Message);
                throw;
            }            
        }

        /// <inheritdoc/>
        public async Task<CategoryDto> GetByIdAsync(int id, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("Запрос в репозиторий на получение категории с идентификатором {CategoryId}.", id);

                var category = await _repository.GetByIdAsync(id);
                if (category == null)
                {
                    _logger.LogError("Не найдена категория с идентификатором {CategoryId}", id);
                    throw new KeyNotFoundException($"Не найдена категория с идентификатором '{id}'");
                }

                var categoryDto = category.MapToDto();
                return categoryDto;
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при получении категории с идентификатором {CategoryId}: {ErrorMessage}", id, e.Message);
                throw;
            }            
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(int id, CategoryDto categoryDto, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("Запрос в репозиторий на изменение категории с идентификатором {CategoryId}.", id);

                var category = await _repository.GetByIdAsync(id);
                if (category == null)
                {
                    _logger.LogError("Не найдена категория с идентификатором {CategoryId}", id);
                    throw new KeyNotFoundException($"Не найдена категория с идентификатором '{id}'");
                }

                category.Name = categoryDto.Name;

                await _repository.UpdateAsync(category);
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при изменении категории с идентификатором {CategoryId}: {ErrorMessage}", id, e.Message);
                throw;
            }            
        }
    }
}
