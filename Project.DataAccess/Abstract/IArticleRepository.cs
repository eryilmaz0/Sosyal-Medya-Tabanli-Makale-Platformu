using System.Collections.Generic;
using Project.Core.DataAccess.Repository;
using Project.Entities.Dtos;
using Project.Entities.Entities;

namespace Project.DataAccess.Abstract
{
    public interface IArticleRepository : IRepository<Article>
    {
        List<ArticleDto> GetAllArticles();
        List<ArticleDto> GetMostViewedArticles();
        List<ArticleDto> GetMostLikedArticles();
        ArticleDto GetArticle(long articleId);
        List<ArticleDto> GetArticlesByUserInterest(long articleCategoryId);
        List<ArticleDto> GetFilteredArticles(string filter);

    }
}