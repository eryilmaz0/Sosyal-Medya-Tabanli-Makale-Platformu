using System.Collections.Generic;
using Project.Core.DataAccess.Repository;
using Project.Entities.Dtos;
using Project.Entities.Entities;

namespace Project.DataAccess.Abstract
{
    public interface IArticleCommentRepository : IRepository<ArticleComment>
    {
        List<ArticleCommentDto> GetArticleCommentsByArticle(long articleId);
        List<ArticleCommentDto> GetArticleCommentsByUser(long userId);
        ArticleCommentDto GetArticleById(long articleCommentId);
    }
}