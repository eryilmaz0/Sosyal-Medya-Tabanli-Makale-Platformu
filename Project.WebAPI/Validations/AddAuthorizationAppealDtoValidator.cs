using FluentValidation;
using Project.Entities.Dtos.PostDtos;

namespace Project.WebAPI.Validations
{
    public class AddAuthorizationAppealDtoValidator : AbstractValidator<AddAuthorizationAppealDto>
    {
        public AddAuthorizationAppealDtoValidator()
        {
            RuleFor(x => x.AppealDescription).NotNull().WithMessage("Başvuru Açıklaması Boş Olamaz.").MinimumLength(1)
                .WithMessage("Lütfen Başvuru Açıklaması Giriniz.");
        }
    }
}