using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.AppServices.User.Additional
{
    public interface IClaimsAccessor
    {
        Task<IEnumerable<Claim>> GetClaims(CancellationToken cancellation);
    }
}
