using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Solarvito.AppServices.User.Repositories;
using Solarvito.Contracts;
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
                    .Include(u => u.CommentsFor)
                    .Include(u => u.Advertisements)
                    .Include(u => u.Role)
                    .Select(u => u.MapToDto())
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
                    .Include(u => u.CommentsFor)
                    .Include(u => u.Advertisements)
                    .Include(u => u.Role)
                    .Select(u => u.MapToDto())
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

                var user = await _repository.GetAllFiltered(u => u.Id.Equals(id))
                    .Include(u => u.CommentsFor)
                    .Include(u => u.Advertisements)
                    .Include(u => u.Role)
                    .Select(u => u.MapToDto())
                    .FirstOrDefaultAsync(cancellationToken);

                if (user == null)
                {
                    _logger.LogError("Не найден пользователь с идентификатором {UserId}", id);
                    throw new KeyNotFoundException($"Не найден пользователь с идентификатором '{id}'");
                }

                return user;
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при получении пользователя с идентификатором {UserId}: {ErrorMessage}", id, e.Message);
                throw;
            }           
        }

        /// <inheritdoc />
        public async Task<UserDto> GetByEmail(string email, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Запрос в репозиторий на получение пользователя с почтой {UserEmail}.", email);

                var user = await _repository.GetAllFiltered(u => u.Email.Equals(email))
                    .Include(u => u.CommentsFor)
                    .Include(u => u.Advertisements)
                    .Include(u => u.Role)
                    .Select(u => u.MapToDto())
                    .FirstOrDefaultAsync(cancellationToken);

                if (user == null)
                {
                    _logger.LogWarning("Не найден пользователь с почтой {UserEmail}", email);
                }

                return user;
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при получении пользователя с почтой {UserEmail}: {ErrorMessage}", email, e.Message);
                throw;
            }
        }


        /// <inheritdoc />
        public async Task<int> AddAsync(UserDto userDto, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Запрос в репозиторий на добавление нового пользователя с почтой {UserEmail} и хэш его пароля.", userDto.Email);

                var user = userDto.MapToEntity();
                await _repository.AddAsync(user);

                return user.Id;
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при добавлении нового пользователя с почтой {UserEmail}: {ErrorMessage}", userDto.Email, e.Message);
                throw;
            }            
        }

        /// <inheritdoc />
        public async Task UpdateAsync(int id, UserUpdateRequestDto request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Запрос в репозиторий на изменение пользователя с идентификатором {UserId}.", id);

                var user = await _repository.GetByIdAsync(id);

                if (user == null)
                {
                    _logger.LogError("Не найден пользователь с идентификатором {UserId}", id);
                    throw new KeyNotFoundException($"Не найден пользователь с идентификатором '{id}'");
                }

                user.Address = request.Address;
                user.Phone = request.Phone;
                user.Name = request.Name;

                await _repository.UpdateAsync(user);
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при изменении пользователя с идентификатором {UserId}: {ErrorMessage}", id, e.Message);
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
