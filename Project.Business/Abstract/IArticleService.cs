using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage;
using Project.Core.Business.BusinessResultObjects;
using Project.Core.Utilities.Paging;
using Project.Entities.Dtos;
using Project.Entities.Dtos.PostDtos;
using Project.Entities.Entities;

namespace Project.Business.Abstract
{
    public interface IArticleService
    {
        IDataResult<PagedList<ArticleDto>> GetAllArticles(int currentPage, int pageSize);
        IDataResult<PagedList<ArticleDto>> GetMostViewedArticles(int currentPage, int pageSize);
        IDataResult<PagedList<ArticleDto>> GetMostLikedArticles(int currentPage, int pageSize);
        IDataResult<List<ArticleDto>> GetArticlesByUserInterest();
        IDataResult<PagedList<ArticleDto>> GetFilteredArticles(string filterText, int currentPage, int pageSize);
        IDataResult<ArticleDto> GetArticle(long articleId);
        IDataResult<Article> GetArticleById(long articleId);
        IResult CreateArticle(AddArticleDto addArticleDto);
        IResult CreateArticle(IFormFile articleFile, long articleCategoryId);
        IResult UpdateArticle(UpdateArticleDto updateArticleDto);
        IResult UpdateArticleCoverPhoto(IFormFile articleCoverPhoto, long articleId);
        IResult RemoveArticle(long articleId);
        IDataResult<List<Article>> GetArticlesByUser();
        IDataResult<Article> GetArticleByUser(long articleId);
        IDataResult<PagedList<ArticleCommentDto>> GetArticleCommentsByArticle(long articleId,int currentPage, int pageSize);
        IDataResult<List<ArticleCommentDto>> GetArticleCommentsByUser(long userId);
        IDataResult<ArticleCommentDto> GetArticleComment(long articleCommentId);
        IResult CreateArticleComment(AddArticleCommentDto addArticleCommentDto);
        IResult UpdateArticleComment(UpdateArticleCommentDto updateArticleCommentDto);
        IResult RemoveArticleComment(long articleCommentId);
        

    }
}
