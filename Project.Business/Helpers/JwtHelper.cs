using System;
using Project.Core.Utilities.Security.JWT;
using Project.Entities.Entities;

namespace Project.Business.Helpers
{
    public class JwtHelper
    {
        public static AccessToken CreateAccessToken(string token, User user, string userRole, DateTime accessTokenExpiration)
        {
            return new AccessToken()
            {
                Token = token,
                Expiration = accessTokenExpiration,
                UserId = user.Id,
                Email = user.Email,
                Name = $"{user.Name} {user.Lastname}",
                Picture = user.Picture,
                UserRole = userRole,
                Created = user.Created

            };
        }
    }
}