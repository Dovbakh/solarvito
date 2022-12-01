using Solarvito.Contracts.Advertisement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.AppServices.Advertisement.Services
{
    public interface IAdvertisementService
    {
        /// <summary>
        /// Возвращает записи товаров используя постраничную загрузку.
        /// </summary>
        /// <param name="take">Количество записей в ответе.</param>
        /// <param name="skip">Количество пропущеных записей.</param>
        /// <param name="cancellation">Отмена операции.</param>
        /// <returns>Коллекция элементов <see cref="AdvertisementDto"/>.</returns>
        Task<IReadOnlyCollection<AdvertisementDto>> GetAllAsync(int take, int skip, CancellationToken cancellation);

        /// <summary>
        /// Возвращает записи товаров по фильтру используя постраничную загрузку.
        /// </summary>
        /// <param name="take">Количество записей в ответе.</param>
        /// <param name="skip">Количество пропущеных записей.</param>
        /// <param name="cancellation">Отмена операции.</param>
        /// <returns>Коллекция элементов <see cref="AdvertisementDto"/>.</returns>
        Task<IReadOnlyCollection<AdvertisementDto>> GetAllFilteredAsync(AdvertisementFilterRequest request, int take, int skip, CancellationToken cancellation);

        /// <summary>
        /// Возвращает обьявление по идентификатору.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<AdvertisementDto> GetByIdAsync(int id, CancellationToken cancellation);

        /// <summary>
        /// Создает обьявление.
        /// </summary>
        /// <returns>Идентификатор обьявления <see cref="AdvertisementDto"/>.</returns>
        Task<int> AddAsync(AdvertisementDto advertisementDto, CancellationToken cancellation);

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
