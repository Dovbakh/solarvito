using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Solarvito.Contracts.Advertisement;
using Solarvito.Contracts.User;

namespace Solarvito.AppServices.Advertisement.Validators
{
    public class AdvertisementValidator : AbstractValidator<AdvertisementRequestDto>
    {
        public AdvertisementValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя обязательно для заполнения.")
                .Matches(@"([А-Я]{1}[а-яё]{1,23})").WithMessage("Неправильный формат имени");

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

            RuleFor(x => x.ImagePath)
                .NotEmpty().WithMessage("Фото обязательно для заполнения.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Категория обязательна к выбору.");

        }
    }
}
