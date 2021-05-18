using System;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Project.Business.Abstract;
using Project.Business.Helpers;
using Project.Business.Messages;
using Project.Core.Business.BusinessResultObjects;
using Project.Core.Business.BusinessResultObjects.Enums;
using Project.Core.Utilities.Security.JWT;
using Project.Entities.Entities;
using TokenOptions = Project.Core.Utilities.Security.JWT.TokenOptions;

namespace Project.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;


        //DI
        public AuthManager(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _configuration = configuration;
        }




        public async Task<IDataResult<User>> GetAuthenticatedUser()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if (user == null)
            {
                return new ErrorDataResult<User>(Message.UserNotFound);
            }

            return new SuccessDataResult<User>(user);
        }





        public async Task<IDataResult<User>> GetUser(long userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                return new ErrorDataResult<User>(Message.UserNotFound);
            }


            return new SuccessDataResult<User>(user);
        }





        public async Task<IResult> IsUserInRole(User user,string role)
        {
            var result = await _userManager.IsInRoleAsync(user, role) ? true : false;

            if (!result)
            {
                return new ErrorResult(Message.UserIsNotInRole);
            }

            return new SuccessResult();
        }





        public async Task<IDataResult<AccessToken>> GetAuthenticatedToken(User user)
        {

            var isUserInRole = await this.IsUserInRole(user, "Writer");
            var userRole = isUserInRole.ResultType == ResultType.Success ? "Writer" : "User";

            //var userRole = await (this.IsUserInRole(user, "Admin").Result.ResultType == ResultType.Success) ? "Admin" : "User";

            var tokenOptions = _configuration.GetSection("JwtConfiguration").Get<TokenOptions>();
            var accessTokenExpiration = DateTime.Now.AddDays(tokenOptions.AccessTokenExpiration);

            IdentityOptions _options = new IdentityOptions();
            var claims = new[]
            {
                new Claim("userId",user.Id.ToString()),
                new Claim("email", user.Email),
                new Claim(ClaimTypes.Role, userRole),
                new Claim("user_role",userRole), 

                new Claim("user_picture",user.Picture),
                new Claim("name", $"{user.Name} {user.Lastname}"),
                new Claim("user_birthday",user.BirthDay.ToString()),
                new Claim("user_created",user.Created.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
            };

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(tokenOptions.SecurityKey));
            var securityToken = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: accessTokenExpiration,
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            var accessToken = JwtHelper.CreateAccessToken(token, user, userRole, accessTokenExpiration);
            return new SuccessDataResult<AccessToken>(accessToken);
        }
    }
}