using Solarvito.Contracts.Advertisement;
using Solarvito.Contracts.AdvertisementImage;
using Solarvito.Contracts.Category;
using Solarvito.Contracts.Comment;
using Solarvito.Contracts.User;
using Solarvito.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            UserName = advertisement.UserName,
            UserId = advertisement.UserId,
            CategoryName = advertisement.Category.Name,
            CategoryId = advertisement.CategoryId
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


        public static Domain.Advertisement MapToEntity(this AdvertisementRequestDto advertisementRequestDto, Domain.Advertisement advertisement) => new()
        {
            Id = advertisement.Id,
            Name = advertisementRequestDto.Name,
            Description = advertisementRequestDto.Description,
            Price = advertisementRequestDto.Price,
            Address = advertisementRequestDto.Address,
            Phone = advertisementRequestDto.Phone,
            UserName = advertisementRequestDto.UserName,
            CreatedAt = advertisement.CreatedAt,
            ExpireAt = advertisement.ExpireAt,
            NumberOfViews = advertisement.NumberOfViews,
            CategoryId = advertisementRequestDto.CategoryId,
            UserId = advertisement.UserId
        };

        public static Domain.Advertisement MapToEntity(this AdvertisementDto advertisementDto) => new()
        {
            Id = advertisementDto.Id,
            Name = advertisementDto.Name,
            Description = advertisementDto.Description,
            Price = advertisementDto.Price,
            Address = advertisementDto.Address,
            Phone = advertisementDto.Phone,
            UserName = advertisementDto.UserName,
            CreatedAt = advertisementDto.CreatedAt,
            ExpireAt = advertisementDto.ExpireAt,
            NumberOfViews = advertisementDto.NumberOfViews,
            CategoryId = advertisementDto.CategoryId,
            UserId = advertisementDto.UserId
        };

        public static AdvertisementDto MapToDto(this AdvertisementRequestDto advertisementRequestDto, int advertisementId) => new()
        {
            Id = advertisementId,
            Name = advertisementRequestDto.Name,
            Description = advertisementRequestDto.Description,
            Price = advertisementRequestDto.Price,
            Address = advertisementRequestDto.Address,
            Phone = advertisementRequestDto.Phone,
            UserName = advertisementRequestDto.UserName,
            CategoryId = advertisementRequestDto.CategoryId
        };

        public static AdvertisementDto MapToDto(this AdvertisementUpdateRequestDto advertisementUpdateRequestDto, int advertisementId) => new()
        {
            Id = advertisementId,
            Name = advertisementUpdateRequestDto.Name,
            Description = advertisementUpdateRequestDto.Description,
            Price = advertisementUpdateRequestDto.Price,
            Address = advertisementUpdateRequestDto.Address,
            Phone = advertisementUpdateRequestDto.Phone,
            UserName = advertisementUpdateRequestDto.UserName,
            CategoryId = advertisementUpdateRequestDto.CategoryId
        };

        public static AdvertisementDto MapToDto(this AdvertisementRequestDto advertisementRequestDto) => new()
        {
            Name = advertisementRequestDto.Name,
            Description = advertisementRequestDto.Description,
            Price = advertisementRequestDto.Price,
            Address = advertisementRequestDto.Address,
            Phone = advertisementRequestDto.Phone,
            UserName = advertisementRequestDto.UserName,
            CategoryId = advertisementRequestDto.CategoryId
        };

        public static AdvertisementDto MapToDto(this AdvertisementResponseDto advertisementResponseDto) => new()
        {
            Id = advertisementResponseDto.Id,
            Name = advertisementResponseDto.Name,
            Description = advertisementResponseDto.Description,
            Price = advertisementResponseDto.Price,
            Address = advertisementResponseDto.Address,
            Phone = advertisementResponseDto.Phone,
            UserName = advertisementResponseDto.UserName,
            CreatedAt = advertisementResponseDto.CreatedAt,
            ExpireAt = advertisementResponseDto.ExpireAt,
            NumberOfViews = advertisementResponseDto.NumberOfViews,
            CategoryId = advertisementResponseDto.CategoryId,
            UserId = advertisementResponseDto.UserId
        };

        public static Domain.AdvertisementImage MapToEntity(this AdvertisementImageDto advertisementImageDto) => new()
        {
            Id = advertisementImageDto.Id,
            FileName = advertisementImageDto.FileName,
            AdvertisementId = advertisementImageDto.AdvertisementId
        };

        public static AdvertisementImageDto MapToDto(this Domain.AdvertisementImage advertisementImage) => new()
        {
            Id = advertisementImage.Id,
            FileName = advertisementImage.FileName,
            AdvertisementId = advertisementImage.AdvertisementId
        };

        public static CommentDto MapToDto(this Domain.Comment comment) => new()
        {
            Id = comment.Id,
            Text = comment.Text,
            Rating = comment.Rating,
            CreatedAt = comment.CreatedAt,
            UserId = comment.UserId,
            AuthorId = comment.AuthorId,
            AdvertisementId = comment.AdvertisementId,
            UserName = comment.User.Name,
            AuthorName = comment.Author.Name,
            AdvertisementName = comment.Advertisement.Name
        };

        public static Domain.Comment MapToEntity(this CommentDto commentDto) => new()
        {
            Id = commentDto.Id,
            Text = commentDto.Text,
            Rating = commentDto.Rating,
            CreatedAt = commentDto.CreatedAt,
            UserId = commentDto.UserId,
            AuthorId = commentDto.AuthorId,
            AdvertisementId = commentDto.AdvertisementId
        };

        public static CommentDto MapToDto(this CommentRequestDto сommentRequestDto) => new()
        {
            Text = сommentRequestDto.Text,
            Rating = сommentRequestDto.Rating,
            AuthorId = сommentRequestDto.AuthorId,
            UserId = сommentRequestDto.UserId,
            AdvertisementId = сommentRequestDto.AdvertisementId
        };

        public static CommentDto MapToDto(this CommentUpdateRequestDto commentUpdateRequestDto) => new()
        {
            Text = commentUpdateRequestDto.Text,
            Rating = commentUpdateRequestDto.Rating
        };

        public static UserDto MapToDto(this Domain.User user) => new()
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            Phone = user.PhoneNumber,
            Address = user.Address,
            Rating = user.CommentsFor.Count != 0 ? (float)user.CommentsFor.Sum(u => u.Rating) / (float)user.CommentsFor.Count : 0,
            CommentsCount = user.CommentsFor.Count,
            AdvertisementCount = user.Advertisements.Count,
            CreatedAt = user.CreatedAt,
            RoleName = user.Role.Name,
            RoleId = user.RoleId
        };

        public static Domain.User MapToEntity(this UserDto userDto) => new()
        {
            Id = userDto.Id,
            Email = userDto.Email,
            Name = userDto.Name,
            PhoneNumber = userDto.Phone,
            Address = userDto.Address,
            CreatedAt = userDto.CreatedAt,
            RoleId = userDto.RoleId
        };

        public static Domain.User MapToEntity(this UserRegisterDto userRegisterDto) => new()
        {
            Email = userRegisterDto.Email,
            UserName = userRegisterDto.Email,
            CreatedAt = DateTime.UtcNow,
            RoleId = "1"
        };
    }
}
