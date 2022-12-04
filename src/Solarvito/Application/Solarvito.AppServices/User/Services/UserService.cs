using Solarvito.AppServices.User.Repositories;
using Solarvito.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.AppServices.User.Services
{
    /// <inheritdoc/>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Инициализировать экземпляр <see cref="UserService"/>
        /// </summary>
        /// <param name="repository">Репозиторий для работы с <see cref="UserDto"/></param>
        public UserService(IUserRepository repository)
        {
            _userRepository = repository;
        }

        /// <inheritdoc/>
        public Task<int> AddAsync(UserDto userDto, CancellationToken cancellation)
        {
            return _userRepository.AddAsync(userDto, cancellation);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(int id, CancellationToken cancellation)
        {
            return _userRepository.DeleteAsync(id, cancellation);
        }

        /// <inheritdoc/>
        public Task<IReadOnlyCollection<UserDto>> GetAllAsync(int take, int skip, CancellationToken cancellation)
        {
            return _userRepository.GetAllAsync(take, skip, cancellation);
        }

        /// <inheritdoc/>
        public Task<UserDto> GetByIdAsync(int id, CancellationToken cancellation)
        {
            return _userRepository.GetByIdAsync(id, cancellation);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(int id, UserDto userDto, CancellationToken cancellation)
        {
            return _userRepository.UpdateAsync(id, userDto, cancellation);
        }
    }
}
