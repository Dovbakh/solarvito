using Solarvito.Contracts.Notifier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.AppServices.Notifier.Services
{
    public interface INotifierService
    {
        void Send(NotifyDto request);
    }
}
