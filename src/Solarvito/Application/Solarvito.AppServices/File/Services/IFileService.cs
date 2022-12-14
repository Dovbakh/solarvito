using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.AppServices.File.Services
{
    /// <summary>
    /// Сервис для работы с файлами.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Получить файл по имени и директории.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="fileFolder">Директория.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Массив байтов с содержимым файла.</returns>
        Task<byte[]> Get(string fileName, string fileFolder, CancellationToken cancellation);

        /// <summary>
        /// Получить файл-изображение по имени.
        /// </summary>
        /// <param name="fileName">Имя файла-изображения.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Массив байтов с содержимым файла-изображения.</returns>
        Task<byte[]> GetImage(string fileName, CancellationToken cancellation);

        /// <summary>
        /// Добавить файл в директорию.
        /// </summary>
        /// <param name="fileFolder">Директория.</param>
        /// <param name="file">Файл.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Название файла.</returns>
        Task<string> Upload(string fileFolder, IFormFile file, CancellationToken cancellation);

        /// <summary>
        /// Добавить файл-изображение.
        /// </summary>
        /// <param name="file">Файл-изображение.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Название файла-изображения.</returns>
        Task<string> UploadImage(IFormFile file, CancellationToken cancellation);
    }
}
