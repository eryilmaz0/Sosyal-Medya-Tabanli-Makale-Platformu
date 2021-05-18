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
    public class EfUUserRepository : EfURepository<User>, IUserRepository
    {
        private readonly ProjectDbContext _context;


        //DI
        public EfUUserRepository(ProjectDbContext context):base(context)
        {
            _context = context;
        }





        public UserDto GetUserProfile(long userId)
        {
            return  _context.Users
                    .AsNoTracking()
                    .Include(x=>x.Favorites).ThenInclude(x=>x.Article)
                    .Include(x => x.Articles)
                    .Include(x=>x.UserHistories)
                    .Include(x => x.Topics)
                    .Where(y => y.Id == userId)
                    .Select(z => new UserDto()
                    {
                        Id = z.Id,
                        Name = z.Name,
                        Lastname = z.Lastname,
                        Email = z.Email,
                        Gender = z.Gender,
                        Picture = z.Picture,
                        BirthDay = z.BirthDay,
                        Created = z.Created,
                        About=z.About,
                        //NAV PROPS
                        Articles = z.Articles.OrderByDescending(z => z.Created).Take(10).Select(x => new ArticleDto()
                        {
                            Id = x.Id,
                            Title = x.Title,
                            Content = x.Content,
                            Created = x.Created,
                            ArticleCategoryId=x.ArticleCategoryId

                        }).ToList(),


                    }).FirstOrDefault();

            
        }




    }
}
