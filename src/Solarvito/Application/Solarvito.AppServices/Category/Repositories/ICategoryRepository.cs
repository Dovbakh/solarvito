using Solarvito.Contracts.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.AppServices.Category.Repositories
{
    /// <summary>
    /// Репозиторий чтения/записи для работы с категориями.
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// Возвращает записи товаров используя постраничную загрузку.
        /// </summary>
        /// <param name="take">Количество записей в ответе.</param>
        /// <param name="skip">Количество пропущеных записей.</param>
        /// <param name="cancellation">Отмена операции.</param>
        /// <returns>Коллекция элементов <see cref="CategoryDto"/>.</returns>
        Task<IReadOnlyCollection<CategoryDto>> GetAll(CancellationToken cancellation);

    }
}
