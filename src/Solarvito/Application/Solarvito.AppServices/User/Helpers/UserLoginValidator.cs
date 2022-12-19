using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Solarvito.Contracts.User;

namespace Solarvito.AppServices.User.Validators
{
    /// <summary>
    /// Валидатор данных для <see cref="UserLoginDto"/>
    /// </summary>
    public class UserLoginValidator : AbstractValidator<UserLoginDto>
    {
        /// <summary>
        /// Правила для валидации <see cref="UserLoginDto"/>
        /// </summary>
        public UserLoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Электронная почта обязательна для заполнения.")
                .EmailAddress().WithMessage("Некорректный формат электронный почты.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Пароль обязателен для заполнения.")
                .Matches(@"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)").WithMessage("Пароль должен содержать цифры, латинские заглавные и строчные буквы.")
                .MinimumLength(8).WithMessage("Пароль должен состоять минимум из 8 символов.")
                .MaximumLength(100).WithMessage("Пароль должен состоять максимум из 100 символов.");
        }
    }
}
