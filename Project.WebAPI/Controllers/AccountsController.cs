using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Project.Business.Abstract;
using Project.Core.Business;
using Project.Core.Business.BusinessResultObjects.Enums;
using Project.Entities.Entities;
using Project.WebAPI.Dtos;
using Project.WebAPI.Filters;
using Project.WebAPI.Helpers;

namespace Project.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IAuthService _authService;


        //DI
        public AccountsController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailService emailService, IAuthService authService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _authService = authService;
        }



        [HttpPost]
        [ValidationFilter]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users.IgnoreQueryFilters().FirstOrDefaultAsync(x=>x.Email == loginDto.Email);
            if (user != null)
            {
                
                if (user.IsDeleted == true)
                {
                    return BadRequest("Hesabınız Engellendi.");
                }


                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

                if (!result.Succeeded)
                {

                    if (!await _userManager.CheckPasswordAsync(user,loginDto.Password))
                    {
                        return BadRequest("Hatalı Şifre.");
                    }
                    
                    return BadRequest("Giriş Yapabilmek İçin Email Adresinizi Onaylamanız Gerekmektedir.");

                }

                //TOKENI OLUŞTUR
                var token =  _authService.GetAuthenticatedToken(user).Result;
                return Ok(token.Data);

            }

            return BadRequest("Hesap Bulunamadı.");
        }




        [HttpPost]
        [ValidationFilter]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var user = new User()
            {
                Name = registerDto.Name,
                Lastname = registerDto.Lastname,
                Gender = registerDto.Gender,
                BirthDay = registerDto.BirthDay,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            var registerResult = await _userManager.CreateAsync(user, registerDto.Password);

            if (registerResult.Succeeded)
            {
                var confirmEmailCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = UrlBuilder.GenerateEmailConfirmationLink(user.Id, HttpUtility.UrlEncode(confirmEmailCode));
                var emailResult = _emailService.SendConfirmAccountEmail(user.Email, callbackUrl);

                if (emailResult.ResultType == ResultType.Success)
                {
                    return Ok("Hesabınız oluşturuldu. Giriş yapabilmek için hesabınızı doğrulayınız.");
                }

                return BadRequest("Email gönderilemedi.");
            }

            return BadRequest(registerResult.Errors.FirstOrDefault().Description);
        }





        [HttpGet]
        public async Task<IActionResult> ConfirmEmail([FromQuery] long userId, [FromQuery] string code)
        {

            if (code == null)
            {
                return BadRequest("Hatalı Kod.");
            }


            var user = await _userManager.Users.FirstOrDefaultAsync(x=>x.Id == userId);


            if (user == null)
            {
                return BadRequest("Kullanıcı Bulunamadı.");
            }

            var confirmEmailResult = await _userManager.ConfirmEmailAsync(user, HttpUtility.HtmlDecode(code));

            if (confirmEmailResult.Succeeded)
            {
                return Ok("Email adresiniz başarıyla doğrulandı.");
            }

            return BadRequest(confirmEmailResult.Errors.FirstOrDefault().Description);


        }





        [HttpPost]
        [ValidationFilter]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);

            if (user == null)
            {
                return BadRequest("Kullanıcı Bulunamadı.");
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                return BadRequest("Lütfen Mail Adresinizi Doğrulayınız.");
            }


            string passwordResetToken = HttpUtility.UrlEncode(await _userManager.GeneratePasswordResetTokenAsync(user));
            string callbackUrl = UrlBuilder.GenerateResetPasswordLink(user.Id, passwordResetToken);
            
            var emailResult = _emailService.SendPasswordResetEmail(user.Email, callbackUrl);

            if (emailResult.ResultType == ResultType.Success)
            {
                return Ok("Email Adresinize Gönderilen Linkten Şifrenizi Sıfırlayabilirsiniz.");
            }

            return BadRequest("Email Gönderilirken Bir Hata Oluştu.");


        }





        [HttpGet]
        public async Task<IActionResult> VerifyResetPasswordToken(long userId, string code)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                return BadRequest("Kullanıcı Bulunamadı. Hatalı Password Token.");
            }

            if (!await this._userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", code))
            {
                return BadRequest("Geçersiz Password Token.");
            }

            return Ok();


        }




        [HttpPost]
        [ValidationFilter]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == resetPasswordDto.UserId);

            if (user == null)
            {
                return BadRequest("Kullanıcı Bulunamadı.");
            }

            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, HttpUtility.UrlDecode(resetPasswordDto.ResetPasswordToken), resetPasswordDto.Password);
            

            if (resetPasswordResult.Succeeded)
            {
                return Ok("Şifreniz Başarıyla Sıfırlandı.");
            }

            return BadRequest(resetPasswordResult.Errors.FirstOrDefault().Description);
        }

        

    }
}
