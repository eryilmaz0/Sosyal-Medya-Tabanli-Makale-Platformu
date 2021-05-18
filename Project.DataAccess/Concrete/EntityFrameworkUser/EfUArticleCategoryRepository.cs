using Project.DataAccess.Abstract;
using Project.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Project.Entities.DbContext;
using Project.Entities.Dtos;
using Project.Entities.Enums;

namespace Project.DataAccess.EntityFrameworkWithSoftDelete
{
    public class EfUArticleCategoryRepository : EfURepository<ArticleCategory>, IArticleCategoryRepository
    {
        private readonly ProjectDbContext _context;


        //DI
        public EfUArticleCategoryRepository(ProjectDbContext context):base(context)
        {
            _context = context;
        }




        //public List<ArticleCategoryDto> GetCategoriesByUserInterest(long userId)
        //{
        //    return _context.UserHistory
        //            .Include(x => x.Article).ThenInclude(x => x.ArticleCategory)
        //            .AsNoTracking()
        //            .Where(x =>

        //                (x.HistoryType == HistoryType.ArticleCreated
        //                 || x.HistoryType == HistoryType.ArticleUpdated
        //                 || x.HistoryType == HistoryType.ArticleCommentCreated
        //                 || x.HistoryType == HistoryType.ArticleCommentUpdated
        //                 || x.HistoryType == HistoryType.ArticleLiked
        //                 || x.HistoryType == HistoryType.ArticleAddedToFavorites
        //                 || x.HistoryType == HistoryType.ArticleViewed
        //                )

        //                && x.UserId == userId

        //            )

        //            .OrderByDescending(x => x.Created) //TARİHE GÖRE SIRALA
        //            .Take(20) //İLK 20 KAYITI AL (DEĞİŞEBİLİR)


        //            .GroupBy(g => new { g.Article.ArticleCategory.Id, g.Article.ArticleCategory.Category, g.Article.ArticleCategory.CategoryDescription })
        //            .Where(x => x.Count() >= 5)                   //HAVING
        //            .OrderByDescending(x => x.Count()).Take(3)  //COUNTA GÖRE SIRALA, İLK ÜÇÜNÜ AL

        //            .Select(y => new ArticleCategoryDto()
        //            {
        //                Id = y.Key.Id,
        //                Category = y.Key.Category,
        //                CategoryDescription = y.Key.CategoryDescription

        //            }).ToList();
            
        //}
    }
}