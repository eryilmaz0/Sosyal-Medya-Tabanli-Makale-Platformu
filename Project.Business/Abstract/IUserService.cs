using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Project.Core.Business.BusinessResultObjects;
using Project.Core.Utilities.Security.JWT;
using Project.Entities.Dtos;
using Project.Entities.Dtos.PostDtos;
using Project.Entities.Enums;

namespace Project.Business.Abstract
{
    public interface IUserService
    {
        IDataResult<AccessToken> UpdateUser(UpdateUserDto updateUserDto);
        IDataResult<AccessToken> UpdateProfilePicture(IFormFile profilePicture);
        IDataResult<UserDto> GetUserProfile(long userId);

    }
}