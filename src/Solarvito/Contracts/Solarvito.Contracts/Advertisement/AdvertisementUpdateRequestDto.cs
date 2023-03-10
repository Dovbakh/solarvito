using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.Contracts.Advertisement
{
    /// <summary>
    /// DTO добавляемого обьявления.
    /// </summary>
    public class AdvertisementUpdateRequestDto
    {

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
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Коллекция новых прикрепленных изображений.
        /// </summary>
        public ICollection<IFormFile>? Images { get; set; }

        /// <summary>
        /// Имена-файлов на уже прикрепленные изображения из обьявления.
        /// </summary>
        public ICollection<string>? ImagePathes { get; set; }

        /// <summary>
        /// Идентификатор категории.
        /// </summary>
        public int CategoryId { get; set; }

    }
}
