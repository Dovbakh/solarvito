﻿using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// Инициализировать экземпляр <see cref="CategoryRepository"/>.
        /// </summary>
        /// <param name="repository">Базовый репозиторий.</param>
        public CategoryRepository(IRepository<Domain.Category> repository)
        {
            _repository = repository;
        }

        /// <inheritdoc/>
        public async Task<int> AddAsync(CategoryDto categoryDto, CancellationToken cancellation)
        {
            var category = categoryDto.MapToEntity();
            await _repository.AddAsync(category);

            return category.Id;
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id, CancellationToken cancellation)
        {
            var category = await _repository.GetByIdAsync(id);
            if(category == null)
            {
                throw new Exception($"Не найдена категория с идентификатором '{id}'");
            }
            await _repository.DeleteAsync(category);
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<CategoryDto>> GetAllAsync(int take, int skip, CancellationToken cancellation)
        {
            return await _repository.GetAll()
                .Select(c => c.MapToDto())
                .Skip(skip).Take(take).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<CategoryDto> GetByIdAsync(int id, CancellationToken cancellation)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
            {
                throw new Exception($"Не найдена категория с идентификатором '{id}'");
            }

            var categoryDto = category.MapToDto();
            return categoryDto;
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(int id, CategoryDto categoryDto, CancellationToken cancellation)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
            {
                throw new Exception($"Не найдена категория с идентификатором '{id}'");
            }

            category.Name = categoryDto.Name;

            await _repository.UpdateAsync(category);
        }
    }
}
