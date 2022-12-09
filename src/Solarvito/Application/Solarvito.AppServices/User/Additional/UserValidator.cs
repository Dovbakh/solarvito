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
    public class UserValidator : AbstractValidator<UserCredsDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Электронная почта обязательна для заполнения.")
                .EmailAddress().WithMessage("Некорректный формат электронный почты.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Пароль обязателен для заполнения.")
                .Matches(@"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)").WithMessage("Пароль должен содержать цифры, латинские заглавные и строчные буквы.")
                .MinimumLength(8).WithMessage("Пароль должен состоять минимум из 8 символов.")
                .MaximumLength(100).WithMessage("Пароль должен состоять максимум из 100 символов.");
            RuleFor(x => x.PasswordConfirm)
                .Equal(x => x.Password).WithMessage("Пароли должны совпадать.");
        }
    }
}
