using Minio.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.Infrastructure.ObjectStorage
{
    public interface IObjectStorage
    {
        public Task<byte[]> Create();
    }
}
