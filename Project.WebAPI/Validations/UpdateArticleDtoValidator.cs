using FluentValidation;
using Project.Entities.Dtos.PostDtos;

namespace Project.WebAPI.Validations
{
    public class UpdateArticleDtoValidator : AbstractValidator<UpdateArticleDto>
    {
        public UpdateArticleDtoValidator()
        {
            RuleFor(x => x.ArticleId).NotNull().GreaterThan(0).WithMessage("Makale ID 0 Olamaz.");

            RuleFor(x => x.Title).NotNull().WithMessage("Lütfen Makale Başlığı Giriniz.").Length(1, 150)
                .WithMessage("Makale Başlığı 1-150 Karakter Olmalı.");

            RuleFor(x => x.Content).NotNull().WithMessage("Makale İçeriği Boş Olamaz.").MinimumLength(1)
                .WithMessage("Lütfen Makale İçeriği Giriniz.");

            RuleFor(x => x.ArticleCategoryId).NotNull().GreaterThan(0)
                .WithMessage("Makalenin Bulunacağı Kategori Bilgisi Giriniz.");
        }
    }
}