using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.Domain
{
    public class User
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Электронная почта пользователя.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Хэш пароля пользователя.
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Номер телефона пользователя.
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Адрес пользователя.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Рейтинг пользователя.
        /// </summary>
        public float Rating { get; set; }

        /// <summary>
        /// Количество оценок.
        /// </summary>
        public int NumberOfRates { get; set; }

        /// <summary>
        /// Дата создания пользователя.
        /// </summary>
        public DateTime CreatedAt { get; set; }
        
        /// <summary>
        /// Роль пользователя.
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Идентификатор роли пользователя.
        /// </summary>
        public int RoleId {get; set; }

        /// <summary>
        /// Коллекция обьявлений пользователя.
        /// </summary>
        public ICollection<Advertisement> Advertisements { get; set; }

        /// <summary>
        /// Коллекция отзывов, написанных пользователем.
        /// </summary>
        public ICollection<Comment> CommentsBy { get; set; }

        /// <summary>
        /// Коллекция отзывов, написанных пользователем.
        /// </summary>
        public ICollection<Comment> CommentsFor { get; set; }

    }
}
