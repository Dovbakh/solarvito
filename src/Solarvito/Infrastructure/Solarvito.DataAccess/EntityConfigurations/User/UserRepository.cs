﻿using Solarvito.AppServices.User.Repositories;
using Solarvito.Contracts.User;
using Solarvito.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.DataAccess.EntityConfigurations.User
{
    public class UserRepository : IUserRepository
    {
        private readonly IRepository<Domain.User> _repository;

        /// <summary>
        /// Инициализировать экземпляр <see cref="UserRepository"/>.
        /// </summary>
        /// <param name="repository">Базовый репозиторий.</param>
        public UserRepository(IRepository<Domain.User> repository)
        {
            _repository = repository;
        }

        /// <inheritdoc/>
        public Task<int> AddAsync(UserDto userDto, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task DeleteAsync(int id, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<IReadOnlyCollection<UserDto>> GetAll(int take, int skip, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<IReadOnlyCollection<UserDto>> GetAllAsync(int take, int skip, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<UserDto> GetByIdAsync(int id, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task UpdateAsync(int id, UserDto userDto, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
