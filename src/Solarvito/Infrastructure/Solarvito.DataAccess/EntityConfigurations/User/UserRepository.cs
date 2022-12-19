using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Solarvito.AppServices.User.Repositories;
using Solarvito.Contracts.Category;
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
        private readonly ILogger<UserRepository> _logger;

        /// <summary>
        /// Инициализирует экземпляр <see cref="UserRepository"/>.
        /// </summary>
        /// <param name="repository">Базовый репозиторий.</param>
        public UserRepository(IRepository<Domain.User> repository, ILogger<UserRepository> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<UserDto>> GetAll(int take, int skip, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Запрос в репозиторий на получение всех пользователей.");

                return await _repository.GetAll()
                .Select(u => new UserDto()
                {
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
            catch (Exception e)
            {
                _logger.LogError("Ошибка при получении всех пользователей: {ErrorMessage}", e.Message);
                throw;
            }            
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<UserDto>> GetAllFiltered(Expression<Func<Domain.User, bool>> predicate, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Запрос в репозиторий на получение всех пользователей по фильтру.");

                return await _repository.GetAllFiltered(predicate)
                .Select(u => new UserDto()
                {
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
            catch (Exception e)
            {
                _logger.LogError("Ошибка при получении всех пользователей по фильтру: {ErrorMessage}", e.Message);
                throw;
            }            
        }

        /// <inheritdoc />
        public async Task<UserDto> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Запрос в репозиторий на получение пользователя с идентификатором {UserId}.", id);

                var user = await _repository.GetByIdAsync(id);
                if (user == null)
                {
                    _logger.LogError("Не найден пользователь с идентификатором {UserId}", id);
                    throw new KeyNotFoundException($"Не найден пользователь с идентификатором '{id}'");
                }

                var userDto = new UserDto()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Phone = user.Phone,
                    Address = user.Address,
                    Rating = user.Rating,
                    NumberOfRates = user.NumberOfRates,
                    CreatedAt = user.CreatedAt,
                    RoleId = user.RoleId
                };

                return userDto;
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при получении пользователя с идентификатором {UserId}: {ErrorMessage}", id, e.Message);
                throw;
            }           
        }

        /// <inheritdoc />
        public async Task<UserHashDto> GetWithHashByEmail(string email, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Запрос в репозиторий на получение пользователя с почтой {UserEmail} и хэш его пароля.", email);

                var userHashDto = await _repository.GetAllFiltered(user => user.Email == email)
                .Include(u => u.Role)
                .Select(u => new UserHashDto()
                {
                    Id = u.Id,
                    Email = u.Email,
                    PasswordHash = u.PasswordHash,
                    RoleName = u.Role.Name
                }).FirstOrDefaultAsync(cancellationToken);

                return userHashDto;
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при получении пользователя с почтой {UserEmail}: {ErrorMessage}", email, e.Message);
                throw;
            }                         
        }

        /// <inheritdoc />
        public async Task<int> AddAsync(UserHashDto userHashDto, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Запрос в репозиторий на добавление нового пользователя с почтой {UserEmail} и хэш его пароля.", userHashDto.Email);

                var user = new Domain.User()
                {
                    Email = userHashDto.Email,
                    PasswordHash = userHashDto.PasswordHash,
                    CreatedAt = DateTime.UtcNow,
                    RoleId = 1
                };

                await _repository.AddAsync(user);

                return user.Id;
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при добавлении нового пользователя с почтой {UserEmail}: {ErrorMessage}", userHashDto.Email, e.Message);
                throw;
            }            
        }

        /// <inheritdoc />
        public async Task UpdateAsync(UserUpdateRequestDto request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Запрос в репозиторий на изменение пользователя с идентификатором {UserId}.", request.Id);

                var user = await _repository.GetByIdAsync(request.Id);

                if (user == null)
                {
                    _logger.LogError("Не найден пользователь с идентификатором {UserId}", request.Id);
                    throw new KeyNotFoundException($"Не найден пользователь с идентификатором '{request.Id}'");
                }

                user.Address = request.Address;
                user.Phone = request.Phone;
                user.Name = request.Name;

                await _repository.UpdateAsync(user);
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при изменении пользователя с идентификатором {UserId}: {ErrorMessage}", request.Id, e.Message);
                throw;
            }            
        }

        /// <inheritdoc />
        public async Task UpdateAsync(UserDto request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Запрос в репозиторий на изменение пользователя с идентификатором {UserId}.", request.Id);

                var user = await _repository.GetByIdAsync(request.Id);

                if (user == null)
                {
                    _logger.LogError("Не найден пользователь с идентификатором {UserId}", request.Id);
                    throw new KeyNotFoundException($"Не найден пользователь с идентификатором '{request.Id}'");
                }

                user.Address = request.Address;
                user.Phone = request.Phone;
                user.Name = request.Name;
                user.CreatedAt = request.CreatedAt;
                user.Rating = request.Rating;
                user.NumberOfRates = request.NumberOfRates;
                user.RoleId = request.RoleId;

                await _repository.UpdateAsync(user);
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при изменении пользователя с идентификатором {UserId}: {ErrorMessage}", request.Id, e.Message);
                throw;
            }
        }

        /// <inheritdoc />
        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Запрос в репозиторий на удаление пользователя с идентификатором {UserId}.", id);

                var user = await _repository.GetByIdAsync(id);
                if (user == null)
                {
                    _logger.LogError("Не найден пользователь с идентификатором {UserId}", id);
                    throw new KeyNotFoundException($"Не найден пользователь с идентификатором '{id}'");
                }

                await _repository.DeleteAsync(user);
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при удалении пользователя с идентификатором {UserId}: {ErrorMessage}", id, e.Message);
                throw;
            }            
        }
    }
}
