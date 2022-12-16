using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.AppServices.File.Repositories
{
    /// <summary>
    /// Репозиторий чтения\записи для работы с файлами.
    /// </summary>
    public interface IFileRepository
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
        /// Добавить файл с именем и типом контента в директорию (из массива байтов).
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="fileFolder">Директория.</param>
        /// <param name="contentType">Тип контента.</param>
        /// <param name="bytes">Массив байтов с содержимым файла.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns></returns>
        Task Upload(string fileName, string fileFolder, string contentType, byte[] bytes, CancellationToken cancellation);

        Task Delete(string fileName, string folderName);
    }
}
