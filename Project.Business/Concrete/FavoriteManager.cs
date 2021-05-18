using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Project.Business.Abstract;
using Project.Business.Messages;
using Project.Core.Business.BusinessResultObjects;
using Project.Core.Business.BusinessResultObjects.Enums;
using Project.Core.Utilities.Business;
using Project.Core.Utilities.Paging;
using Project.DataAccess.UnitOfWork;
using Project.Entities.DbContext;
using Project.Entities.Dtos;
using Project.Entities.Dtos.PostDtos;
using Project.Entities.Entities;
using Project.Entities.Enums;

namespace Project.Business.Concrete
{
    public class FavoriteManager : IFavoriteService
    {
        private readonly IProjectUnitOfWork _uow;
        private readonly IAuthService _authService;
        private readonly IUserHistoryService _userHistoryService;
        private readonly IArticleService _articleService;



        //DI
        public FavoriteManager(IProjectUnitOfWork uow, IAuthService authService, IUserHistoryService userHistoryService, IArticleService articleService)
        {
            _uow = uow;
            _authService = authService;
            _userHistoryService = userHistoryService;
            _articleService = articleService;
        }




        public IDataResult<PagedList<FavoriteDto>> GetFavoriteArticlesByUser(int currentPage, int pageSize)
        {
            var errorResult = BusinessRules.Run(CheckAuthenticatedUserExist(), ValidatePagingParams(currentPage,pageSize));

            if (errorResult != null)
            {
                return ResultConverter.ResultToDataResult<PagedList<FavoriteDto>>(errorResult);
            }
            

            var user = _authService.GetAuthenticatedUser().Result.Data;
            var pagedUserFavorites = PagedList<FavoriteDto>.ToPagedList(_uow.Favorites.GetFavoriteArticlesByUser(user.Id), currentPage, pageSize);
            return new SuccessDataResult<PagedList<FavoriteDto>>(pagedUserFavorites);
        }






        public IResult CreateFavoriteArticle(AddFavoriteDto addFavoriteDto)
        {
            var errorResult = BusinessRules.Run(CheckAuthenticatedUserExist(), CheckArticleIsExist(addFavoriteDto.ArticleId));

            if (errorResult != null)
            {
                return errorResult;
            }

            var user = _authService.GetAuthenticatedUser().Result.Data;

            //BU MAKALE ZATEN FAVORİLERİNDE Mİ?
            if (IsArticleAlreadyUsersFavorite(user.Id, addFavoriteDto.ArticleId).ResultType == ResultType.Success)
            {
                //EVET İSE, SİL.
                var favorite = _uow.Favorites.Get(x => x.ArticleId == addFavoriteDto.ArticleId && x.UserId == user.Id);
                var result = DeleteFavorite(favorite);
                return result;
            }


            //DEĞİL İSE, EKLE
            var newFavorite = new Favorite(){ArticleId = addFavoriteDto.ArticleId};
            if (!string.IsNullOrEmpty(addFavoriteDto.Description))
            {
                newFavorite.Description = addFavoriteDto.Description;
            }

            user.Favorites.Add(newFavorite);
            _uow.Commit();
            return new SuccessResult(Message.FavoriteCreated);

        }





        public IResult RemoveFavoriteArticle(long favoriteId)
        {
            var user = _authService.GetAuthenticatedUser().Result.Data;
            var favorite = _uow.Favorites.Get(x => x.Id == favoriteId);

            var errorResult = BusinessRules.Run(CheckAuthenticatedUserExist(), IsFavoriteExist(favoriteId),
                                                IsFavoriteBelongToUser(user.Id, favoriteId));

            if (errorResult != null)
            {
                return errorResult;
            }


            this._uow.Favorites.Remove(favorite);
            this._uow.Commit();
            return new SuccessResult(Message.FavoriteRemoved);
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





        private IResult CheckArticleIsExist(long articleId)
        {

            var article = _articleService.GetArticleById(articleId);

            if (article == null)
            {
                return new ErrorResult(Message.ArticleNotFound);
            }

            return new SuccessResult();
            
        }




        private IResult IsFavoriteExist(long favoriteId)
        {

            var favorite = _uow.Favorites.Get(x => x.Id == favoriteId);

            if (favorite == null)
            {
                return new ErrorResult(Message.FavoriteNotFound);
            }

            return new SuccessResult();
        }





        private IResult IsFavoriteBelongToUser(long userId, long favoriteId)
        {
            var favorite = _uow.Favorites.Get(x => x.Id == favoriteId);

            if (favorite.UserId != userId)
            {
                return new ErrorResult(Message.CantProcessOnFavorite);
            }

            return new SuccessResult();
        }




        private IResult IsArticleAlreadyUsersFavorite(long userId, long articleId)
        {

            var favorite = _uow.Favorites.Get(x => x.ArticleId == articleId && x.UserId == userId);

            if (favorite == null)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
            
        }




        private IResult DeleteFavorite(Favorite favorite)
        {
            _uow.Favorites.Remove(favorite);
            _uow.Commit();
            return new SuccessResult(Message.FavoriteRemoved);
        }



        private IResult ValidatePagingParams(int currentPage, int pageSize)
        {
          if (currentPage <= 0 || pageSize <= 0)
          {
            return new ErrorResult(Message.PagingParamsNotValid);
          }

          return new SuccessResult();
        }
  }



}
