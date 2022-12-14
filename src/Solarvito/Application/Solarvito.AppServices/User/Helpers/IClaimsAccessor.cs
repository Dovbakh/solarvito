using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.AppServices.User.Additional
{
    /// <summary>
    /// Аксессор для работы с клеймами.
    /// </summary>
    public interface IClaimsAccessor
    {
        /// <summary>
        /// Получить клеймы.
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns>Список элементов <see cref="Claim"/></returns>
        Task<IEnumerable<Claim>> GetClaims(CancellationToken cancellation);
    }
}
