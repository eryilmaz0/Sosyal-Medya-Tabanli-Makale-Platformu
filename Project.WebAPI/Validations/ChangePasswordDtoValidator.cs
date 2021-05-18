using FluentValidation;
using Project.WebAPI.Dtos;

namespace Project.WebAPI.Validations
{
    public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordDtoValidator()
        {
            RuleFor(x => x.OldPassword).NotNull().WithMessage("Eski şifrenizi giriniz.").MinimumLength(1)
                .WithMessage("Eski Şifrenizi Giriniz.");

            RuleFor(x => x.NewPassword).NotNull().WithMessage("Yeni şifrenizi giriniz.").MinimumLength(1)
                .WithMessage("Yeni Şifrenizi Giriniz.");

            RuleFor(x => x.NewPasswordConfirm).NotNull().WithMessage("Yeni Şifrenizi Tekrar Giriniz.")
                .MinimumLength(1).WithMessage("Yeni Şifrenizi Tekrar Giriniz.").When(x => x.NewPassword != null);



            When(x => x.NewPassword != null && x.NewPasswordConfirm != null, () =>
            {
                RuleFor(x => x).Must(ValidatePasswordFields).WithMessage("Şifreler uyuşmuyor.");
            });
        }


        private bool ValidatePasswordFields(ChangePasswordDto changePasswordDto)
        {
            return (changePasswordDto.NewPasswordConfirm == changePasswordDto.NewPassword);
        }
    }
}