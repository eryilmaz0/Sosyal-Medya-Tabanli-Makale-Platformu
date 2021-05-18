using FluentValidation;
using Project.Entities.Dtos;
using Project.Entities.Dtos.PostDtos;

namespace Project.WebAPI.Validations
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.About).NotNull().WithMessage("Hakkında Alanı Boş Olamaz.").MinimumLength(1)
                .WithMessage("Lütfen Hakkında Bilgisi Giriniz.");
        }
    }
}