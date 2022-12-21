using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.Contracts.User
{
    /// <summary>
    /// DTO пользователя с новой электронной почтой и текущим паролем.
    /// </summary>
    public class UserChangeEmailDto
    {
        /// <summary>
        /// Новая почта пользователя.
        /// </summary>
        public string newEmail { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public string Password { get; set; }
    }
}
