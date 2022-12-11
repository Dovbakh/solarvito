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
                .Select(u => new UserDto() {
                    Id = u.Id,
                    Name = u.Name,
                    Phone = u.Phone,
                    Address = u.Address,
                    Rating = u.Rating,
                    NumberOfRates = u.NumberOfRates,
                    CreatedAt = u.CreatedAt
                })
                .Skip(skip).Take(take).ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<UserDto>> GetAllFiltered(Expression<Func<Domain.User, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _repository.GetAllFiltered(predicate)
                .Select(u => new UserDto() {
                    Id = u.Id,
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

            var userDto = new UserDto() {
                Id = user.Id,
                Name = user.Name,
                Phone = user.Phone,
                Address = user.Address,
                Rating = user.Rating,
                NumberOfRates = user.NumberOfRates,
                CreatedAt = user.CreatedAt
            };

            return userDto;
        }

        public async Task<UserHashDto> GetWithHashByEmail(string email, CancellationToken cancellationToken)
        {
            var userHashDto = await _repository.GetAllFiltered(user => user.Email == email)
                .Include(u => u.Role)
                .Select(u => new UserHashDto() {
                    Id = u.Id,
                    Email = u.Email,
                    PasswordHash = u.PasswordHash,
                    RoleName = u.Role.Name
                }).FirstOrDefaultAsync(cancellationToken);

            return userHashDto;              
        }

        public async Task<int> AddAsync(UserHashDto userHashDto, CancellationToken cancellationToken)
        {
            var user = new Domain.User() { 
                Email = userHashDto.Email, PasswordHash = userHashDto.PasswordHash, CreatedAt = DateTime.UtcNow, RoleId = 1
            };

            await _repository.AddAsync(user);

            return user.Id;
        }

        public async Task<int> UpdateAsync(UserUpdateRequestDto request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.Id);

            if (user == null)
            {
                throw new Exception($"Не найден пользователь с идентификатором '{request.Id}'");
            }

            user.Address = request.Address;
            user.Phone = request.Phone;
            user.Name = request.Name;

            await _repository.UpdateAsync(user);

            return user.Id;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
            {
                throw new Exception($"Не найден пользователь с идентификатором '{id}'");
            }

            await _repository.DeleteAsync(user);
        }
    }
}
