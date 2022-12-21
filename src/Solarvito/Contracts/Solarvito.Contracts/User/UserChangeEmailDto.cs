using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.Contracts.User
{
    public class UserChangeEmailDto
    {
        /// <summary>
        /// Новая почта пользователя.
        /// </summary>
        public UserEmailDto newEmail { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public UserPasswordDto Password { get; set; }
    }
}
