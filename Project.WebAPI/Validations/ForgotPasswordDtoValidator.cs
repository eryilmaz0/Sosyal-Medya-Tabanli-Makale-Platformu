using FluentValidation;
using Project.WebAPI.Dtos;

namespace Project.WebAPI.Validations
{
    public class ForgotPasswordDtoValidator : AbstractValidator<ForgotPasswordDto>
    {
        public ForgotPasswordDtoValidator()
        {
            RuleFor(x => x.Email).NotNull().WithMessage("Email Boş Olamaz.").EmailAddress()
                .WithMessage("Email adresi uygun değil.").MinimumLength(1).WithMessage("Email Bilgisi Giriniz.");
        }   
    }
}