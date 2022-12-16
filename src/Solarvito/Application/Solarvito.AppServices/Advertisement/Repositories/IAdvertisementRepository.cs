using Minio.DataModel;
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
        /// Получить все обьявления с пагинацией.
        /// </summary>
        /// <param name="take">Количество получаемых обьявлений.</param>
        /// <param name="skip">Количество пропускаемых обьявлений.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Коллекция элементов <see cref="AdvertisementResponseDto"/>.</returns>
        Task<IReadOnlyCollection<AdvertisementResponseDto>> GetAllAsync(int take, int skip, CancellationToken cancellation);

        /// <summary>
        /// Получить все обьявления по фильтру и с пагинацией.
        /// </summary>
        /// <param name="filter">Фильтр <see cref="AdvertisementFilterRequest"/> для поиска.</param>
        /// <param name="take">Количество получаемых обьявлений.</param>
        /// <param name="skip">Количество пропускаемых обьявлений.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns></returns>
        Task<IReadOnlyCollection<AdvertisementResponseDto>> GetAllFilteredAsync(AdvertisementFilterRequest filter, int take, int skip, CancellationToken cancellation);

        /// <summary>
        /// Получить обьявление по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор обьявления.</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns>Элемент <see cref="AdvertisementResponseDto"/>.</returns>
        Task<AdvertisementResponseDto> GetByIdAsync(int id, CancellationToken cancellation);

        /// <summary>
        /// Добавить новое обьявление.
        /// </summary>
        /// <param name="advertisementRequestDto">Элемент <see cref="AdvertisementRequestDto"/>.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Идентификатор нового обьявления.</returns>
        Task<int> AddAsync(AdvertisementDto advertisementDto, CancellationToken cancellation);

        /// <summary>
        /// Изменить обьявление.
        /// </summary>
        /// <param name="id">Идентификатор обьявления.</param>
        /// <param name="advertisementDto">Элемент <see cref="AdvertisementDto"/>.</param>
        /// <param name="cancellation">Токен отмены.</param>
        Task UpdateAsync(int id, AdvertisementDto advertisementDto, CancellationToken cancellation);

        Task UpdateAsync(int id, AdvertisementRequestDto advertisementRequestDto, CancellationToken cancellation);

        /// <summary>
        /// Удалить обьявление.
        /// </summary>
        /// <param name="id">Идентификатор обьявления.</param>
        /// <param name="cancellation">Токен отмены.</param>
        Task DeleteAsync(int id, CancellationToken cancellation);

    }
}
