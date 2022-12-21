using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Solarvito.AppServices.File.Repositories;
using Solarvito.Infrastructure.ObjectStorage;
using Solarvito.Infrastructure.Repository;
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
        private readonly ILogger<FileRepository> _logger;
        private readonly ICachedRepository<byte[]> _cachedRepository;

        private readonly string CacheKey = "FilesKey_";

        /// <summary>
        /// Инициализировать экземпляр <see cref="FileRepository"/>.
        /// </summary>
        /// <param name="objectStorage">Базовый репозиторий.</param>
        public FileRepository(
            IObjectStorage objectStorage, 
            ILogger<FileRepository> logger,
            ICachedRepository<byte[]> cachedRepository)
        {
            _objectStorage = objectStorage;
            _logger = logger;
            _cachedRepository = cachedRepository;
        }

        /// <inheritdoc/>
        public async Task<byte[]> Get(string fileName, string fileFolder, CancellationToken cancellation)
        {
            try
            {
                var cache = await _cachedRepository.GetById(CacheKey + fileName);
                if (cache != null)
                {
                    return cache;
                }


                _logger.LogInformation("Запрос в обьектное хранилище на получение файла с именем '{FileName}' в корзине '{FolderName}'.", fileName, fileFolder);
                var fileBytes = await _objectStorage.Get(fileName, fileFolder);

                _cachedRepository.SetWithId(CacheKey + fileName, fileBytes);

                return fileBytes;
            }
            catch(Exception e)
            {
                _logger.LogError("Ошибка при получении файла '{FileName}' из корзины '{FolderName}'.", fileName, fileFolder);
                throw;
            }
        }

        /// <inheritdoc/>
        public Task Upload(string fileName, string fileFolder, string contentType, byte[] bytes, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("Запрос в обьектное хранилище на добавление файла с именем '{FileName}' в корзину '{FolderName}'. Тип контента: {ContentType}. Размер файла: {FileSize}", fileName, fileFolder, contentType, bytes.Length);
                return _objectStorage.Upload(fileName, fileFolder, contentType, bytes);
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при добавлении файла с именем '{FileName}' в корзину '{FolderName}'. Тип контента: {ContentType}. Размер файла: {FileSize}", fileName, fileFolder, contentType, bytes.Length);
                throw;
            }        
        }

        public Task Delete(string fileName, string folderName)
        {
            try
            {
                _logger.LogInformation("Запрос в обьектное хранилище на удаление файла с именем '{FileName}' в корзине '{FolderName}'.", fileName, folderName);
                return _objectStorage.Delete(fileName, folderName);
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка при удалении файла '{FileName}' из корзины '{FolderName}'.", fileName, folderName);
                throw;
            }         
        }
    }
}
