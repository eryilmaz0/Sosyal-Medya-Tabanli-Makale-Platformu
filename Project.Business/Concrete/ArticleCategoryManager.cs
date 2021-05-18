using Project.Business.Abstract;
using Project.Core.Business.BusinessResultObjects;
using Project.DataAccess.Abstract;
using Project.DataAccess.UnitOfWork;
using Project.Entities.Dtos;
using System.Collections.Generic;
using MlNet;
using Project.Business.Messages;
using Project.Core.Business.BusinessResultObjects.Enums;
using Project.Core.Utilities.Business;
using Project.Entities.Entities;

namespace Project.Business.Concrete
{
    public class ArticleCategoryManager : IArticleCategoryService
    {
        private readonly IProjectUnitOfWork _uow;
        private readonly IAuthService _authService;
        


        //DI
        public ArticleCategoryManager(IProjectUnitOfWork uow, IAuthService authService)
        {
            _uow = uow;
            _authService = authService;
           
        }



    public IDataResult<List<ArticleCategory>> GetArticleCategories()
    {
      return new SuccessDataResult<List<ArticleCategory>>(_uow.ArticleCategories.GetAll());
    }





    public IDataResult<ArticleCategory> GetArticleCategory(long articleCategoryId)
    {
            var error = BusinessRules.Run(isArticleCategoryExist(articleCategoryId));

            if (error != null)
            {
                return ResultConverter.ResultToDataResult<ArticleCategory>(error);
            }


            return new SuccessDataResult<ArticleCategory>(_uow.ArticleCategories.Get(x=>x.Id == articleCategoryId));
        }




    public IDataResult<ArticleCategory> PredictArticleCategoryByUser(long userId)
    {
            var error = BusinessRules.Run(CheckUserIsExist(userId));

            if (error != null)
            {
                return ResultConverter.ResultToDataResult<ArticleCategory>(error);
            }


            var predictedCategoryId = PredictHelper.PredictArticleCategoryByUser(userId);
            return new SuccessDataResult<ArticleCategory>(_uow.ArticleCategories.Get(x => x.Id == predictedCategoryId));
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





    private IResult isArticleCategoryExist(long categoryId)
        {
            var category = this._uow.ArticleCategories.Get(x => x.Id == categoryId);

            if (category == null)
            {
                return new ErrorResult();
            }


            return new SuccessResult();
            
        }
    }
}
