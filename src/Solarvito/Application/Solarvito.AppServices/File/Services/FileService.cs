using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Minio;
using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using Solarvito.AppServices.File.Repositories;
using Solarvito.AppServices.Services;
using Solarvito.Contracts.Advertisement;
using Solarvito.Contracts.AdvertisementImage;
using static System.Net.WebRequestMethods;

namespace Solarvito.AppServices.File.Services
{
    /// <inheritdoc/>
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly ILogger<FileService> _logger;
        private readonly IDistributedCache _distributedCache;

        /// <summary>
        /// Инициализировать экземпляр <see cref="FileService"/>
        /// </summary>
        /// <param name="fileRepository">Репозиторий для работы с файлами.</param>
        public FileService(
            IFileRepository fileRepository, 
            ILogger<FileService> logger,
            IDistributedCache distributedCache)
        {
            _fileRepository = fileRepository;
            _logger = logger;
            _distributedCache = distributedCache;
        }

        /// <inheritdoc/>
        public Task<byte[]> Get(string fileName, string fileFolder, CancellationToken cancellation)
        {
            return _fileRepository.Get(fileName, fileFolder, cancellation);

        }

        /// <inheritdoc/>
        public Task<byte[]> GetImage(string fileName, CancellationToken cancellation)
        {            
            var fileFolder = "images";

            return _fileRepository.Get(fileName, fileFolder, cancellation);
        }

        /// <inheritdoc/>
        public async Task<string> Upload(string fileFolder, IFormFile file, CancellationToken cancellation)
        {
            var bytes = new byte[file.Length];
            await file.OpenReadStream().ReadAsync(bytes, 0, bytes.Length);

            var fileName = Guid.NewGuid().ToString();
            await _fileRepository.Upload(fileName, fileFolder, file.ContentType, bytes, cancellation);

            return fileName;         
        }

        /// <inheritdoc/>
        public async Task<string> UploadImage(IFormFile file, CancellationToken cancellation)
        {
            if(file.ContentType != "image/png" && file.ContentType != "image/jpeg")
            {
                throw new InvalidOperationException("Неподдерживаемый формат изображения.");
            }

            var bytes = new byte[file.Length];
            await file.OpenReadStream().ReadAsync(bytes, 0, bytes.Length);

            var resizedBytes = await ResizeImage(bytes, 1280, 960, ResizeMode.Max);


            var fileName = Guid.NewGuid().ToString();
            var fileFolder = "images";
            await _fileRepository.Upload(fileName, fileFolder, file.ContentType, resizedBytes, cancellation);

            return fileName;
        }

        public Task Delete(string fileName, string folderName)
        {
            return _fileRepository.Delete(fileName, folderName);
        }

        public Task DeleteImage(string fileName)
        {
            return _fileRepository.Delete(fileName, "images");
        }

        /// <summary>
        /// Изменить размер изображения.
        /// </summary>
        /// <param name="imageBytes">Массив байтов с содержимым файла-изображения.</param>
        /// <param name="width">Необходимая ширина.</param>
        /// <param name="height">Необходимая высота.</param>
        /// <param name="mode">Режим изменения размера.</param>
        /// <returns>Массив байтов с содержимым измененного файла-изображения.</returns>
        private async Task<byte[]> ResizeImage(byte[] imageBytes, int width, int height, ResizeMode mode)
        {
            var imageInfo = Image.Identify(imageBytes);
            if (imageInfo == null)
            {
                throw new Exception("Файл не является изображением.");
            }

            var image = Image.Load(imageBytes);

            var options = new ResizeOptions() { Mode = mode, Size = new Size(width, height) };
            image.Mutate(x => x.Resize(options));

            using (var stream = new MemoryStream())
            {
                await image.SaveAsJpegAsync(stream);
                stream.Position = 0;
                return stream.ToArray();
            }
        }

         
    }
}
