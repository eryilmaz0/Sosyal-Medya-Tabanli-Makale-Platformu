using FluentValidation;
using Project.Entities.Dtos.PostDtos;

namespace Project.WebAPI.Validations
{
    public class AddFavoriteDtoValidator : AbstractValidator<AddFavoriteDto>
    {
        public AddFavoriteDtoValidator()
        {
            RuleFor(x => x.ArticleId).NotNull().WithMessage("Makale ID Boş Olamaz.").GreaterThan(0)
                .WithMessage("Geçerli Makale ID Giriniz.");
        }
    }
}