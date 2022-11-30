using Solarvito.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.AppServices.User.Repositories
{
    /// <summary>
    /// Репозиторий чтения/записи для работы с пользователями.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Возвращает пользователей используя постраничную загрузку.
        /// </summary>
        /// <param name="take">Количество записей в ответе.</param>
        /// <param name="skip">Количество пропущеных записей.</param>
        /// <param name="cancellation">Отмена операции.</param>
        /// <returns>Коллекция элементов <see cref="UserDto"/>.</returns>
        Task<IReadOnlyCollection<UserDto>> GetAll(int take, int skip, CancellationToken cancellation);

    }
}
