﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.Contracts.User
{
    /// <summary>
    /// DTO пользователя с хэшем пароля.
    /// </summary>
    public class UserHashDto
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

        public string RoleName { get; set; }
    }
}
