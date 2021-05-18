using Project.DataAccess.Abstract;
using Project.Entities.DbContext;
using Project.Entities.Dtos;
using Project.Entities.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Project.DataAccess.EntityFrameworkWithSoftDelete
{
    public class EfUFavoriteRepository : EfURepository<Favorite>, IFavoriteRepository
    {
        private readonly ProjectDbContext _context;


        //DI
        public EfUFavoriteRepository(ProjectDbContext context):base(context)
        {
            _context = context;
        }





    public List<FavoriteDto> GetFavoriteArticlesByUser(long userId)
    {
      return _context.Favorites
              .AsNoTracking()
              .Include(x => x.Article)
              .Where(x => x.UserId == userId)
              .Select(y =>
              new FavoriteDto()
              {
                Id = y.Id,
                Description = y.Description,
                ArticleId = y.ArticleId,
                Created = y.Created,
                UserId = y.UserId,
                ArticleTitle = y.Article.Title
              }).ToList();
    }
  }
}
