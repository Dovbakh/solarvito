using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.Contracts.User
{
    /// <summary>
    /// DTO пользователя с почтой и новым паролем.
    /// </summary>
    public class UserResetPasswordDto
    {
        /// <summary>
        /// Электронная почта пользователя.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Новый пароль пользователя.
        /// </summary>
        public string NewPassword { get; set; }
    }
}
