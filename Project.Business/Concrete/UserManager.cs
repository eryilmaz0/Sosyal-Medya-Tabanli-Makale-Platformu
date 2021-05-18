using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Project.Business.Abstract;
using Project.Business.Messages;
using Project.Core.Business.BusinessResultObjects;
using Project.Core.Business.BusinessResultObjects.Enums;
using Project.Core.Utilities.Business;
using Project.Core.Utilities.Security.JWT;
using Project.DataAccess.UnitOfWork;
using Project.Entities.Dtos;
using Project.Entities.Dtos.PostDtos;
using Project.Entities.Enums;

namespace Project.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IProjectUnitOfWork _uow;
        private readonly IAuthService _authService;
        private readonly IUserHistoryService _userHistoryService;
        private readonly IDocumentService _documentService;


        //DI
        public UserManager(IProjectUnitOfWork uow, IAuthService authService, IUserHistoryService userHistoryService, IDocumentService documentService)
        {
            _uow = uow;
            _authService = authService;
            _userHistoryService = userHistoryService;
            _documentService = documentService;
        }





        public  IDataResult<AccessToken> UpdateUser(UpdateUserDto updateUserDto)
        {
            var errorResult = BusinessRules.Run(CheckAuthenticatedUserExist());

            if (errorResult != null)
            {
                return ResultConverter.ResultToDataResult<AccessToken>(errorResult);
            }
            

            var user = _authService.GetAuthenticatedUser().Result.Data;
            user.About = updateUserDto.About;

            _uow.Users.Update(user);
            _uow.Commit();

            return new SuccessDataResult<AccessToken>(_authService.GetAuthenticatedToken(user).Result.Data);
        }





        public IDataResult<AccessToken> UpdateProfilePicture(IFormFile profilePicture)
        {
            var errorResult = BusinessRules.Run(CheckAuthenticatedUserExist(), CheckProfilePictureIsEmpty(profilePicture));

            if (errorResult != null)
            {
                return ResultConverter.ResultToDataResult<AccessToken>(errorResult);
            }

            var uploadPictureResult = _documentService.UplaodFile(profilePicture, FileType.ProfilePics);


            if (uploadPictureResult.ResultType != ResultType.Success)
            {
                return ResultConverter.ResultToDataResult<AccessToken>(uploadPictureResult);
            }

            var user = _authService.GetAuthenticatedUser().Result.Data;

            user.Picture = uploadPictureResult.Message;
            _uow.Users.Update(user);
            _uow.Commit();

            return new SuccessDataResult<AccessToken>(_authService.GetAuthenticatedToken(user).Result.Data);
        }





        public IDataResult<UserDto> GetUserProfile(long userId)
        {
            var errorResult = BusinessRules.Run(CheckUserIsExist(userId));

            if (errorResult != null)
            {
                return ResultConverter.ResultToDataResult<UserDto>(errorResult);
            }

            var userProfile = _uow.Users.GetUserProfile(userId);
            return new SuccessDataResult<UserDto>(userProfile);
        }






        private IResult CheckAuthenticatedUserExist()
        {

            var user = _authService.GetAuthenticatedUser().Result.Data;

            if (user == null)
            {
                return new UnauthorizedResult();
            }

            return new SuccessResult();

        }



        private IResult CheckUserIsExist(long userId)
        {

            var user = _authService.GetUser(userId).Result.Data;

            if (user == null)
            {
                return new ErrorResult(Message.UserNotFound);
            }

            return new SuccessResult();
            
        }



        private IResult CheckProfilePictureIsEmpty(IFormFile profilePicture)
        {
            if (profilePicture == null)
            {
                return new ErrorResult(Message.ProfilePictureCantBeEmpty);
            }

            return new SuccessResult();;
        }

    }
}