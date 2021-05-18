using FluentValidation;
using Project.WebAPI.Dtos;

namespace Project.WebAPI.Validations
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("isim Boş Olamaz.").Length(3, 30)
                .WithMessage("İsim 3-30 karakter olmalıdır.");


            RuleFor(x => x.Lastname).NotNull().WithMessage("Soyisim bilgisi giriniz.").Length(2, 30)
                .WithMessage("Soyisim 2-30 karakter olmalıdır.");


            RuleFor(x => x.Gender).NotNull().WithMessage("Cinsiyet bilgisi seçiniz.").IsInEnum()
                .WithMessage("Cinsiyet bilgisi seçiniz.");


            RuleFor(x => x.BirthDay).NotNull().WithMessage("Doğum tarihi bilgisi giriniz.");


            RuleFor(x => x.Email).NotNull().WithMessage("Email Boş Olamaz.").EmailAddress()
                .WithMessage("Email adresi uygun değil.").MinimumLength(1).WithMessage("Email Bilgisi Giriniz.");


            RuleFor(x => x.Password).NotNull().WithMessage("Şifre bilgisi giriniz.").MinimumLength(1)
                .WithMessage("Şifre Bilgisi Giriniz.");


            RuleFor(x => x.PasswordConfirm).NotNull().WithMessage("Şifrenizi tekrar giriniz.")
                .MinimumLength(1).WithMessage("Şifrenizi Tekrar Giriniz.").When(x=>x.Password != null);

            
            When(x => x.Password != null && x.PasswordConfirm != null, () =>
            {
                RuleFor(x => x).Must(ValidatePasswordFields).WithMessage("Şifreler uyuşmuyor.");
            });




        }


        private bool ValidatePasswordFields(RegisterDto registerDto)
        {
            return (registerDto.PasswordConfirm == registerDto.Password);
        }
    }
}