using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Project.Entities.Entities;
using System.Threading.Tasks;

namespace Project.WebAPI.IdentityCustomValidators
{
    public class IdentityUserValidator : IUserValidator<User>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
        {
            List<IdentityError> ValidationErrors = new List<IdentityError>();

            if (user.Name.Any(char.IsDigit))
            {
                ValidationErrors.Add(new IdentityError(){Code = "NameContainsDigit", Description = "İsim Alanında Sayı Bulunamaz."});
            }

            if (user.Name.Length > 75)
            {
                ValidationErrors.Add(new IdentityError() { Code = "NameTooLong", Description = "İsim Alanı 75 Karakteri Geçemez." });
            }


            if (!ValidationErrors.Any())
                return Task.FromResult(IdentityResult.Success);
            else
                return Task.FromResult(IdentityResult.Failed(ValidationErrors.ToArray()));

        }
    }
}