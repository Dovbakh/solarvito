using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.Contracts.Comment
{
    public class CommentRequestDto
    {
        /// <summary>
        /// Текст отзыва.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Оценка, указанная пользователем.
        /// </summary>
        public int Rating { get; set; }
        /// <summary>
        /// Идентификатор пользователя, который оставил отзыв.
        /// </summary>
        public int AuthorId { get; set; }
        /// <summary>
        /// Идентификатор пользователя, которому оставлен отзыв.
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Идентификатор обьявления, к которому оставлен отзыв.
        /// </summary>
        public int AdvertisementId { get; set; }
    }
}
