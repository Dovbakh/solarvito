using Solarvito.AppServices.Category.Repositories;
using Solarvito.Contracts.Advertisement;
using Solarvito.Contracts.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.AppServices.Category.Services
{
    /// <inheritdoc/>
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        /// <summary>
        /// Инициализировать экземпляр <see cref="CategoryService"/>
        /// </summary>
        /// <param name="repository">Репозиторий для работы с <see cref="CategoryDto"/></param>
        public CategoryService(ICategoryRepository repository)
        {
            _categoryRepository = repository;
        }

        /// <inheritdoc/>
        public Task<int> AddAsync(CategoryDto categoryDto, CancellationToken cancellation)
        {
            return _categoryRepository.AddAsync(categoryDto, cancellation);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(int id, CancellationToken cancellation)
        {
            return _categoryRepository.DeleteAsync(id, cancellation);
        }

        /// <inheritdoc/>
        public Task<IReadOnlyCollection<CategoryDto>> GetAllAsync(int take, int skip, CancellationToken cancellation)
        {
            return _categoryRepository.GetAllAsync(take, skip, cancellation);
        }

        /// <inheritdoc/>
        public Task<CategoryDto> GetByIdAsync(int id, CancellationToken cancellation)
        {
            return _categoryRepository.GetByIdAsync(id, cancellation);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(int id, CategoryDto categoryDto, CancellationToken cancellation)
        {
            return _categoryRepository.UpdateAsync(id, categoryDto, cancellation);
        }
    }
}
