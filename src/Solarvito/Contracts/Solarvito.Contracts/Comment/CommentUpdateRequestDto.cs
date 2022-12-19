using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.Contracts.Comment
{
    public class CommentUpdateRequestDto
    {
        /// <summary>
        /// Текст отзыва.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Оценка, указанная пользователем.
        /// </summary>
        public int Rating { get; set; }
    }
}
