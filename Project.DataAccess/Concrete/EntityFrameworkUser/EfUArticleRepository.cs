using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Project.Core.ValidationErrorObjects;
using Project.DataAccess.Abstract;
using Project.Entities.DbContext;
using Project.Entities.Dtos;
using Project.Entities.Entities;
using Project.Entities.Enums;

namespace Project.DataAccess.EntityFrameworkWithSoftDelete
{
    public class EfUArticleRepository : EfURepository<Article>, IArticleRepository
    {
        private readonly ProjectDbContext _context;

        //DI
        public EfUArticleRepository(ProjectDbContext context):base(context)
        {
            _context = context;
        }



        public List<ArticleDto> GetAllArticles()
        {
            return  _context.Articles
                    .AsNoTracking()
                    .Include(x => x.User)
                    .Include(x => x.ArticleLikeDisslikes)
                    .Include(x=>x.ArticleCategory)
                    .Select(y => new ArticleDto()
                    {
                        Id = y.Id,
                        Content=y.Content,
                        Title = y.Title,
                        ViewCount = y.ViewCount,
                        Created = y.Created,
                        Picture = y.Picture,

                        User = new UserDto()
                        {
                            Name = y.User.Name,
                            Lastname = y.User.Lastname,
                            Picture = y.User.Picture
                        },

                        ArticleCategory = new ArticleCategoryDto()
                        {
                            Id = y.ArticleCategoryId,
                            Category = y.ArticleCategory.Category
                        },

                        ArticleLikeDisslikes =  y.ArticleLikeDisslikes.Where(y=>y.User != null).Select(z=> new ArticleLikeDisslikeDto()
                        {
                            LikeDisslikeType = z.LikeDisslikeType

                        }).ToList(),

                        ArticleComments = y.ArticleComments.Select(x=>new ArticleCommentDto()
                        {
                            Id = x.Id
                        }).ToList()

                        

                    }).ToList();
        }





        public ArticleDto GetArticle(long articleId)
        {
            return _context.Articles
                .AsNoTracking()
                .Include(x => x.User)
                .Include(x=>x.ArticleCategory)
                .Include(x => x.ArticleComments).ThenInclude(x => x.User)
                .Include(x=>x.Favorites)
                .Include(x => x.ArticleLikeDisslikes)
                .Where(x=> x.Id == articleId)
                .Select(y => new ArticleDto()
                {
                    Id = y.Id,
                    Title = y.Title,
                    Content = y.Content,
                    ArticleFile=y.ArticleFile,
                    UserId = y.UserId,
                    Picture = y.Picture,
                    ViewCount = y.ViewCount,
                    Created = y.Created,

                    User = new UserDto()
                    {
                        Name = y.User.Name,
                        Lastname = y.User.Lastname,
                        Picture = y.User.Picture
                    },

                    ArticleCategory = new ArticleCategoryDto()
                    {
                        Id = y.ArticleCategory.Id,
                        Category = y.ArticleCategory.Category
                    },
                    
                    //ArticleComments = y.ArticleComments.Select(z => new ArticleCommentDto()
                    //{
                    //    Id = z.Id,
                    //    Comment = z.Comment,
                    //    Created = z.Created,
                    //    User = new UserDto() {Name = z.User.Name, Lastname = z.User.Lastname, Picture = z.User.Picture}
                    //}).ToList(),

                    Favorites=y.Favorites.Where(p => p.User != null).Select(p=> new FavoriteDto() {
                      Id=p.Id,
                      UserId = p.UserId
                    }).ToList(),

                    ArticleLikeDisslikes = y.ArticleLikeDisslikes.Where(p=>p.User != null).Select(p => new ArticleLikeDisslikeDto()
                    {
                        Id = p.Id,
                        LikeDisslikeType = p.LikeDisslikeType,
                        UserId=p.UserId
                    }).ToList()


                }).FirstOrDefault();


        }





        public List<ArticleDto> GetMostViewedArticles()
        {
            return   _context.Articles
                    .AsNoTracking()
                    .Include(x => x.User)
                    .Include(x=>x.ArticleCategory)
                    .Include(x => x.ArticleLikeDisslikes)
                    .OrderByDescending(x=>x.ViewCount)
                    .Select(y => new ArticleDto()
                    {
                        Id = y.Id,
                        Title = y.Title,
                        Content = y.Content,
                        ViewCount = y.ViewCount,
                        Created = y.Created,
                        Picture = y.Picture,

                        User = new UserDto()
                        {
                            Name = y.User.Name,
                            Lastname = y.User.Lastname,
                            Picture = y.User.Picture
                        },

                        ArticleCategory = new ArticleCategoryDto()
                        {
                            Category = y.ArticleCategory.Category
                        },

                        ArticleLikeDisslikes = y.ArticleLikeDisslikes.Where(z=>z.User != null).Select(z => new ArticleLikeDisslikeDto()
                        {
                            LikeDisslikeType = z.LikeDisslikeType

                        }).ToList(),



                    }).ToList();
        }





        public List<ArticleDto> GetMostLikedArticles()
        {
            return   _context.Articles
                    .AsNoTracking()
                    .Include(x => x.User)
                    .Include(x=>x.ArticleCategory)
                    .Include(x => x.ArticleLikeDisslikes)
                    .OrderByDescending(x => x.ArticleLikeDisslikes.Where(x=>x.LikeDisslikeType == LikeDisslikeType.Like).Count())
                    .Select(y => new ArticleDto()
                    {
                        Id = y.Id,
                        Title = y.Title,
                        Content = y.Content,
                        ViewCount = y.ViewCount,
                        Created = y.Created,
                        Picture = y.Picture,

                        User = new UserDto()
                        {
                            Name = y.User.Name,
                            Lastname = y.User.Lastname,
                            Picture = y.User.Picture
                        },


                        ArticleCategory = new ArticleCategoryDto()
                        {
                            Category = y.ArticleCategory.Category
                        },

                        //!!
                        ArticleLikeDisslikes = y.ArticleLikeDisslikes.Where(y=>y.User != null).Select(z => new ArticleLikeDisslikeDto()
                        {
                            LikeDisslikeType = z.LikeDisslikeType

                        }).ToList(),

                    }).ToList();
        }





        public List<ArticleDto> GetArticlesByUserInterest(long articleCategoryId)
        {
            return _context.Articles
                .AsNoTracking()
                .Include(x => x.User)
                .Include(x => x.ArticleCategory)
                .Include(x => x.ArticleLikeDisslikes)
                .Where(x=>x.ArticleCategoryId == articleCategoryId)
                .OrderByDescending(x => x.ViewCount)
                .Select(y => new ArticleDto()
                {
                    Id = y.Id,
                    Title = y.Title,
                    Content = y.Content,
                    ViewCount = y.ViewCount,
                    Created = y.Created,
                    Picture = y.Picture,

                    User = new UserDto()
                    {
                        Id = y.UserId,
                        Name = y.User.Name,
                        Lastname = y.User.Lastname,
                        Picture = y.User.Picture
                    },

                    ArticleCategory = new ArticleCategoryDto()
                    {
                        Category = y.ArticleCategory.Category
                    },

                    ArticleLikeDisslikes = y.ArticleLikeDisslikes.Where(z => z.User != null).Select(z => new ArticleLikeDisslikeDto()
                    {
                        LikeDisslikeType = z.LikeDisslikeType

                    }).ToList(),



                }).Take(5).ToList();
        }





        public List<ArticleDto> GetFilteredArticles(string filter)
        {
            return _context.Articles
                .AsNoTracking()
                .Include(x => x.User)
                .Include(x => x.ArticleCategory)
                .Include(x => x.ArticleLikeDisslikes)
                .Where(x=>x.Title.ToLower().Contains(filter.ToLower()))
                .OrderByDescending(x => x.ViewCount)
                .Select(y => new ArticleDto()
                {
                    Id = y.Id,
                    Title = y.Title,
                    Content = y.Content,
                    ViewCount = y.ViewCount,
                    Created = y.Created,
                    Picture = y.Picture,

                    User = new UserDto()
                    {
                        Id = y.UserId,
                        Name = y.User.Name,
                        Lastname = y.User.Lastname,
                        Picture = y.User.Picture
                    },

                    ArticleCategory = new ArticleCategoryDto()
                    {
                        Category = y.ArticleCategory.Category
                    },

                    ArticleLikeDisslikes = y.ArticleLikeDisslikes.Where(z => z.User != null).Select(z => new ArticleLikeDisslikeDto()
                    {
                        LikeDisslikeType = z.LikeDisslikeType

                    }).ToList(),



                }).ToList();
        }
    }
}
