using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project.DataAccess.Abstract;
using Project.Entities.DbContext;
using Project.Entities.Dtos;
using Project.Entities.Entities;
using Project.Entities.Enums;

namespace Project.DataAccess.EntityFrameworkWithSoftDelete
{
    public class EfUArticleLikeDisslikeRepository : EfURepository<ArticleLikeDisslike>, IArticleLikeDisslikeRepository
    {
        private readonly ProjectDbContext _context;


        //DI
        public EfUArticleLikeDisslikeRepository(ProjectDbContext context):base(context)
        {
            _context = context;
        }


        public List<ArticleLikeDisslikeDto> GetUsersLikedOrDisslikedArticle(long articleId, LikeDisslikeType _likeDisslikeType)
        {
            return  _context.ArticleLikeDisslikes
                    .AsNoTracking()
                    .Include(x => x.User)
                    .Where(x => x.ArticleId == articleId && x.LikeDisslikeType == _likeDisslikeType)
                    .Select(y => new ArticleLikeDisslikeDto()
                    {
                        Id = y.Id,

                        User = new UserDto()
                        {
                            Id = y.User.Id,
                            Name = y.User.Name,
                            Lastname = y.User.Lastname,
                            Picture = y.User.Picture
                        }

                    }).ToList();
        }
    }
}