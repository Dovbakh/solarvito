using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.Contracts.Advertisement
{
    public class AdvertisementRequestDto
    {

        /// <summary>
        /// Название обьявления.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Текст обьявления.
        /// </summary>
        public string Description { get; set; }

        public decimal Price { get; set; }

        /// <summary>
        /// Адресс, указанный в обьявлении.
        /// </summary>
        public string Address { get; set; }

        public string Phone { get; set; }

        /// <summary>
        /// Путь к картинке обьявления.
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Идентификатор категории.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Идентификатор категории.
        /// </summary>
        public int UserId { get; set; }

    }
}
