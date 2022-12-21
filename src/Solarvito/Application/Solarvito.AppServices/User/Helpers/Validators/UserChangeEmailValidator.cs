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
    /// Валидатор данных для <see cref="UserChangeEmailDto"/>
    /// </summary>
    public class UserChangeEmailValidator : AbstractValidator<UserChangeEmailDto>
    {
        /// <summary>
        /// Валидатор данных для <see cref="UserChangeEmailDto"/>
        /// </summary>
        public UserChangeEmailValidator()
        {
            RuleFor(x => x.newEmail)
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
