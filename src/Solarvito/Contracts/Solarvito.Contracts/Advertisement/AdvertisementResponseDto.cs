using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.Contracts.Advertisement
{
    /// <summary>
    /// DTO получаемого обьявления.
    /// </summary>
    public class AdvertisementResponseDto
    {
        /// <summary>
        /// Идентификатор обьявления.
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// Название обьявления.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Текст обьявления.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Адрес.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Номер телефона.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Ссылки на изображения из обьявления.
        /// </summary>
        public ICollection<string> ImagePathes { get; set; }

        /// <summary>
        /// Дата создания обьявления.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дата истечения срока обьявления.
        /// </summary>
        public DateTime ExpireAt { get; set; }

        /// <summary>
        /// Количество просмотров обьявления.
        /// </summary>
        public int NumberOfViews { get; set; }

        /// <summary>
        /// Идентификатор категории.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Название категории обьявления.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Имя пользователя, разместившего обьявление.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Рейтинг пользователя, разместившего обьявление.
        /// </summary>
        public float UserRating { get; set; }

        /// <summary>
        /// Количество отзывов о пользователе, разместившем обьявление.
        /// </summary>
        public int UserNumberOfRates { get; set; }
    }
}
