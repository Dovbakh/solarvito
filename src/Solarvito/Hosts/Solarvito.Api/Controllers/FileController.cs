using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Solarvito.AppServices.Advertisement.Services;
using Solarvito.AppServices.File.Services;
using Solarvito.Contracts.Advertisement;
using System.Net;

namespace Solarvito.Api.Controllers
{
    /// <summary>
    /// Работа с файлами.
    /// </summary>
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        /// <summary>
        /// Работа с файлами.
        /// </summary>
        /// <param name="fileService">Сервис для работы с файлами.</param>
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// Получить изображение по имени файла.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Файл с типом контента "image/jpeg"</returns>
        [HttpGet("image")]
        [ProducesResponseType(typeof(File), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetImage(string fileName, CancellationToken cancellation)
        {
            var result = await _fileService.GetImage(fileName, cancellation);
            
            return File(result, "image/jpeg");
        }
        
        /// <summary>
        /// Загрузить изображение.
        /// </summary>
        /// <param name="file">Файл-изображение.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Сгенерированное название файла.</returns>
        [HttpPost("image/upload")]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UploadImage(IFormFile file, CancellationToken cancellation)
        {
            var result = await _fileService.UploadImage(file, cancellation);
            
            return Ok(result);
        }

    }
}
