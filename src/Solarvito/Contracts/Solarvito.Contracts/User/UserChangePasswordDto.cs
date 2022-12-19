using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.Contracts.User
{
    public class UserChangePasswordDto
    {
        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Новый пароль пользователя.
        /// </summary>
        public string NewPassword { get; set; }
    }
}
