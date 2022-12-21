using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.Domain
{
    public class Role : IdentityRole
    {
        /// <summary>
        /// Коллекция пользователей с этой ролью.
        /// </summary>
        public IReadOnlyCollection<User> Users { get; set; }
    }
}
