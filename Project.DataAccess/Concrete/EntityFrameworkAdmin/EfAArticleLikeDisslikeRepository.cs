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
    public class EfAArticleLikeDisslikeRepository : EfARepository<ArticleLikeDisslike>, IArticleLikeDisslikeRepository
    {
        private readonly ProjectDbContext _context;


        //DI
        public EfAArticleLikeDisslikeRepository(ProjectDbContext context) : base(context)
        {
            _context = context;
        }



        public List<ArticleLikeDisslikeDto> GetUsersLikedOrDisslikedArticle(long articleId, LikeDisslikeType likeDisslikeType)
        {
            return  _context.ArticleLikeDisslikes
                    .AsNoTracking()
                    .IgnoreQueryFilters()
                    .Include(x => x.User)
                    .Where(x => x.ArticleId == articleId && x.LikeDisslikeType == likeDisslikeType)
                    .Select(y => new ArticleLikeDisslikeDto()
                    {
                        Id = y.Id,
                       isDeleted = y.IsDeleted,
                       User = new UserDto()
                       {
                           Id = y.User.Id,
                           Name = y.User.Name,
                           Lastname = y.User.Lastname,
                           Picture = y.User.Picture,
                           isDeleted = y.User.IsDeleted
                       }

                    }).ToList();
        }
    }
}