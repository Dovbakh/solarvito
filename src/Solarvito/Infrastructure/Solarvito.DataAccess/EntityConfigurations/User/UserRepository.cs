using Microsoft.EntityFrameworkCore;
using Solarvito.AppServices.User.Repositories;
using Solarvito.Contracts.User;
using Solarvito.Domain;
using Solarvito.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.DataAccess.EntityConfigurations.User
{
    /// <inheritdoc />
    public class UserRepository : IUserRepository
    {
        private readonly IRepository<Domain.User> _repository;

        /// <summary>
        /// Инициализирует экземпляр <see cref="UserRepository"/>.
        /// </summary>
        /// <param name="repository">Базовый репозиторий.</param>
        public UserRepository(IRepository<Domain.User> repository)
        {
            _repository = repository;
        }
        public async Task<IReadOnlyCollection<UserDto>> GetAll(int take, int skip, CancellationToken cancellationToken)
        {
            return await _repository.GetAll()
                .Select(u => new UserDto()
                {
                    Id = u.Id,
                    Email = u.Email,
                    Name = u.Name,
                    Phone = u.Phone,
                    Address = u.Address,
                    Rating = u.Rating,
                    NumberOfRates = u.NumberOfRates,
                    CreatedAt = u.CreatedAt
                })
                .Take(take).Skip(skip).ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<UserDto>> GetAllFiltered(Expression<Func<Domain.User, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _repository.GetAllFiltered(predicate)
                .Select(u => new UserDto()
                {
                    Id = u.Id,
                    Email = u.Email,
                    Name = u.Name,
                    Phone = u.Phone,
                    Address = u.Address,
                    Rating = u.Rating,
                    NumberOfRates = u.NumberOfRates,
                    CreatedAt = u.CreatedAt
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<UserDto> GetById(int id, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(id);

            if (user == null)
            {
                throw new Exception($"Не найден пользователь с идентификатором '{id}'");
            }

            var userDto = new UserDto()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Phone = user.Phone,
                Address = user.Address,
                Rating = user.Rating,
                NumberOfRates = user.NumberOfRates,
                CreatedAt = user.CreatedAt
            };

            return userDto;
        }

        //public async Task<string> GetPasswordHashById(int id, CancellationToken cancellationToken)
        //{
        //    var user = await _repository.GetByIdAsync(id);

        //    if (user == null) {
        //        throw new Exception($"Не найден пользователь с идентификатором '{id}'");
        //    }

        //    return user.Password;
        //}

        public async Task<UserVerifyDto> GetWithHashByEmail(string email, CancellationToken cancellationToken)
        {
            var user = await _repository.GetAllFiltered(user => user.Email == email)
                .Select(u => new UserVerifyDto()
                {
                    Id = u.Id,
                    Email = u.Email,
                    PasswordHash = u.PasswordHash
                }).FirstOrDefaultAsync();

            return user;              
        }

        public Task AddAsync(Domain.User model)
        {
            return _repository.AddAsync(model);
        }


    }
}
