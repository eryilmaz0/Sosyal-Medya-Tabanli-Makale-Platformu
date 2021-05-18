using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Project.Entities.Entities;
using System.Threading.Tasks;

namespace Project.WebAPI.IdentityCustomValidators
{
    public class IdentityPasswordValidator : IPasswordValidator<User>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string password)
        {
            List<IdentityError> ValidationErrors = new List<IdentityError>();


            if (password.ToLower().Contains(user.Name.ToLower()))
            {
                ValidationErrors.Add(new IdentityError(){Code = "PasswordContainsUsername", Description = "Şifre İsminizi İçeremez."});
            }


            if (password.ToLower().Contains(user.Lastname.ToLower()))
            {
                ValidationErrors.Add(new IdentityError() { Code = "PasswordContainsUserLastname", Description = "Şifre Soyadınızı İçeremez." });
            }


            if (password.ToLower().Contains(user.Email.ToLower()))
            {
                ValidationErrors.Add(new IdentityError() { Code = "PasswordContainsEmail", Description = "Şifre Mail Adresinizi İçeremez." });
            }



            if (!ValidationErrors.Any())
                return Task.FromResult(IdentityResult.Success);

            else
                return Task.FromResult(IdentityResult.Failed(ValidationErrors.ToArray()));
        }
    }
}