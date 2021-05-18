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
    public class EfAArticleRepository : EfARepository<Article>, IArticleRepository
    {
        private readonly ProjectDbContext _context;


        //DI
        public EfAArticleRepository(ProjectDbContext context) : base(context)
        {
            _context = context;
        }



        public List<ArticleDto> GetAllArticles()
        {
            return  _context.Articles
                    .AsNoTracking()
                    .IgnoreQueryFilters()
                    .Include(x => x.User)
                    .Include(x=>x.ArticleCategory)
                    .Include(x => x.ArticleLikeDisslikes)
                    .Select(y => new ArticleDto()
                    {
                        Id = y.Id,
                        Title = y.Title,
                        ViewCount = y.ViewCount,
                        Created = y.Created,
                        isDeleted = y.IsDeleted,
                        Picture = y.Picture,

                        User = new UserDto()
                        {
                            Name = y.User.Name,
                            Lastname = y.User.Lastname,
                            Picture = y.User.Picture,
                            isDeleted = y.User.IsDeleted
                            
                        },


                        ArticleCategory = new ArticleCategoryDto()
                        {
                            Category = y.ArticleCategory.Category,
                            IsDeleted = y.ArticleCategory.IsDeleted
                        },


                        ArticleLikeDisslikes = y.ArticleLikeDisslikes.Select(z => new ArticleLikeDisslikeDto()
                        {
                            LikeDisslikeType = z.LikeDisslikeType,
                            isDeleted = z.IsDeleted

                        }).ToList()

                    }).ToList();
        }




        public ArticleDto GetArticle(long articleId)
        {
            return _context.Articles
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Include(x => x.User)
                .Include(x=>x.ArticleCategory)
                .Include(x => x.ArticleComments).ThenInclude(x => x.User)
                .Include(x => x.ArticleLikeDisslikes)
                .Where(x=> x.Id == articleId)
                .Select(y => new ArticleDto()
                {
                    Id = y.Id,
                    Title = y.Title,
                    Content = y.Content,
                    UserId = y.UserId,
                    Picture = y.Picture,
                    ViewCount = y.ViewCount,
                    Created = y.Created,
                    isDeleted = y.IsDeleted,

                    User = new UserDto()
                    {
                        Name = y.User.Name,
                        Lastname = y.User.Lastname,
                        Picture = y.User.Picture,
                        isDeleted = y.User.IsDeleted
                    },


                    ArticleCategory = new ArticleCategoryDto()
                    {
                        Category = y.ArticleCategory.Category,
                        IsDeleted = y.ArticleCategory.IsDeleted
                    },


                    ArticleComments = y.ArticleComments.Select(z => new ArticleCommentDto()
                    {
                        Id = z.Id,
                        Comment = z.Comment,
                        Created = z.Created,
                        isDeleted = z.IsDeleted,
                        User = new UserDto() { Name = z.User.Name, Lastname = z.User.Lastname, Picture = z.User.Picture }
                    }).ToList(),

                    ArticleLikeDisslikes = y.ArticleLikeDisslikes.Select(p => new ArticleLikeDisslikeDto()
                    {
                        Id = p.Id,
                        LikeDisslikeType = p.LikeDisslikeType,
                        isDeleted = p.IsDeleted

                    }).ToList()

                }).FirstOrDefault();


        }




        public List<ArticleDto> GetMostViewedArticles()
        {
            return   _context.Articles
                    .AsNoTracking()
                    .IgnoreQueryFilters()
                    .Include(x => x.User)
                    .Include(x=>x.ArticleCategory)
                    .Include(x => x.ArticleLikeDisslikes)
                    .OrderByDescending(x => x.ViewCount)
                    .Select(y => new ArticleDto()
                    {
                        Id = y.Id,
                        Title = y.Title,
                        ViewCount = y.ViewCount,
                        Created = y.Created,
                        Picture = y.Picture,
                        isDeleted = y.IsDeleted,

                        User = new UserDto()
                        {
                            Name = y.User.Name,
                            Lastname = y.User.Lastname,
                            Picture = y.User.Picture,
                            isDeleted = y.User.IsDeleted
                        },

                        ArticleCategory = new ArticleCategoryDto()
                        {
                            Category = y.ArticleCategory.Category,
                            IsDeleted = y.ArticleCategory.IsDeleted
                        },

                        ArticleLikeDisslikes = y.ArticleLikeDisslikes.Select(z => new ArticleLikeDisslikeDto()
                        {
                            LikeDisslikeType = z.LikeDisslikeType,
                            isDeleted = z.IsDeleted

                        }).ToList(),



                    }).ToList();
        }




        public List<ArticleDto> GetMostLikedArticles()
        {
            return _context.Articles
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Include(x => x.User)
                .Include(x=>x.ArticleCategory)
                .Include(x => x.ArticleLikeDisslikes)
                .OrderByDescending(x => x.ArticleLikeDisslikes.Where(x => x.LikeDisslikeType == LikeDisslikeType.Like).Count())
                .Select(y => new ArticleDto()
                {
                    Id = y.Id,
                    Title = y.Title,
                    ViewCount = y.ViewCount,
                    Created = y.Created,
                    Picture = y.Picture,
                    isDeleted = y.IsDeleted,

                    User = new UserDto()
                    {
                        Name = y.User.Name,
                        Lastname = y.User.Lastname,
                        Picture = y.User.Picture,
                        isDeleted = y.User.IsDeleted
                    },

                    ArticleCategory = new ArticleCategoryDto()
                    {
                        Category = y.ArticleCategory.Category,
                        IsDeleted = y.ArticleCategory.IsDeleted
                    },

                    ArticleLikeDisslikes = y.ArticleLikeDisslikes.Select(z => new ArticleLikeDisslikeDto()
                    {
                        LikeDisslikeType = z.LikeDisslikeType,
                        isDeleted = z.IsDeleted

                    }).ToList(),



                }).ToList();
        }




        public List<ArticleDto> GetArticlesByUserInterest(long articleCategoryId)
        {
            return _context.Articles
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Include(x => x.User)
                .Include(x => x.ArticleCategory)
                .Include(x => x.ArticleLikeDisslikes)
                .Where(x => x.ArticleCategoryId == articleCategoryId)
                .OrderByDescending(x => x.ArticleLikeDisslikes.Where(x => x.LikeDisslikeType == LikeDisslikeType.Like).Count())
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
                    ArticleLikeDisslikes = y.ArticleLikeDisslikes.Where(y => y.User != null).Select(z => new ArticleLikeDisslikeDto()
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
                .Where(x => x.Title.ToLower().Contains(filter.ToLower()))
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