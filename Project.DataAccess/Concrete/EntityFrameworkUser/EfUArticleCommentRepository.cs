using Project.DataAccess.Abstract;
using Project.Entities.DbContext;
using Project.Entities.Dtos;
using Project.Entities.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Project.DataAccess.EntityFrameworkWithSoftDelete
{
    public class EfUArticleCommentRepository : EfURepository<ArticleComment>, IArticleCommentRepository
    {

        private readonly ProjectDbContext _context;


        //DI
        public EfUArticleCommentRepository(ProjectDbContext context):base(context)
        {
            _context = context;
        }




        public List<ArticleCommentDto> GetArticleCommentsByArticle(long articleId)
        {
            return  _context.ArticleComments
                    .AsNoTracking()
                    .Where(x => x.ArticleId == articleId && x.User != null)
                    .Select(y => new ArticleCommentDto()
                    {
                        Id = y.Id,
                        Comment = y.Comment,
                        Created = y.Created,
                        UserId = y.UserId,
                        User = new UserDto()

                        {
                            Name = y.User.Name, Lastname = y.User.Lastname, Picture = y.User.Picture
                        }

                    }).OrderByDescending(x=>x.Created).ToList();
        }





        public List<ArticleCommentDto> GetArticleCommentsByUser(long userId)
        {
            return _context.ArticleComments
                    .AsNoTracking()
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
                    .Where(x => x.Id == articleCommentId)
                    .Select(y =>
                    new ArticleCommentDto()
                    {
                        Id = y.Id,
                        Comment = y.Comment,
                        Created = y.Created,
                        UserId = y.UserId,

                        User = new UserDto()
                        {
                            Name = y.User.Name, Lastname = y.User.Lastname, Picture = y.User.Picture
                        }

                    }).FirstOrDefault();
        }
    }
}
