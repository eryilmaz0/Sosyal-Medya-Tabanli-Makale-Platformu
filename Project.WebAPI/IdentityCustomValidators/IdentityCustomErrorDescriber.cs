using Microsoft.AspNetCore.Identity;

namespace Project.WebAPI.IdentityCustomValidators
{
    public class IdentityCustomErrorDescriber : IdentityErrorDescriber
    {

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError() { Code = "DuplicateEmail", Description = "Bu Email Kullanılıyor." };
        }



        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError() { Code = "PasswordTooShort", Description = $"Şifre En Az {length} Karakter Olmalı." };
        }



        public override IdentityError InvalidToken()
        {
            return new IdentityError() { Code = "İnvalidToken", Description = "Geçersiz Kod." };
        }



        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError() { Code = "İnvalidEMail", Description = "Geçersiz Email." };
        }



        public override IdentityError UserAlreadyInRole(string role)
        {
            return new IdentityError() { Code = "UserAlreadyInRole", Description = "Kullanıcı Zaten Bu Role Sahip." };
        }


        //HİÇ BİR ZAMAN OLAMAZ
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError() { Code = "DuplicateUserName", Description = "Bu Email Kullanılıyor." };
        }

        public override IdentityError PasswordMismatch()
        {
            return new IdentityError() { Code = "IncorrectPassword", Description = "Eski Şifreniz Yanlış." };
        }
    }
}