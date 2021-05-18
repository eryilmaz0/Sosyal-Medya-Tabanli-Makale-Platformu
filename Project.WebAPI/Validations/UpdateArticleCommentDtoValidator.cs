using FluentValidation;
using Project.Entities.Dtos.PostDtos;

namespace Project.WebAPI.Validations
{
    public class UpdateArticleCommentDtoValidator : AbstractValidator<UpdateArticleCommentDto>
    {
        public UpdateArticleCommentDtoValidator()
        {
            RuleFor(x => x.ArticleCommentId).NotNull().GreaterThan(0).WithMessage("Yorum ID Boş Olamaz.");
            RuleFor(x => x.Comment).NotNull().WithMessage("Yorum İçeriği Boş Olamaz.").MinimumLength(1)
                .WithMessage("Yorum Bilgisi Giriniz.");
        }
    }
}