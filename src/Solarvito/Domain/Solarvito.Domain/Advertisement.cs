using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.Domain
{
    internal class Advertisement
    {
        /// <summary>
        /// Идентификатор обьявления.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор категории.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Название обьявления.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Текст обьявления.
        /// </summary>
        public string Description {get; set; }

        /// <summary>
        /// Адресс, указанный в обьявлении.
        /// </summary>
        public string Adress { get; set; }

        /// <summary>
        /// Путь к картинке обьявления.
        /// </summary>
        public string ImagePath { get; set; }

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
        /// Категория обьявления.
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// Пользователь, подавший обьявление.
        /// </summary>
        public User User { get; set; }
    }
}
