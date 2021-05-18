
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Project.Business.Abstract;
using Project.Business.Messages;
using Project.Core.Business.BusinessResultObjects;
using Project.Core.Business.BusinessResultObjects.Enums;
using Project.Core.Utilities.Business;
using Project.Core.Utilities.Paging;
using Project.Core.ValidationErrorObjects;
using Project.DataAccess.Abstract;
using Project.DataAccess.UnitOfWork;
using Project.Entities.Dtos;
using Project.Entities.Dtos.PostDtos;
using Project.Entities.Entities;
using Project.Entities.Enums;

namespace Project.Business.Concrete
{
    public class ArticleManager : IArticleService
    {
        private readonly IProjectUnitOfWork _uow;
        private readonly IAuthService _authService;
        private readonly IUserHistoryService _userHistoryService;
        private readonly IArticleCategoryService _articleCategoryService;
        private readonly IDocumentService _documentService;


        //DI
        public ArticleManager(IProjectUnitOfWork uow, IAuthService authService, 
                            IUserHistoryService userHistoryService, IArticleCategoryService articleCategoryService,
                            IDocumentService documentService)
        {
            _uow = uow;
            _authService = authService;
            _userHistoryService = userHistoryService;
            _articleCategoryService = articleCategoryService;
            _documentService = documentService;
        }





        public IDataResult<PagedList<ArticleDto>> GetAllArticles(int currentPage, int pageSize)
        {
            var errorResult = BusinessRules.Run(ValidatePagingParams(currentPage, pageSize));

            if (errorResult != null)
            {
                return ResultConverter.ResultToDataResult<PagedList<ArticleDto>>(errorResult);
            }


            var pagedArticleList = PagedList<ArticleDto>.ToPagedList(_uow.Articles.GetAllArticles(), currentPage, pageSize);
            return new SuccessDataResult<PagedList<ArticleDto>>(pagedArticleList);
        }





        public IDataResult<ArticleDto> GetArticle(long articleId)
        {

            var errorResult = BusinessRules.Run(IsArticleExist(articleId));


            if (errorResult != null)
            {
                return ResultConverter.ResultToDataResult<ArticleDto>(errorResult);
            }



            return new SuccessDataResult<ArticleDto>(_uow.Articles.GetArticle(articleId));
        }





        public IDataResult<Article> GetArticleById(long articleId)
        {

            var errorResult = BusinessRules.Run(IsArticleExist(articleId));


            if (errorResult != null)
            {
                return ResultConverter.ResultToDataResult<Article>(errorResult);
            }



            return new SuccessDataResult<Article>(_uow.Articles.Get(x=>x.Id == articleId));
        }





        public IDataResult<PagedList<ArticleDto>> GetMostViewedArticles(int currentPage, int pageSize)
        {
            var errorResult = BusinessRules.Run(ValidatePagingParams(currentPage, pageSize));

            if (errorResult != null)
            {
                return ResultConverter.ResultToDataResult<PagedList<ArticleDto>>(errorResult);
            }

            var pagedArticleList = PagedList<ArticleDto>.ToPagedList(_uow.Articles.GetMostViewedArticles(), currentPage, pageSize);
            return new SuccessDataResult<PagedList<ArticleDto>>(pagedArticleList);

        }





        public IDataResult<PagedList<ArticleDto>> GetMostLikedArticles(int currentPage, int pageSize)
        {
            var errorResult = BusinessRules.Run(ValidatePagingParams(currentPage, pageSize));

            if (errorResult != null)
            {
                return ResultConverter.ResultToDataResult<PagedList<ArticleDto>>(errorResult);
            }

            var pagedArticleList = PagedList<ArticleDto>.ToPagedList(_uow.Articles.GetMostLikedArticles(), currentPage, pageSize);
            return new SuccessDataResult<PagedList<ArticleDto>>(pagedArticleList);

        }






        public IDataResult<List<ArticleDto>> GetArticlesByUserInterest()
        {

            var errorResult = BusinessRules.Run(CheckAuthenticatedUserExist());

            if (errorResult != null)
            {
                return ResultConverter.ResultToDataResult<List<ArticleDto>>(errorResult);
            }


            var user = _authService.GetAuthenticatedUser().Result.Data;

            var predictedArticleCategoryId = _articleCategoryService.PredictArticleCategoryByUser(user.Id);


            return new SuccessDataResult<List<ArticleDto>>(_uow.Articles.GetArticlesByUserInterest(predictedArticleCategoryId.Data.Id));


        }





        public IDataResult<PagedList<ArticleDto>> GetFilteredArticles(string filterText, int currentPage, int pageSize)
        {
            var errorResult = BusinessRules.Run(ValidatePagingParams(currentPage, pageSize));

            if (errorResult != null)
            {
                return ResultConverter.ResultToDataResult<PagedList<ArticleDto>>(errorResult);
            }

            var pagedArticles = PagedList<ArticleDto>.ToPagedList(_uow.Articles.GetFilteredArticles(filterText), currentPage, pageSize);
            return new SuccessDataResult<PagedList<ArticleDto>>(pagedArticles);
        }





        public IResult CreateArticle(AddArticleDto addArticleDto)
        {


            var errorResult = BusinessRules.Run(CheckAuthenticatedUserExist(),CheckArticleCategoryExist(addArticleDto.ArticleCategoryId));


            if (errorResult != null)
            {
                return errorResult;
            }
                                            
            
            var user = _authService.GetAuthenticatedUser().Result.Data;


            var article = new Article()
            {
                Title = addArticleDto.Title,
                Content = addArticleDto.Content,
                UserId = user.Id,
                ArticleCategoryId = addArticleDto.ArticleCategoryId
            };

            user.UserHistories.Add(new UserHistory(){ArticleCategoryId = addArticleDto.ArticleCategoryId});
            _uow.Articles.Add(article); //
            _uow.Commit();  //

            return new SuccessResult(Message.ArticleCreated);




        }





        public IResult CreateArticle(IFormFile articleFile, long articleCategoryId)
        {

          var errorResult = BusinessRules.Run(CheckAuthenticatedUserExist(), CheckArticleCategoryExist(articleCategoryId));


          if (errorResult != null)
          {
            return errorResult;
          }

          //UPLOAD ET
          var uploadArticleResult = _documentService.UplaodFile(articleFile, FileType.Articles);

          //VARSA HATAYI DÖN
          if (uploadArticleResult.ResultType != ResultType.Success)
          {
            return uploadArticleResult;
          }


          var user = _authService.GetAuthenticatedUser().Result.Data;


          var article = new Article()
          {
            UserId = user.Id,
            ArticleFile = uploadArticleResult.Message,
            ArticleCategoryId = articleCategoryId
          };

          _uow.Articles.Add(article); //
          _uow.Commit();  //

          return new SuccessResult(Message.ArticleCreated);

        }




        public IResult UpdateArticle(UpdateArticleDto updateArticleDto)
        {

            var user = _authService.GetAuthenticatedUser().Result.Data;
            var article = _uow.Articles.Get(x => x.Id == updateArticleDto.ArticleId);

            //LOGIN KULLANICI VAR MI
            //MAKALE MEVCUT MU
            //MAKALE KATEGORİSİ MEVCUT MU
            //MAKALE MEVCUT KULLANICIYA MI AİT
            var errorResult = BusinessRules.Run( CheckAuthenticatedUserExist(), IsArticleExist(updateArticleDto.ArticleId),
                                                 CheckArticleCategoryExist(updateArticleDto.ArticleCategoryId),
                                                 IsArticleBelongToUser(user.Id, article.Id) //(PATLAR)
                                                );

            if (errorResult != null)
            {
                return errorResult;
            }

            /*BURAYA KADAR OLAN KISIM İŞ KURALLARI. BİR İŞ MOTORU YAZILABİLİR
            -------------------------------------------------------------------- */
            article.Title = updateArticleDto.Title;
            article.Content = updateArticleDto.Content;
            article.ArticleCategoryId = updateArticleDto.ArticleCategoryId;

            this._uow.Articles.Update(article);
            this._uow.Commit();

            return new SuccessResult(Message.ArticleUpdated);
        }






        public IResult UpdateArticleCoverPhoto(IFormFile articleCoverPhoto, long articleId)
        {
            var user = _authService.GetAuthenticatedUser().Result.Data;
            var article = _uow.Articles.Get(x => x.Id == articleId);

            //LOGIN KULLANICI VAR MI
            //MAKALE MEVCUT MU
            //MAKALE MEVCUT KULLANICIYA MI AİT
            var errorResult = BusinessRules.Run(CheckAuthenticatedUserExist(), IsArticleExist(articleId),
                                                IsArticleBelongToUser(user.Id, article.Id), CheckIsArticlePhotoEmpty(articleCoverPhoto));

            if (errorResult != null)
            {
                return errorResult;
            }

            //UPLOAD ET
            var updateArticlePhotoResult = _documentService.UplaodFile(articleCoverPhoto,FileType.ArticleCoverPhotos);

            if (updateArticlePhotoResult.ResultType != ResultType.Success)
            {
                return updateArticlePhotoResult;
            }

            //UPDATE
            article.Picture = updateArticlePhotoResult.Message;
            _uow.Articles.Update(article);
            _uow.Commit();

            return new SuccessResult(Message.ArticleUpdated);
        }





        public IResult RemoveArticle(long articleId)
        {

            var user = _authService.GetAuthenticatedUser().Result.Data;
            var article = _uow.Articles.Get(x => x.Id == articleId);

            var errorResult = BusinessRules.Run(CheckAuthenticatedUserExist(), IsArticleExist(articleId),
                                                IsArticleBelongToUser(user.Id, article.Id));


            if (errorResult != null)
            {
                return errorResult;
            }


            _uow.Articles.Remove(article);
            _uow.Commit();

            return new SuccessResult(Message.ArticleDeleted);


        }





        public IDataResult<List<Article>> GetArticlesByUser()
        {

            var errorResult = BusinessRules.Run(CheckAuthenticatedUserExist());

            if (errorResult != null)
            {
                return ResultConverter.ResultToDataResult<List<Article>>(errorResult);
            }


            var user = _authService.GetAuthenticatedUser().Result.Data;
            return new SuccessDataResult<List<Article>>(_uow.Articles.GetAll(x=>x.UserId == user.Id).OrderByDescending(x=>x.Created).ToList());

        }





        //
        public IDataResult<Article> GetArticleByUser(long articleId)
        {

            var user = _authService.GetAuthenticatedUser().Result.Data;

            var errorResult = BusinessRules.Run(CheckAuthenticatedUserExist(), IsArticleExist(articleId),
                                                IsArticleBelongToUser(user.Id, articleId));


            if (errorResult != null)
            {
                return ResultConverter.ResultToDataResult<Article>(errorResult);
            }

            return new SuccessDataResult<Article>(_uow.Articles.Get(x => x.Id == articleId));
        }





        public IDataResult<PagedList<ArticleCommentDto>> GetArticleCommentsByArticle(long articleId, int currentPage, int pageSize)
        {

            var errorResult = BusinessRules.Run(IsArticleExist(articleId), ValidatePagingParams(currentPage,pageSize));

            if (errorResult != null)
            {
                return ResultConverter.ResultToDataResult<PagedList<ArticleCommentDto>>(errorResult);
            }

            var pagedArticleComments =  PagedList<ArticleCommentDto>
                                        .ToPagedList(_uow.ArticleComments.GetArticleCommentsByArticle(articleId), currentPage, pageSize);
            return new SuccessDataResult<PagedList<ArticleCommentDto>>(pagedArticleComments);
        }






        public IDataResult<List<ArticleCommentDto>> GetArticleCommentsByUser(long userId)
        {
            var errorResult = BusinessRules.Run(CheckAuthenticatedUserExist());

            if (errorResult != null)
            {
                return ResultConverter.ResultToDataResult<List<ArticleCommentDto>>(errorResult);
            }
           

            return new SuccessDataResult<List<ArticleCommentDto>>(_uow.ArticleComments.GetArticleCommentsByUser(userId));
        }






        public IDataResult<ArticleCommentDto> GetArticleComment(long articleCommentId)
        {
            var errorResult = BusinessRules.Run(IsArticleCommentExist(articleCommentId));

            if (errorResult != null)
            {
                return ResultConverter.ResultToDataResult<ArticleCommentDto>(errorResult);
            }
            

            return new SuccessDataResult<ArticleCommentDto>(_uow.ArticleComments.GetArticleById(articleCommentId));
        }






        public IResult CreateArticleComment(AddArticleCommentDto addArticleCommentDto)
        {
            var errorResult = BusinessRules.Run(CheckAuthenticatedUserExist(),IsArticleExist(addArticleCommentDto.ArticleId.Value));

            if (errorResult != null)
            {
                return errorResult;
            }
           

            var user = _authService.GetAuthenticatedUser().Result.Data;
            var article = _uow.Articles.Get(x => x.Id == addArticleCommentDto.ArticleId.Value);


            var articleComment = new ArticleComment()
            {
                UserId = user.Id, 
                ArticleId = addArticleCommentDto.ArticleId.Value,
                Comment = addArticleCommentDto.Comment
            };


            _uow.ArticleComments.Add(articleComment);
            _uow.Commit();

            return new SuccessResult(Message.ArticleCommentCreated);

        }






        public IResult UpdateArticleComment(UpdateArticleCommentDto updateArticleCommentDto)
        {
            var user = _authService.GetAuthenticatedUser().Result.Data;
            var articleComment = _uow.ArticleComments.Get(x => x.Id == updateArticleCommentDto.ArticleCommentId);

            var errorResult = BusinessRules.Run(CheckAuthenticatedUserExist(), IsArticleCommentExist(updateArticleCommentDto.ArticleCommentId),
                                                IsArticleCommentBelongToUser(user.Id, articleComment.Id));



            articleComment.Comment = updateArticleCommentDto.Comment;
            _uow.ArticleComments.Update(articleComment);
            _uow.Commit();
            return new SuccessResult(Message.ArticleCommentUpdated);

        }






        public IResult RemoveArticleComment(long articleCommentId)
        {
            var user = _authService.GetAuthenticatedUser().Result.Data;
            var articleComment = _uow.ArticleComments.Get(x => x.Id == articleCommentId);

            var errorResult = BusinessRules.Run(CheckAuthenticatedUserExist(), IsArticleCommentExist(articleCommentId),
                                                IsArticleCommentBelongToUser(user.Id, articleComment.Id));

            if (errorResult != null)
            {
                return errorResult;
            }

            
            _uow.ArticleComments.Remove(articleComment);
            _uow.Commit();
            
            return new SuccessResult(Message.ArticleCommentDeleted);
        }


        //







        private IResult CheckArticleCategoryExist(long categoryId)
        {

            var category = _articleCategoryService.GetArticleCategory(categoryId).Data;

            if (category == null)
            {
                return new ErrorResult(Message.ArticleCategoryNotFound);
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




        private IResult IsArticleExist(long articleId)
        {
            var article = _uow.Articles.Get(x => x.Id == articleId);

            if (article == null)
            {
                return new ErrorResult(Message.ArticleNotFound);
            }

            return new SuccessResult();
        }




        private IResult IsArticleBelongToUser(long userId, long articleId)
        {

            var article = _uow.Articles.Get(x => x.Id == articleId);

            if (article.UserId != userId)
            {
                return new ErrorResult(Message.CantProcessOnArticle);
            }

            return new SuccessResult();
            
        }





        private IResult IsArticleCommentBelongToUser(long userId, long articleCommentId)
        {

            var articleComment = _uow.ArticleComments.Get(x => x.Id == articleCommentId);

            if (articleComment.UserId != userId)
            {
                return new ErrorResult(Message.CantProcessOnArticleComment);
            }

            return new SuccessResult();
            
        }




        private IResult IsArticleCommentExist(long articleCommentId)
        {

            var articleComment = _uow.ArticleComments.Get(x => x.Id == articleCommentId);

            if (articleComment == null)
            {
                return new ErrorResult(Message.ArticleCommentNotFound);
            }

            return new SuccessResult();

        }



        private IResult CheckIsArticlePhotoEmpty(IFormFile articleCoverPhoto)
        {

            if (articleCoverPhoto == null)
            {
                return new ErrorResult(Message.ArticleCoverPhotoCantBeEmpty);
            }

            return new SuccessResult();
            
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
