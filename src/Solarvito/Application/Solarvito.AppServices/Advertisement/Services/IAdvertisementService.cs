using Minio.DataModel;
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
        Task<byte[]> GetImage(CancellationToken cancellation);

        /// <summary>
        /// Получить все обьявления с пагинацией.
        /// </summary>
        /// <param name="take">Количество получаемых обьявлений.</param>
        /// <param name="skip">Количество пропускаемых обьявлений.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Коллекция элементов <see cref="AdvertisementResponseDto"/>.</returns>
        Task<IReadOnlyCollection<AdvertisementResponseDto>> GetAllAsync(CancellationToken cancellation, int? page);

        // TODO
        Task<IReadOnlyCollection<AdvertisementResponseDto>> GetAllFilteredAsync(AdvertisementFilterRequest request, CancellationToken cancellation, int? page);

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
        /// <param name="advertisementRequestDto">Элемент <see cref="AdvertisementRequestDto"/>.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Идентификатор нового обьявления.</returns>
        Task<int> AddAsync(AdvertisementRequestDto advertisementRequestDto, CancellationToken cancellation);

        /// <summary>
        /// Изменить обьявление.
        /// </summary>
        /// <param name="id">Идентификатор обьявления.</param>
        /// <param name="advertisementDto">Элемент <see cref="AdvertisementDto"/>.</param>
        Task UpdateAsync(int id, AdvertisementRequestDto advertisementRequestDto, CancellationToken cancellation);

        /// <summary>
        /// Удалить обьявление.
        /// </summary>
        /// <param name="id">Идентификатор обьявления.</param>
        Task DeleteAsync(int id, CancellationToken cancellation);
    }
}
