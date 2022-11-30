using Solarvito.Contracts.Advertisement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.AppServices.Advertisement.Repositories
{
    /// <summary>
    /// Репозиторий чтения/записи для работы с обьявлениями.
    /// </summary>
    public interface IAdvertisementRepository
    {
        /// <summary>
        /// Возвращает записи товаров используя постраничную загрузку.
        /// </summary>
        /// <param name="take">Количество записей в ответе.</param>
        /// <param name="skip">Количество пропущеных записей.</param>
        /// <param name="cancellation">Отмена операции.</param>
        /// <returns>Коллекция элементов <see cref="AdvertisementDto"/>.</returns>
        Task<IReadOnlyCollection<AdvertisementDto>> GetAll(int take, int skip, CancellationToken cancellation);

        /// <summary>
        /// Создает обьявление.
        /// </summary>
        /// <returns>Идентификатор обьявления <see cref="ShoppingCartDto"/>.</returns>
        Task<int> CreateAsync(AdvertisementDto advertisementDto, CancellationToken cancellation);

        /// <summary>
        /// Изменяет обьявление.
        /// </summary>
        /// <param name="id">Идентификатор обьявления.</param>
        /// <param name="advertisementDto">Измененное обьявление</param>
        Task UpdateAsync(int id, AdvertisementDto advertisementDto, CancellationToken cancellation);

        /// <summary>
        /// Удаляет обьявление.
        /// </summary>
        /// <param name="id">Идентификатор обьявления.</param>
        Task DeleteAsync(int id, CancellationToken cancellation);

    }
}
