using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project.DataAccess.Abstract;
using Project.Entities.DbContext;
using Project.Entities.Dtos;
using Project.Entities.Entities;

namespace Project.DataAccess.EntityFramework
{
    public class EfAArticleCommentRepository : EfARepository<ArticleComment>, IArticleCommentRepository
    {
        private readonly ProjectDbContext _context;


        //DI
        public EfAArticleCommentRepository(ProjectDbContext context):base(context)
        {
            _context = context;
        }





        public List<ArticleCommentDto> GetArticleCommentsByArticle(long articleId)
        {
            return  _context.ArticleComments
                    .AsNoTracking()
                    .IgnoreQueryFilters()
                    .Include(x=>x.User)
                    .Where(x => x.ArticleId == articleId)
                    .Select(y => new ArticleCommentDto()
                    {
                        Id = y.Id,
                        Comment = y.Comment,
                        Created = y.Created,
                        UserId = y.UserId,
                        isDeleted = y.IsDeleted,

                        User = new UserDto()

                        {
                            Name = y.User.Name,
                            Lastname = y.User.Lastname,
                            Picture = y.User.Picture,
                            isDeleted = y.User.IsDeleted
                        }

                    }).ToList();
        }





        public List<ArticleCommentDto> GetArticleCommentsByUser(long userId)
        {
            return  _context.ArticleComments
                    .AsNoTracking()
                    .IgnoreQueryFilters()
                    .Where(x => x.UserId == userId)
                    .Select(y => new ArticleCommentDto()
                    {
                        Id = y.Id,
                        Comment = y.Comment,
                        Created = y.Created,
                        UserId = y.UserId

                    }).ToList();
        }





        public ArticleCommentDto GetArticleById(long articleCommentId)
        {
            return _context.ArticleComments
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Where(x => x.Id == articleCommentId)
                .Select(y =>
                    new ArticleCommentDto()
                    {
                        Id = y.Id,
                        Comment = y.Comment,
                        Created = y.Created,
                        UserId = y.UserId,
                        isDeleted = y.IsDeleted,
                        User = new UserDto()
                        {
                            Name = y.User.Name, Lastname = y.User.Lastname, Picture = y.User.Picture, isDeleted = y.User.IsDeleted
                        }

                    }).FirstOrDefault();
        }
    }
}