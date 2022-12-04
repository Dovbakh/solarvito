﻿using Solarvito.Contracts.Advertisement;
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
        /// <returns>Коллекция элементов <see cref="AdvertisementDto"/>.</returns>
        Task<IReadOnlyCollection<AdvertisementDto>> GetAllAsync(int take, int skip, CancellationToken cancellation);

        //// TODO
        //Task<IReadOnlyCollection<AdvertisementDto>> GetAllFilteredAsync(AdvertisementFilterRequest request, int take, int skip, CancellationToken cancellation);

        /// <summary>
        /// Получить все обьявления с пагинацией.
        /// </summary>
        /// <param name="take">Количество получаемых обьявлений.</param>
        /// <param name="skip">Количество пропускаемых обьявлений.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Коллекция элементов <see cref="AdvertisementDto"/>.</returns>
        Task<IReadOnlyCollection<AdvertisementDto>> GetAllByUserIdAsync(int userId, int take, int skip, CancellationToken cancellation);

        /// <summary>
        /// Получить все обьявления с пагинацией.
        /// </summary>
        /// <param name="take">Количество получаемых обьявлений.</param>
        /// <param name="skip">Количество пропускаемых обьявлений.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Коллекция элементов <see cref="AdvertisementDto"/>.</returns>
        Task<IReadOnlyCollection<AdvertisementDto>> GetAllByCategoryIdAsync(int categoryId, int take, int skip, CancellationToken cancellation);

        /// <summary>
        /// Получить обьявление по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор обьявления.</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns>Элемент <see cref="AdvertisementDto"/>.</returns>
        Task<AdvertisementDto> GetByIdAsync(int id, CancellationToken cancellation);

        /// <summary>
        /// Добавить новое обьявление.
        /// </summary>
        /// <param name="advertisementDto">Элемент <see cref="AdvertisementDto"/>.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Идентификатор нового обьявления.</returns>
        Task<int> AddAsync(AdvertisementDto advertisementDto, CancellationToken cancellation);

        /// <summary>
        /// Изменить обьявление.
        /// </summary>
        /// <param name="id">Идентификатор обьявления.</param>
        /// <param name="advertisementDto">Элемент <see cref="AdvertisementDto"/>.</param>
        Task UpdateAsync(int id, AdvertisementDto advertisementDto, CancellationToken cancellation);

        /// <summary>
        /// Удалить обьявление.
        /// </summary>
        /// <param name="id">Идентификатор обьявления.</param>
        Task DeleteAsync(int id, CancellationToken cancellation);

    }
}
