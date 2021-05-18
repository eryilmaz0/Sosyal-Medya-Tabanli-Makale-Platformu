using System;
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
    public class EfAUserRepository : EfARepository<User>,IUserRepository
    {
        private readonly ProjectDbContext _context;


        //DI
        public EfAUserRepository(ProjectDbContext context):base(context)
        {
            _context = context;
        }




        public UserDto GetUserProfile(long userId)
        {
           return   _context.Users
                    .AsNoTracking()
                    .IgnoreQueryFilters()
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
                        isDeleted = z.IsDeleted,

                        //NAV PROPS
                        Articles = z.Articles.OrderByDescending(z => z.Created).Take(10).Select(x => new ArticleDto()
                        {
                            Id = x.Id,
                            Title = x.Title,
                            Created = x.Created,
                            isDeleted = z.IsDeleted,

                        }).ToList(),

                        Topics = z.Topics.OrderByDescending(z => z.Created).Take(10).Select(x => new TopicDto()
                        {
                            Id = x.Id,
                            Title = x.Title,
                            Created = x.Created,
                            isDeleted = z.IsDeleted,

                        }).ToList(),


                    }).FirstOrDefault();

        }





    }
}