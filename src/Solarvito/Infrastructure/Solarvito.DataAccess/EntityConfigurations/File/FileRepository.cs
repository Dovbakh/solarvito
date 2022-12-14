using Microsoft.AspNetCore.Http;
using Solarvito.AppServices.File.Repositories;
using Solarvito.Infrastructure.ObjectStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.DataAccess.EntityConfigurations.File
{
    /// <inheritdoc/>
    public class FileRepository : IFileRepository
    {
        private readonly IObjectStorage _objectStorage;

        /// <summary>
        /// Инициализировать экземпляр <see cref="FileRepository"/>.
        /// </summary>
        /// <param name="objectStorage">Базовый репозиторий.</param>
        public FileRepository(IObjectStorage objectStorage)
        {
            _objectStorage = objectStorage;
        }

        /// <inheritdoc/>
        public Task<byte[]> Get(string fileName, string fileFolder, CancellationToken cancellation)
        {
            return _objectStorage.Get(fileName, fileFolder);
        }

        /// <inheritdoc/>
        public Task Upload(string fileName, string fileFolder, string contentType, byte[] bytes, CancellationToken cancellation)
        {
            return _objectStorage.Upload(fileName, fileFolder, contentType, bytes);
        }
    }
}
