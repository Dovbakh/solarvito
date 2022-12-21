using Minio.DataModel;
using Solarvito.Contracts.Advertisement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.AppServices.Advertisement.Services
{
    /// <summary>
    /// Сервис для работы с обьявлениями.
    /// </summary>
    public interface IAdvertisementService
    {
        /// <summary>
        /// Получить все обьявления с пагинацией.
        /// </summary>
        /// <param name="page">Номер страницы.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Коллекция элементов <see cref="AdvertisementResponseDto"/>.</returns>
        Task<IReadOnlyCollection<AdvertisementResponseDto>> GetAllAsync(CancellationToken cancellation, int? page);

        /// <summary>
        /// Получить все обьявления по фильтру и с пагинацией.
        /// </summary>
        /// <param name="request">Фильтр <see cref="AdvertisementFilterRequest"/> для поиска.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <param name="page">Номер страницы.</param>
        /// <returns>Коллекция элементов <see cref="AdvertisementResponseDto"/>.</returns>
        Task<IReadOnlyCollection<AdvertisementResponseDto>> GetAllFilteredAsync(AdvertisementFilterRequest request, CancellationToken cancellation, int? page);

        /// <summary>
        /// Получить обьявление по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор обьявления.</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns>Элемент <see cref="AdvertisementResponseDto"/>.</returns>
        Task<AdvertisementResponseDto> GetByIdAsync(int id, CancellationToken cancellation);

        Task<IReadOnlyCollection<AdvertisementResponseDto>> GetHistoryAsync(int? page, CancellationToken cancellation);

        Task<IReadOnlyCollection<AdvertisementResponseDto>> GetLastViewedAsync(int? count, CancellationToken cancellation);
        /// <summary>
        /// Добавить новое обьявление.
        /// </summary>
        /// <param name="advertisementRequestDto">Элемент <see cref="AdvertisementRequestDto"/>.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Идентификатор нового обьявления.</returns>
        Task<int> AddAsync(AdvertisementRequestDto advertisementRequestDto, CancellationToken cancellation);

        /// <summary>
        /// Изменить обьявление.
        /// </summary>
        /// <param name="id">Идентификатор обьявления.</param>
        /// <param name="advertisementUpdateRequestDto">Элемент <see cref="AdvertisementUpdateRequestDto"/>.</param>
        /// <param name="cancellation">Токен отмены.</param>
        Task UpdateAsync(int id, AdvertisementUpdateRequestDto advertisementUpdateRequestDto, CancellationToken cancellation);

        /// <summary>
        /// Удалить обьявление.
        /// </summary>
        /// <param name="id">Идентификатор обьявления.</param>
        /// <param name="cancellation">Токен отмены.</param>
        Task DeleteAsync(int id, CancellationToken cancellation);
    }
}
