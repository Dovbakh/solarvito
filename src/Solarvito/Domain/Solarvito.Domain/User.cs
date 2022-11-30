using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.Domain
{
    internal class User
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Логин пользователя.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Номер телефона пользователя.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Электронная почта пользователя.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Адрес пользователя.
        /// </summary>
        public string Adress { get; set; }

        /// <summary>
        /// Рейтинг пользователя.
        /// </summary>
        public decimal Rating { get; set; }
        
        /// <summary>
        /// Дата создания пользователя.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Коллекция обьявлений пользователя.
        /// </summary>
        public ICollection<Advertisement> Advertisements { get; set; }

    }
}
