using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.AppServices.AdvertisementImage.Repositories
{
    /// <summary>
    /// Репозиторий для чтения\записи изображений из обьявления.
    /// </summary>
    public interface IAdvertisementImageRepository
    {
        /// <summary>
        /// Добавить изображение.
        /// </summary>
        /// <param name="advertisementImage">Элемент <see cref="Domain.AdvertisementImage"/></param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns></returns>
        Task<int> AddAsync(Domain.AdvertisementImage advertisementImage, CancellationToken cancellation);


        /// <summary>
        /// Удалить изображение.
        /// </summary>
        /// <param name="id">Идентификатор изображения.</param>
        /// <param name="cancellation">Токен отмены.</param>
        Task DeleteAsync(int id, CancellationToken cancellation);



        /// <summary>
        /// Получить все изображения по идентификатору обьявления.
        /// </summary>
        /// <param name="advertisementId">Идентификатор обьявления.</param>
        /// <param name="cancellation">ТОкен отмены.</param>
        /// <returns>Коллекция элементов <see cref="Domain.AdvertisementImage"/>.</returns>
        Task<IReadOnlyCollection<Domain.AdvertisementImage>> GetAllByAdvertisementId(int advertisementId, CancellationToken cancellation);


        /// <summary>
        /// Получить изображение по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Элемент <see cref="Domain.AdvertisementImage"/>.</returns>
        Task<Domain.AdvertisementImage> GetByIdAsync(int id, CancellationToken cancellation);

    }
}
