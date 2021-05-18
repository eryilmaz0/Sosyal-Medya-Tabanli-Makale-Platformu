using System.Collections.Generic;
using Project.Business.Abstract;
using Project.Business.Messages;
using Project.Core.Business.BusinessResultObjects;
using Project.Core.Business.BusinessResultObjects.Enums;
using Project.Core.Utilities.Business;
using Project.DataAccess.UnitOfWork;
using Project.Entities.Dtos;
using Project.Entities.Dtos.PostDtos;
using Project.Entities.Entities;
using Project.Entities.Enums;

namespace Project.Business.Concrete
{
    public class AuthorizationAppealManager : IAuthorizationAppealService
    {
        private readonly IProjectUnitOfWork _uow;
        private readonly IAuthService _authService;


        //DI
        public AuthorizationAppealManager(IProjectUnitOfWork uow, IAuthService authService)
        {
            _uow = uow;
            _authService = authService;
        }

        



        public IDataResult<List<AuthorizationAppeal>> GetAuthorizationAppealsByUser()
        {

            var errorResult = BusinessRules.Run(CheckAuthenticatedUserExist());

            if (errorResult != null)
            {
                return ResultConverter.ResultToDataResult<List<AuthorizationAppeal>>(errorResult);
            }
            

            var user = _authService.GetAuthenticatedUser().Result.Data;

            return new SuccessDataResult<List<AuthorizationAppeal>>(this._uow.AuthorizationAppeals.GetAll(x=>x.UserId == user.Id));
        }






        public IDataResult<AuthorizationAppealDto> GetAuthorizationAppealById(long appealId)
        {
            var user = _authService.GetAuthenticatedUser().Result.Data;

            var errorResult = BusinessRules.Run(CheckAuthenticatedUserExist(), CheckAppealIsExist(appealId),
                                                IsAppealBelongToUser(user.Id, appealId));

            if (errorResult != null)
            {
                return ResultConverter.ResultToDataResult<AuthorizationAppealDto>(errorResult);
            }

            
            return new SuccessDataResult<AuthorizationAppealDto>(this._uow.AuthorizationAppeals.GetIncludedAppealById(appealId));
        }






        public IResult CreateAuthorizationAppeal(AddAuthorizationAppealDto addAuthorizationAppealDto)
        {
            var user = _authService.GetAuthenticatedUser().Result.Data;

            var errorResult = BusinessRules.Run(CheckAuthenticatedUserExist(), IsThereAnyAppealOnWait(user.Id),
                                                IsThereAnyConfirmedAppeal(user.Id));

            if (errorResult != null)
            {
                return errorResult;
            }

            
             user.AuthorizationAppeals.Add(new AuthorizationAppeal(){AppealDescription = addAuthorizationAppealDto.AppealDescription});
            _uow.Commit();
            return new SuccessResult(Message.AppealCreated);
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





        private IResult CheckAppealIsExist(long appealId)
        {

            var appeal = _uow.AuthorizationAppeals.Get(x => x.Id == appealId);

      if (appeal == null)
            {
                return new ErrorResult(Message.AppealNotFound);
            }

            return new SuccessResult();
            
        }





        private IResult IsAppealBelongToUser(long userId, long appealId)
        {
            var appeal = _uow.AuthorizationAppeals.Get(x => x.Id == appealId);

            if (appeal.UserId != userId)
            {
                return new ErrorResult(Message.CantProcessOnAppeal);
            }

            return new SuccessResult();

        }





        private IResult IsThereAnyAppealOnWait(long userId)
        {

            var waitingAppeal = _uow.AuthorizationAppeals.Get(x=>x.UserId == userId && x.AppealStatus == AppealStatus.Waiting);

            if (waitingAppeal != null)
            {
                return new ErrorResult(Message.ThereIsWaitingAppeal);
            }

            return new SuccessResult();

        }





        private IResult IsThereAnyConfirmedAppeal(long userId)
        {

            var confirmedAppeal = _uow.AuthorizationAppeals.Get(x=>x.UserId == userId && x.AppealStatus == AppealStatus.Positive);

            if (confirmedAppeal != null)
            {
                return new ErrorResult(Message.ThereIsConfirmedAppeal);
            }

            return new SuccessResult();
        }
    }
}
