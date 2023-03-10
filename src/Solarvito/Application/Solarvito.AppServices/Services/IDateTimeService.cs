using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.AppServices.Services
{
    /// <summary>
    /// Сервис предоставления даты и времени.
    /// </summary>
    public interface IDateTimeService
    {
        /// <summary>
        /// Возвращает текущую системную дату.
        /// </summary>
        /// <returns>Текущая системная дата.</returns>
        DateTime GetDateTime();

        /// <summary>
        /// Возвращает текущую системную дату в формате UTC.
        /// </summary>
        /// <returns>Текущая системная дата в формате UTC.</returns>
        DateTime GetUtcDateTime();
    }
}
