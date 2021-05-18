using System.Collections.Generic;
using Project.Core.Business.BusinessResultObjects;
using Project.Entities.Dtos;
using Project.Entities.Entities;

namespace Project.Business.Abstract
{
    public interface IArticleCategoryService
    {
        IDataResult<List<ArticleCategory>> GetArticleCategories();
        IDataResult<ArticleCategory> GetArticleCategory(long articleCategoryId);
        IDataResult<ArticleCategory> PredictArticleCategoryByUser(long userId);
    }
}
