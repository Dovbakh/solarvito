using Solarvito.Contracts.Advertisement;
using Solarvito.Contracts.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.Contracts
{
    /// <summary>
    /// Ручной маппер.
    /// </summary>
    public static class Mapper
    {
        /// <param name="advertisement">Элемент <see cref="Domain.Advertisement"/>.</param>
        /// <returns>Элемент <see cref="AdvertisementResponseDto"/>.</returns>
        public static AdvertisementResponseDto MapToDto(this Domain.Advertisement advertisement) => new()
        {
            Id = advertisement.Id,
            Name = advertisement.Name,
            Description = advertisement.Description,
            Price = advertisement.Price,
            Address = advertisement.Address,
            Phone = advertisement.Phone,
            ImagePathes = advertisement.AdvertisementImages.Select(ai => ai.FileName).ToList(),
            CreatedAt = advertisement.CreatedAt,
            ExpireAt = advertisement.ExpireAt,
            NumberOfViews = advertisement.NumberOfViews,
            UserName = advertisement.User.Name,
            UserId = advertisement.UserId,
            CategoryName = advertisement.Category.Name,
            CategoryId = advertisement.CategoryId                        
        };

        /// <param name="advertisementRequestDto">Элемент <see cref="AdvertisementRequestDto"/>.</param>
        /// <returns>Элемент <see cref="Domain.Advertisement"/>.</returns>
        public static Domain.Advertisement MapToEntity(this AdvertisementRequestDto advertisementRequestDto) => new()
        {
            Name = advertisementRequestDto.Name,
            Description = advertisementRequestDto.Description,
            Price = advertisementRequestDto.Price,
            Address = advertisementRequestDto.Address,
            Phone = advertisementRequestDto.Phone,
            CreatedAt = DateTime.UtcNow,
            ExpireAt = DateTime.UtcNow.AddDays(30),
            NumberOfViews = 0,
            CategoryId = advertisementRequestDto.CategoryId,
            UserId = advertisementRequestDto.UserId
        };

        /// <param name="categoryDto">Элемент <see cref="CategoryDto"/>.</param>
        /// <returns>Элемент <see cref="Domain.Category"/>.</returns>
        public static Domain.Category MapToEntity(this CategoryDto categoryDto) => new()
        {
            Name = categoryDto.Name,
            ParentId = categoryDto.ParentId
        };

        /// <param name="category">Элемент <see cref="Domain.Category"/>.</param>
        /// <returns>Элемент <see cref="CategoryDto"/>.</returns>
        public static CategoryDto MapToDto(this Domain.Category category) => new()
        {
            Id = category.Id,
            Name = category.Name,
            ParentId = category.ParentId
        };
    }
}
