﻿//using FluentValidation;
//using Solarvito.Contracts.User;
//using Solarvito.Contracts.User.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Solarvito.AppServices.User.Helpers
//{
//    public class UserCredentialsValidator
//    {
//        /// <summary>
//        /// Валидатор данных для <see cref="UserLoginDto"/>
//        /// </summary>
//        public class UserPasswordValidator : AbstractValidator<ICredentials>
//        {
//            /// <summary>
//            /// Правила для валидации <see cref="UserLoginDto"/>
//            /// </summary>
//            public UserPasswordValidator()
//            {
//                RuleFor(x => x.Password)
//                    .NotEmpty().WithMessage("Пароль обязателен для заполнения.")
//                    .Matches(@"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)").WithMessage("Пароль должен содержать цифры, латинские заглавные и строчные буквы.")
//                    .MinimumLength(8).WithMessage("Пароль должен состоять минимум из 8 символов.")
//                    .MaximumLength(100).WithMessage("Пароль должен состоять максимум из 100 символов.");

//                RuleFor(x => x.NewPassword)
//                    .NotEqual(x => x.Password).WithMessage("Новый пароль не должен совпадать со старым.")
//                    .NotEmpty().WithMessage("Пароль обязателен для заполнения.")
//                    .Matches(@"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)").WithMessage("Пароль должен содержать цифры, латинские заглавные и строчные буквы.")
//                    .MinimumLength(8).WithMessage("Пароль должен состоять минимум из 8 символов.")
//                    .MaximumLength(100).WithMessage("Пароль должен состоять максимум из 100 символов.");
//            }
//        }
//    }
//}
