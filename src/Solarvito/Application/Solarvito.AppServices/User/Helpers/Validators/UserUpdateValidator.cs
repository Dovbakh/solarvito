using FluentValidation;
using Solarvito.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.AppServices.User.Helpers.Validators
{
    /// <summary>
    /// Валидатор данных для <see cref="UserUpdateRequestDto"/>
    /// </summary>
    public class UserUpdateValidator : AbstractValidator<UserUpdateRequestDto>
    {
        /// <summary>
        /// Правила для валидации <see cref="UserUpdateRequestDto"/>
        /// </summary>
        public UserUpdateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название обьявления обязательно для заполнения.")
                .Matches(@"([A-ZА-Я0-9]([a-zA-Z0-9а-яА-Я]|[- @\.#&!№;%:?*()_])*)").WithMessage("Неправильный формат названия обьявления.");

            RuleFor(x => x.Phone)
                        .Matches(@"[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}").WithMessage("Неверный формат номера телефона.")
                        .NotEmpty().WithMessage("Номер телефона обязателен для заполнения.");

            RuleFor(x => x.Address)
                        .NotEmpty().WithMessage("Адрес обязателен для заполнения.");
        }
    }




    
}
