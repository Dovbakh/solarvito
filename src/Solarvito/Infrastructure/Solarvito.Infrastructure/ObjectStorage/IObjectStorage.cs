using Microsoft.AspNetCore.Http;
using Minio.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.Infrastructure.ObjectStorage
{
    /// <summary>
    /// Репозиторий обьектного хранилища.
    /// </summary>
    public interface IObjectStorage
    {
        /// <summary>
        /// Получить обьект по имени и корзине.
        /// </summary>
        /// <param name="objectName">Имя обьекта.</param>
        /// <param name="bucketName">Имя корзины.</param>
        /// <returns>Массив байтов с содержимым обьекта.</returns>
        public Task<byte[]> Get(string objectName, string bucketName);

        /// <summary>
        /// Добавить обьект с указанным именем, корзиной и типом контента.
        /// </summary>
        /// <param name="objectName">Имя обьекта.</param>
        /// <param name="bucketName">Имя корзины.</param>
        /// <param name="contentType">Тип контента.</param>
        /// <param name="bytes">Массив байтов с содержимым обьекта.</param>
        Task Upload(string objectName, string bucketName, string contentType, byte[] bytes);
    }
}
