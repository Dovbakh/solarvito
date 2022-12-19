using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.Contracts.Comment
{
    public class CommentFilterRequest
    {
        /// <summary>
        /// Текст отзыва.
        /// </summary>
        public string? Text { get; set; }
        /// <summary>
        /// Оценка, указанная пользователем.
        /// </summary>
        public int? Rating { get; set; }
        /// <summary>
        /// Идентификатор пользователя, которому оставлен отзыв.
        /// </summary>
        public int? AuthorId { get; set; }
        /// <summary>
        /// Идентификатор пользователя, которому оставлен отзыв.
        /// </summary>
        public int? UserId { get; set; }
        /// <summary>
        /// Идентификатор обьявления, к которому оставлен отзыв.
        /// </summary>
        public int? AdvertisementId { get; set; }

        /// <summary>
        /// Порядок сортировки.
        /// </summary>
        public int? OrderDesc { get; set; }

        /// <summary>
        /// Вид сортировки
        /// 1 - сортировка по дате добавления.
        /// 2 - сортировка по рейтингу.
        /// </summary>
        public int? SortBy { get; set; }
    }
}
