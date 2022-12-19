using FluentValidation;
using Solarvito.Contracts.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.AppServices.Comment.Helpers
{
    public class CommentValidator : AbstractValidator<CommentRequestDto>
    {
        public CommentValidator()
        {
            RuleFor(c => c.Text)
                .NotEmpty().WithMessage("Введите текст комментария к отзыву.")
                .MaximumLength(1000).WithMessage("Размер текста не должен превышать 1000 символов.");
            RuleFor(c => c.Rating)
                .NotEmpty().WithMessage("Укажите оценку к отзыву.")
                .InclusiveBetween(1, 5).WithMessage("Оценка должна быть от 1 до 5.");
            RuleFor(c => c.AuthorId)
                .NotEmpty().WithMessage("Укажите автора отзыва.");
            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("Укажите кому предназначается отзыв.");
            RuleFor(c => c.AdvertisementId)
                .NotEmpty().WithMessage("Укажите к какому обьявлению относится отзыв.");
        }
    }
}
