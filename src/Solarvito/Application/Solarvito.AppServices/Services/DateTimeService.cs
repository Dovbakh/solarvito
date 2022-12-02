using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.AppServices.Services
{

    /// <inheritdoc />
    public class DateTimeService : IDateTimeService
    {
        /// <inheritdoc />
        public DateTime GetDateTime()
        {
            return DateTime.Now;
        }

        /// <inheritdoc />
        public DateTime GetUtcDateTime()
        {
            return DateTime.UtcNow;
        }
    }
}
