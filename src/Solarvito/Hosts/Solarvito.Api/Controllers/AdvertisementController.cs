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
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add(string name, int userId, int categoryId, CancellationToken cancellation)
        {

            var advertisementDto = new AdvertisementDto()
            {
                Name = name,
                Description = "1",
                UserId = userId,
                CategoryId = categoryId,
                ImagePath = "1",
                CreatedAt = DateTime.UtcNow,
                ExpireAt = DateTime.UtcNow.AddDays(30),
                NumberOfViews = 0
            };

            await _advertisementService.AddAsync(advertisementDto, cancellation);

            return Created("1", new { });
        }
    }
}
