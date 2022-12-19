using FluentValidation;
using Solarvito.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.AppServices.User.Helpers
{

    public class UserEmailValidator : AbstractValidator<UserEmailDto>
    {
        public UserEmailValidator()
        {

        }
    }
}
