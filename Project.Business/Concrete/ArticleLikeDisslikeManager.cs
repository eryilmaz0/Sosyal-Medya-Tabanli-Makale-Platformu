using System.Collections.Generic;
using Project.Business.Abstract;
using Project.Business.Messages;
using Project.Core.Business.BusinessResultObjects;
using Project.Core.Business.BusinessResultObjects.Enums;
using Project.Core.Utilities.Business;
using Project.DataAccess.UnitOfWork;
using Project.Entities.Dtos;
using Project.Entities.Entities;
using Project.Entities.Enums;

namespace Project.Business.Concrete
{
    public class ArticleLikeDisslikeManager : IArticleLikeDisslikeService
    {
        private readonly IProjectUnitOfWork _uow;
        private readonly IAuthService _authService;
        private readonly IUserHistoryService _userHistoryService;
        private readonly IArticleService _articleService;


        //DI
        public ArticleLikeDisslikeManager(IProjectUnitOfWork uow, IAuthService authService, IUserHistoryService userHistoryService, IArticleService articleService)
        {
            _uow = uow;
            _authService = authService;
            _userHistoryService = userHistoryService;
            _articleService = articleService;
        }






        public IDataResult<List<ArticleLikeDisslikeDto>> GetUsersLikedOrDisslikedArticle(long articleId, LikeDisslikeType likeDisslikeType)
        {
            var errorResult = BusinessRules.Run(IsArticleExist(articleId));

            if (errorResult != null)
            {
                return ResultConverter.ResultToDataResult<List<ArticleLikeDisslikeDto>>(errorResult);
            }
            
            return new SuccessDataResult<List<ArticleLikeDisslikeDto>>(_uow.ArticleLikeDisslikes.GetUsersLikedOrDisslikedArticle(articleId, likeDisslikeType));
        }







        public IResult AddLike(long articleId)
        {

            //LOGIN KULLANICI VAR MI, MAKALE MEVCUT MU
            var errorResult = BusinessRules.Run(CheckAuthenticatedUserExist(), IsArticleExist(articleId));

            if (errorResult != null)
            {
                return errorResult;
            }
            

            var user = _authService.GetAuthenticatedUser().Result.Data;


            //KULLANICI BU MAKALEYE LIKE ATMIŞ MI?
            if (IsThereUserLikeOnArticle(articleId, user.Id).ResultType == ResultType.Success)
            {
                //ATMIŞ İSE, KALDIR.
                var userLike = GetUserLikeOnArticle(articleId, user.Id).Data;
                DeleteLike(userLike);
            }
            

            //LIKE ATMAMIŞ İSE
            else
            {
                //KULLANICI BU MAKALEYE DISSLIKE ATMIŞ MI?
                if (IsThereUserDisslikeOnArticle(articleId, user.Id).ResultType == ResultType.Success)
                {
                    //ATMIŞ İSE KALDIR.
                    var userDisslike = GetUserDisslikeOnArticle(articleId, user.Id).Data;
                    DeleteDisslike(userDisslike);
                }


                _uow.ArticleLikeDisslikes.Add(new ArticleLikeDisslike() { UserId = user.Id, ArticleId = articleId, LikeDisslikeType = LikeDisslikeType.Like });

            }

            _uow.Commit();
            return new SuccessResult();



        }






        public IResult AddDisslike(long articleId)
        {
            var errorResult = BusinessRules.Run(CheckAuthenticatedUserExist(), IsArticleExist(articleId));

            if (errorResult != null)
            {
                return errorResult;
            }

            var user = _authService.GetAuthenticatedUser().Result.Data;

            //KULLANICI MAKALEYE DISSLIKE ATMIŞ MI?
            if (IsThereUserDisslikeOnArticle(articleId, user.Id).ResultType == ResultType.Success)
            {
                //ATMIŞ İSE, SİL.
                var userDisslike = GetUserDisslikeOnArticle(articleId, user.Id).Data;
                DeleteDisslike(userDisslike);
            }

            //ATMAMIŞ İSE
            else
            {
                //LIKE ATMIŞ MI?
                if (IsThereUserLikeOnArticle(articleId, user.Id).ResultType == ResultType.Success)
                {
                    //ATMIŞ İSE, SİL VE LIKE EKLE
                    var userLike = GetUserLikeOnArticle(articleId, user.Id).Data;
                    DeleteLike(userLike);
                }

                this._uow.ArticleLikeDisslikes.Add(new ArticleLikeDisslike(){UserId = user.Id, ArticleId = articleId, LikeDisslikeType = LikeDisslikeType.Disslike});
                
            }

            this._uow.Commit();
            return new SuccessResult();
        }







        public IResult DeleteLike(ArticleLikeDisslike userLike)
        {
            //LOGIN KULLANICI, MAKALE VE LIKE VAR MI KONTROLLERİ YUKARI SERVİSTE YAPILDI.
            //TEKRAR YAPMAYA GEREK YOK. KULLANICININ MAKALEDE ZATEN LIKE'I OLDUĞUNU BİLİYORUZ.

            _uow.ArticleLikeDisslikes.Remove(userLike);
            return new SuccessResult(Message.DefaultSuccess);
        }






        public IResult DeleteDisslike(ArticleLikeDisslike userDisslike)
        {
            //LOGIN KULLANICI, MAKALE VE LIKE VAR MI KONTROLLERİ YUKARI SERVİSTE YAPILDI.
            //TEKRAR YAPMAYA GEREK YOK. KULLANICININ MAKALEDE ZATEN DISSLIKE'I OLDUĞUNU BİLİYORUZ.

            _uow.ArticleLikeDisslikes.Remove(userDisslike);
            return new SuccessResult(Message.DefaultSuccess);
        }






        private IResult IsArticleExist(long articleId)
        {

            var article = _articleService.GetArticle(articleId).Data;

            if (article == null)
            {
                return new ErrorResult(Message.ArticleNotFound);
            }

            return new SuccessResult();

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




        private IResult IsThereUserLikeOnArticle(long articleId, long userId)
        {

            var userLike = _uow.ArticleLikeDisslikes.Get(x =>
                x.UserId == userId && x.ArticleId == articleId && x.LikeDisslikeType == LikeDisslikeType.Like);

            if (userLike == null)
            {
                return new ErrorResult();
            }

            return new SuccessResult();

        }




        private IResult IsThereUserDisslikeOnArticle(long articleId, long userId)
        {

            var userLike = _uow.ArticleLikeDisslikes.Get(x =>
                x.UserId == userId && x.ArticleId == articleId && x.LikeDisslikeType == LikeDisslikeType.Disslike);

            if (userLike == null)
            {
                return new ErrorResult();
            }

            return new SuccessResult();

        }




        private IDataResult<ArticleLikeDisslike> GetUserLikeOnArticle(long articleId, long userId)
        {
            return  new SuccessDataResult<ArticleLikeDisslike>
                (
                _uow.ArticleLikeDisslikes.Get(x =>
                    x.UserId == userId && x.ArticleId == articleId && x.LikeDisslikeType == LikeDisslikeType.Like)
                );
        }




        private IDataResult<ArticleLikeDisslike> GetUserDisslikeOnArticle(long articleId, long userId)
        {
            return new SuccessDataResult<ArticleLikeDisslike>
            (
                _uow.ArticleLikeDisslikes.Get(x =>
                    x.UserId == userId && x.ArticleId == articleId && x.LikeDisslikeType == LikeDisslikeType.Disslike)
            );
        }




        private IResult IsLikeExist(long likeId)
        {

            var like = _uow.ArticleLikeDisslikes.Get(x=>x.Id == likeId && x.LikeDisslikeType == LikeDisslikeType.Like);

            if (like == null)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
            
        }




        private IResult IsDisslikeExist(long disslikeId)
        {

            var disslike = _uow.ArticleLikeDisslikes.Get(x => x.Id == disslikeId && x.LikeDisslikeType == LikeDisslikeType.Like);

            if (disslike == null)
            {
                return new ErrorResult();
            }

            return new SuccessResult();

        }




        private IResult IsLikeBelongToUser(long userId, long likeId)
        {

            var like = _uow.ArticleLikeDisslikes.Get(x => x.Id == likeId && x.LikeDisslikeType == LikeDisslikeType.Like);

            if (like.UserId != userId)
            {
                return new ErrorResult();
            }

            return new SuccessResult();

        }




        private IResult IsDisslikeBelongToUser(long userId, long disslikeId)
        {

            var disslike = _uow.ArticleLikeDisslikes.Get(x => x.Id == disslikeId && x.LikeDisslikeType == LikeDisslikeType.Disslike);

            if (disslike.UserId != userId)
            {
                return new ErrorResult();
            }

            return new SuccessResult();

        }
    }
}