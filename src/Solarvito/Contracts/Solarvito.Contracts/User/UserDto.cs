using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.Contracts.User
{
    /// <summary>
    /// DTO пользователя.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Электронная почта пользователя.
        /// </summary>
        public string Email { get; set; }

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
        /// Количество отзывов о пользователе.
        /// </summary>
        public int CommentsCount { get; set; }

        /// <summary>
        /// Количество отзывов о пользователе.
        /// </summary>
        public int AdvertisementCount { get; set; }

        /// <summary>
        /// Дата создания пользователя.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Идентификатор роли пользователя.
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// Название роли пользователя.
        /// </summary>
        public string RoleName { get; set; }
    }
}
