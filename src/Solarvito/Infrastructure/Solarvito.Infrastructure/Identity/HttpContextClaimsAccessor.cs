using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Solarvito.AppServices.User.Additional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.Infrastructure.Identity
{
    public class HttpContextClaimsAccessor : IClaimsAccessor
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public HttpContextClaimsAccessor(IHttpContextAccessor contextAccessor) 
        {
            _contextAccessor = contextAccessor;
        }

        public async Task<IEnumerable<Claim>> GetClaims(CancellationToken cancellation)
        {
            return _contextAccessor.HttpContext.User.Claims;           
        }

        public async Task SetClaims(ClaimsPrincipal principal, CancellationToken cancellation)
        {
            await _contextAccessor.HttpContext.SignInAsync(principal);
        }
    }
}
