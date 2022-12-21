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
    /// Валидатор данных для <see cref="UserEmailDto"/>
    /// </summary>
    public class UserEmailValidator : AbstractValidator<UserEmailDto>
    {
        /// <summary>
        /// Валидатор данных для <see cref="UserEmailDto"/>
        /// </summary>
        public UserEmailValidator()
        {
            RuleFor(x => x.Value)
                .NotEmpty().WithMessage("Электронная почта обязательна для заполнения.")
                .EmailAddress().WithMessage("Некорректный формат электронный почты.");

        }
    }
}
