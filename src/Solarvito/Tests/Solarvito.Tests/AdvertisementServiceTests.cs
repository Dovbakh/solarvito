using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Solarvito.AppServices.Advertisement.Repositories;
using Solarvito.AppServices.Advertisement.Services;
using Solarvito.AppServices.AdvertisementImage.Repositories;
using Solarvito.AppServices.File.Services;
using Solarvito.AppServices.User.Services;
using Solarvito.Contracts.Advertisement;
using Solarvito.Contracts.User;
using Solarvito.DataAccess.EntityConfigurations.Advertisement;
using Solarvito.Infrastructure.Repository;

namespace Solarvito.Tests
{
    public class AdvertisementServiceTests
    {
        [Fact]
        public async Task AddAsync_int_Success()
        {
            // arrange
            var entry = new AdvertisementRequestDto() {
                Name = "Обьявление",
                Description = "Описание",
                Price = 100,
                Address = "Адрес",
                Phone = "+77777777777",
                UserName = "Имя",
                Images = new List<IFormFile>(),
                CategoryId = 1,
                UserId = 1
            };

            var validationResult = new ValidationResult()
            {
                
            };

            var userDto = new UserDto()
            {
                Email = "email@email.com",
                Name = "Имя",
                Phone = "+77777777777",
                Address = "Адрес",
                Rating = 3,
                CommentsCount = 3,
                AdvertisementCount = 3,
                CreatedAt = DateTime.UtcNow,
                RoleId = "1",
                RoleName = "user"
            };

            var cancellation = new CancellationToken(false);
            var advertisementRepositoryMock = new Mock<IAdvertisementRepository>();
            var userServiceMock = new Mock<IUserService>();
            var validatorMock = new Mock<IValidator<AdvertisementRequestDto>>();
            var validatorUpdateMock = new Mock<IValidator<AdvertisementUpdateRequestDto>>();
            var fileSerivceMock = new Mock<IFileService>();
            var advertisementImageRepositoryMock = new Mock<IAdvertisementImageRepository>();
            var loggerMock = new Mock<ILogger<AdvertisementService>>();


            AdvertisementService advertisementService = new AdvertisementService(advertisementRepositoryMock.Object, userServiceMock.Object, validatorMock.Object, validatorUpdateMock.Object, fileSerivceMock.Object, advertisementImageRepositoryMock.Object, loggerMock.Object);
            userServiceMock.Setup(userServiceMock => userServiceMock.GetCurrent(It.IsAny<CancellationToken>()).Result).Returns(userDto);
            validatorMock.Setup(validatorMock => validatorMock.Validate(It.IsAny<AdvertisementRequestDto>())).Returns(validationResult);

            // act
            var result = await advertisementService.AddAsync(entry, cancellation);


            // assert
            Assert.NotNull(result);           

        }

        [Fact]
        public async Task GetAll_int_Success()
        {

        }
    }
}