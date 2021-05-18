using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project.DataAccess.Abstract;
using Project.Entities.DbContext;
using Project.Entities.Dtos;
using Project.Entities.Entities;

namespace Project.DataAccess.EntityFramework
{
    public class EfAFavoriteRepository : EfARepository<Favorite>, IFavoriteRepository
    {
        private readonly ProjectDbContext _context;


        //DI
        public EfAFavoriteRepository(ProjectDbContext context) : base(context)
        {
            _context = context;
        }





        public List<FavoriteDto> GetFavoriteArticlesByUser(long userId)
        {
            return _context.Favorites
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Include(x => x.Article)
                .Where(x => x.UserId == userId)
                .Select(y =>
                    new FavoriteDto()
                    {
                        Id = y.Id,
                        Description = y.Description,
                        Created = y.Created,
                        isDeleted = y.IsDeleted,

                        Article = new ArticleDto()
                        {
                            Id = y.ArticleId,
                            Title = y.Article.Title,
                            isDeleted = y.Article.IsDeleted
                        }

                    }).ToList();
        }
    }
}