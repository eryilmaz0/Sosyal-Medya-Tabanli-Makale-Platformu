using FluentValidation;
using Project.Entities.Dtos.PostDtos;

namespace Project.WebAPI.Validations
{
    public class UpdateArticleCoverPhotoDtoValidator : AbstractValidator<UpdateArticleCoverPhotoDto>
    {
        public UpdateArticleCoverPhotoDtoValidator()
        {
            RuleFor(x => x.CoverPhoto).NotNull().WithMessage("Kapak Resmi Boş Olamaz.").MinimumLength(1)
                .WithMessage("Kapak Resmi Seçiniz.");

            RuleFor(x => x.ArticleId).NotNull().GreaterThan(0).WithMessage("Makale ID Boş Olamaz.");
        }
    }
}