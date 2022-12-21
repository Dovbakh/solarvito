using Solarvito.Contracts.Notifier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.AppServices.Notifier.Services
{
    /// <summary>
    /// Сервис для отправки уведомлений.
    /// </summary>
    public interface INotifierService
    {
        /// <summary>
        /// Отправить сообщение указанному адресату.
        /// </summary>
        /// <param name="request"></param>
        void Send(NotifyDto request);
    }
}
