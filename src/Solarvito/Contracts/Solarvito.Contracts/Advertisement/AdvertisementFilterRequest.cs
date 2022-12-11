using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.Contracts.Advertisement
{
    public class AdvertisementFilterRequest
    {
        /// <summary>
        /// Идентификатор категории.
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Название обьявления.
        /// </summary>
        public string? Text { get; set; }

        public decimal? minPrice { get; set; }

        public decimal? maxPrice { get; set; }

        /// <summary>
        /// выше 4х
        /// </summary>
        public int? highRating { get; set; }

        public int? OrderDesc { get; set; }

        /// <summary>
        /// 1 - сортировка по дате добавления
        /// 2 - сортировка по цене
        /// </summary>
        public int? SortBy { get; set; }


    }
}
