using FluentValidation;
using Project.WebAPI.Dtos;

namespace Project.WebAPI.Validations
{
    public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordDtoValidator()
        {
            RuleFor(x => x.Password).NotNull().WithMessage("Şifre bilgisi giriniz.").MinimumLength(1)
                .WithMessage("Şifre Bilgisi Giriniz.");


            RuleFor(x => x.PasswordConfirm).NotNull().WithMessage("Şifrenizi tekrar giriniz.").MinimumLength(1).WithName("Şifrenizi Tekrar Giriniz.").When(x => x.Password != null);


            RuleFor(x => x.UserId).NotNull().WithMessage("UserId boş olamaz.");


            RuleFor(x => x.ResetPasswordToken).NotNull().WithMessage("Password reset token boş olamaz.").MinimumLength(1).WithMessage("Password Reset Token Giriniz.");


            When(x => x.Password != null && x.PasswordConfirm != null, () =>
            {
                RuleFor(x => x).Must(ValidatePasswordFields).WithMessage("Şifreler uyuşmuyor.");
            });
        }


        private bool ValidatePasswordFields(ResetPasswordDto resetPasswordDto)
        {
            return (resetPasswordDto.PasswordConfirm == resetPasswordDto.Password);
        }
    }
}