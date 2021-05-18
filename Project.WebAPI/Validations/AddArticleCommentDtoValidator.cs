using FluentValidation;
using Project.Entities.Dtos.PostDtos;

namespace Project.WebAPI.Validations
{
    public class AddArticleCommentDtoValidator : AbstractValidator<AddArticleCommentDto>
    {
        public AddArticleCommentDtoValidator()
        {
            RuleFor(x => x.Comment).NotNull().WithMessage("Yorum Alanı Boş Olamaz.").MinimumLength(1)
                .WithMessage("Lütfen Yorum Bilgisi Giriniz.");


            RuleFor(x => x.ArticleId).NotNull().WithMessage("Makale ID Boş Olamaz.").GreaterThan(0)
                .WithMessage("Makale ID 0 Olamaz.");
        }
    }
}