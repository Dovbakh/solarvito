using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.Contracts.Comment
{
    public class CommentDto
    {
        /// <summary>
        /// Идентификатор отзыва.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Текст отзыва.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Оценка, указанная пользователем.
        /// </summary>
        public int Rating { get; set; }
        /// <summary>
        /// Дата отзыва.
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Идентификатор пользователя, который оставил отзыв.
        /// </summary>
        public string AuthorId { get; set; }
        /// <summary>
        /// Идентификатор пользователя, которому оставлен отзыв.
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// Идентификатор обьявления, к которому оставлен отзыв.
        /// </summary>
        public int AdvertisementId { get; set; }
        /// <summary>
        /// Имя пользователя-автора.
        /// </summary>
        public string? AuthorName { get; set; }
        /// <summary>
        /// Имя пользователя, которому оставлен отзыв.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Название обьявления, к которому оставлен отзыв.
        /// </summary>
        public string AdvertisementName { get; set; }

    }
}
