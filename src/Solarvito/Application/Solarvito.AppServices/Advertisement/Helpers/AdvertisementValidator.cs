using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Solarvito.Contracts.Advertisement;
using Solarvito.Contracts.User;
using SixLabors.ImageSharp;
using Microsoft.IdentityModel.Tokens;
using static System.Net.Mime.MediaTypeNames;
using Image = SixLabors.ImageSharp.Image;
using SixLabors.ImageSharp.Formats;

namespace Solarvito.AppServices.Advertisement.Validators
{
    /// <summary>
    /// Валидатор данных для <see cref="AdvertisementRequestDto"/>
    /// </summary>
    public class AdvertisementValidator : AbstractValidator<AdvertisementRequestDto>
    {
        /// <summary>
        /// Правила для валидации <see cref="AdvertisementRequestDto"/>
        /// </summary>
        public AdvertisementValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя обязательно для заполнения.")
                .Matches(@"([A-ZА-Я0-9]([a-zA-Z0-9а-яА-Я]|[- @\.#&!№;%:?*()_])*)").WithMessage("Неправильный формат названия обьявления.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Описание обязательно для заполнения.");

            RuleFor(x => x.Phone)
                .Matches(@"[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}").WithMessage("Неверный формат номера телефона.")
                .NotEmpty().WithMessage("Номер телефона обязателен для заполнения.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Адрес обязателен для заполнения.");
     
            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Цена обязательна для заполнения.")
                .GreaterThanOrEqualTo(0).WithMessage("Цена не может быть отрицательной.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Имя обязательно для заполнения.")
                .Matches(@"([А-Я]{1}[а-яё]{1,23})").WithMessage("Неправильный формат имени");

            RuleFor(x => x.Images)
                .NotEmpty().WithMessage("Прикрепите хотя бы одно изображение.")
                .Must(x => x.All(x => x.Length < 25 * 1024 * 1024)).WithMessage("Размер файла должен быть меньше 25 Мб.")
                .Must(x => x.All(x => x.ContentType == "image/png" || x.ContentType == "image/jpeg")).WithMessage("Неподдерживаемый формат изображения.")
                .Must(x => x.All(x => {                   
                    using var stream = x.OpenReadStream();

                    var imageInfo = Image.Identify(stream);
                    if (imageInfo == null)
                    {
                        throw new ValidationException("Неподдерживаемый формат изображения.");
                    }
                    if (imageInfo.Width < 600) return false;
                    if (imageInfo.Height < 300) return false;

                    stream.Position = 0;
                    var imageFormat = Image.DetectFormat(stream);
                    if (imageFormat.DefaultMimeType != "image/png" && imageFormat.DefaultMimeType != "image/jpeg") return false;
                    return true;
                })).WithMessage("Размер изображения должен быть минимум 600х300.");
                

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Категория обязательна к выбору.");

        }
    }
}
