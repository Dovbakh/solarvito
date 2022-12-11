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
        Task<byte[]> GetImage(CancellationToken cancellation);

        /// <summary>
        /// Получить все обьявления с пагинацией.
        /// </summary>
        /// <param name="take">Количество получаемых обьявлений.</param>
        /// <param name="skip">Количество пропускаемых обьявлений.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Коллекция элементов <see cref="AdvertisementDto"/>.</returns>
        Task<IReadOnlyCollection<AdvertisementResponseDto>> GetAllAsync(int take, int skip, CancellationToken cancellation);

        Task<IReadOnlyCollection<AdvertisementResponseDto>> GetAllFilteredAsync(AdvertisementFilterRequest filter, int take, int skip, CancellationToken cancellation);

        /// <summary>
        /// Получить обьявление по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор обьявления.</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns>Элемент <see cref="AdvertisementDto"/>.</returns>
        Task<AdvertisementResponseDto> GetByIdAsync(int id, CancellationToken cancellation);

        /// <summary>
        /// Добавить новое обьявление.
        /// </summary>
        /// <param name="advertisementDto">Элемент <see cref="AdvertisementDto"/>.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Идентификатор нового обьявления.</returns>
        Task<int> AddAsync(AdvertisementRequestDto advertisementRequestDto, CancellationToken cancellation);

        /// <summary>
        /// Изменить обьявление.
        /// </summary>
        /// <param name="id">Идентификатор обьявления.</param>
        /// <param name="advertisementDto">Элемент <see cref="AdvertisementDto"/>.</param>
        Task UpdateAsync(int id, AdvertisementRequestDto advertisementDto, CancellationToken cancellation);

        /// <summary>
        /// Удалить обьявление.
        /// </summary>
        /// <param name="id">Идентификатор обьявления.</param>
        Task DeleteAsync(int id, CancellationToken cancellation);

    }
}
