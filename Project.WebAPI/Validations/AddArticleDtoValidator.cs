using FluentValidation;
using Project.Entities.Dtos;
using Project.Entities.Dtos.PostDtos;

namespace Project.WebAPI.Validations
{
    public class AddArticleDtoValidator : AbstractValidator<AddArticleDto>
    {

        public AddArticleDtoValidator()
        {
            RuleFor(x => x.Title).NotNull().WithMessage("Lütfen Maakale Başlığı Giriniz.").Length(1, 150)
                .WithMessage("Makale Başlığı 1-150 Karakter Olmalıdır.");

            RuleFor(x => x.Content).NotNull().WithMessage("Makale İçeriği Boş Olamaz.").MinimumLength(1)
                .WithMessage("Lütfen Makale İçeriği Giriniz.");

            RuleFor(x => x.ArticleCategoryId).NotNull().GreaterThan(0).WithMessage("Makale Kategorisi Giriniz.");
        }

    }
}