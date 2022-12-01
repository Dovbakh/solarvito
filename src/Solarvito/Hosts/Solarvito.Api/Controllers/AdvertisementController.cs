using Microsoft.AspNetCore.Mvc;
using Solarvito.AppServices.Advertisement.Services;
using Solarvito.Contracts.Advertisement;
using System.Net;

namespace Solarvito.Api.Controllers
{
    /// <summary>
    /// Работа с обьявлениями.
    /// </summary>
    [ApiController]
    [Route("v1/[controller]")]
    public class AdvertisementController : ControllerBase
    {
        private readonly IAdvertisementService _advertisementService;

        public AdvertisementController(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<AdvertisementDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(int take, int skip, CancellationToken cancellation)
        {
            var result = await _advertisementService.GetAllAsync(take, skip, cancellation);

            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(AdvertisementDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellation)
        {
            var result = await _advertisementService.GetByIdAsync(id, cancellation);

            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="cancellation"></param>
        /// <returns>Возвращает идентификатор нового пользователя</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add(string name, string description, int categoryId, string imagePath, CancellationToken cancellation)
        {

            var advertisementDto = new AdvertisementDto()
            {
                Name = name,
                Description = description,
                UserId = 1,
                CategoryId = categoryId,
                ImagePath = imagePath,
                NumberOfViews = 0
            };

            var advertisementId = await _advertisementService.AddAsync(advertisementDto, cancellation);
            return Created("", advertisementId);
        }

        /// <summary>
        /// Удаляет обьявление
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellation)
        {
            await _advertisementService.DeleteAsync(id, cancellation);

            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(int id, string name, string description, int categoryId, string imagePath, CancellationToken cancellation)
        {
            var advertisementDto = new AdvertisementDto()
            {
                Name = name,
                Description = description,
                CategoryId = categoryId,
                ImagePath = imagePath,
            };
            await _advertisementService.UpdateAsync(id, advertisementDto, cancellation);

            return Ok();
        }
    }
}
