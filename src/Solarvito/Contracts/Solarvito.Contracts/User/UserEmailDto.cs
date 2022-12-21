using Solarvito.Contracts.User.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.Contracts.User
{
    /// <summary>
    /// DTO пользователя с электронной почтой.
    /// </summary>
    public class UserEmailDto
    {

        /// <summary>
        /// Электронная почта пользователя.
        /// </summary>
        public string Value {get; set;}
    }
}
