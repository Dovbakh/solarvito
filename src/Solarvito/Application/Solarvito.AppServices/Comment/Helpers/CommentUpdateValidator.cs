using FluentValidation;
using Solarvito.Contracts.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.AppServices.Comment.Helpers
{
    /// <summary>
    /// Валидатор данных для <see cref="CommentUpdateRequestDto"/>
    /// </summary>
    public class CommentUpdateValidator : AbstractValidator<CommentUpdateRequestDto>
    {
        /// <summary>
        /// Валидатор данных для <see cref="CommentUpdateRequestDto"/>
        /// </summary>
        public CommentUpdateValidator()
        {
            RuleFor(c => c.Text)
                .NotEmpty().WithMessage("Введите текст комментария к отзыву.")
                .MaximumLength(1000).WithMessage("Размер текста не должен превышать 1000 символов.");
            RuleFor(c => c.Rating)
                .NotEmpty().WithMessage("Укажите оценку к отзыву.")
                .InclusiveBetween(1, 5).WithMessage("Оценка должна быть от 1 до 5.");
        }
    }
}
