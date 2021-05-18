using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project.DataAccess.Abstract;
using Project.Entities.DbContext;
using Project.Entities.Dtos;
using Project.Entities.Entities;
using Project.Entities.Enums;

namespace Project.DataAccess.EntityFramework
{
    public class EfAUserHistoryRepository : EfARepository<UserHistory>, IUserHistoryRepository
    {
        private readonly ProjectDbContext _context;


        //DI
        public EfAUserHistoryRepository(ProjectDbContext context) : base(context)
        {
            _context = context;
        }




        //public List<UserHistoryDto> GerUserHistories(long userId)
        //{

        //    return  _context.UserHistories
        //            .AsNoTracking()
        //            .IgnoreQueryFilters()
        //            .Where(x => x.UserId == userId)
        //            .OrderByDescending(x => x.Created)
        //            .Take(50)
        //            .Select(y => new UserHistoryDto()
        //            {

        //                HistoryType = y.HistoryType,
        //                UserId = y.UserId,
        //                ArticleId = y.ArticleId,
        //                ArticleCommentId = y.ArticleCommentId,
        //                TopicId = y.TopicId,
        //                TopicCommentId = y.TopicCommentId,
        //                Created = y.Created,

        //            }).ToList();
        //}





        //public List<ArticleCategoryDto> GetCategoriesByUserInterest(long userId)
        //{
        //    return _context.UserHistory
        //        .Include(x => x.Article).ThenInclude(x => x.ArticleCategory)
        //        .IgnoreQueryFilters()
        //        .AsNoTracking()
        //        .OrderByDescending(x => x.Created) //TARİHE GÖRE SIRALA
        //        .Take(20) //İLK 20 KAYITI AL (DEĞİŞEBİLİR)
        //        .Where(x =>

        //            (x.HistoryType == HistoryType.ArticleCreated
        //             || x.HistoryType == HistoryType.ArticleUpdated
        //             || x.HistoryType == HistoryType.ArticleCommentCreated
        //             || x.HistoryType == HistoryType.ArticleCommentUpdated
        //             || x.HistoryType == HistoryType.ArticleLiked
        //             || x.HistoryType == HistoryType.ArticleAddedToFavorites
        //             || x.HistoryType == HistoryType.ArticleViewed
        //            )

        //            && x.UserId == userId
        //        )


        //        .GroupBy(g => new { g.Article.ArticleCategory.Id, g.Article.ArticleCategory.Category, g.Article.ArticleCategory.CategoryDescription })
        //        .OrderByDescending(x => x.Count()).Take(3)  //COUNTA GÖRE SIRALA, İLK ÜÇÜNÜ AL

        //        .Select(y => new ArticleCategoryDto()
        //        {
        //            Id = y.Count(),
        //            Category = y.Key.Category,
        //            CategoryDescription = y.Key.CategoryDescription

        //        }).ToList();

        //}
    }
}