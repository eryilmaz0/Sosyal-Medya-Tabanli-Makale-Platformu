using FluentValidation;
using Project.WebAPI.Dtos;

namespace Project.WebAPI.Validations
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email).NotNull().WithMessage("Email adresini giriniz.").Length(10, 50)
                .WithMessage("Email adresi 10-50 karakter uzunluğunda olmalıdır.").EmailAddress().WithMessage("Email adresi uygun değil.");

            RuleFor(x => x.Password).NotNull().WithName("Şifre bilgisi giriniz.").MinimumLength(6)
                .WithMessage("Şifre en az 6 karakter olmalıdır.");
        }
    }
}