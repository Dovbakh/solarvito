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
    public static class Mapper
    {
        public static AdvertisementResponseDto MapToDto(this Domain.Advertisement advertisement) => new()
        {
            Id = advertisement.Id,
            Name = advertisement.Name,
            Description = advertisement.Description,
            Price = advertisement.Price,
            Address = advertisement.Address,
            Phone = advertisement.Phone,
            ImagePath = advertisement.ImagePath,
            CreatedAt = advertisement.CreatedAt,
            ExpireAt = advertisement.ExpireAt,
            NumberOfViews = advertisement.NumberOfViews,
            UserName = advertisement.User.Name,
            UserId = advertisement.UserId,
            CategoryName = advertisement.Category.Name,
            CategoryId = advertisement.CategoryId                        
        };

        public static Domain.Advertisement MapToEntity(this AdvertisementRequestDto advertisementRequestDto) => new()
        {
            Name = advertisementRequestDto.Name,
            Description = advertisementRequestDto.Description,
            Price = advertisementRequestDto.Price,
            Address = advertisementRequestDto.Address,
            Phone = advertisementRequestDto.Phone,
            ImagePath = advertisementRequestDto.ImagePath,
            CreatedAt = DateTime.UtcNow,
            ExpireAt = DateTime.UtcNow.AddDays(30),
            NumberOfViews = 0,
            CategoryId = advertisementRequestDto.CategoryId,
            UserId = advertisementRequestDto.UserId
        };

        public static Domain.Category MapToEntity(this CategoryDto categoryDto) => new()
        {
            Name = categoryDto.Name,
            ParentId = categoryDto.ParentId
        };

        public static CategoryDto MapToDto(this Domain.Category category) => new()
        {
            Id = category.Id,
            Name = category.Name,
            ParentId = category.ParentId
        };
    }
}
