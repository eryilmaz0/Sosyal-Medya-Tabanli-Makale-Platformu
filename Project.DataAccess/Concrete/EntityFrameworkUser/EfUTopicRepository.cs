using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project.DataAccess.Abstract;
using Project.Entities.DbContext;
using Project.Entities.Dtos;
using Project.Entities.Entities;

namespace Project.DataAccess.EntityFrameworkWithSoftDelete
{
    public class EfUTopicRepository : EfURepository<Topic>, ITopicRepository
    {
        private readonly ProjectDbContext _context;


        //DI
        public EfUTopicRepository(ProjectDbContext context):base(context)
        {
            _context = context;
        }




        public List<TopicDto> GetAllTopicsWithInclude()
        {
            return  _context.Topics
                    .AsNoTracking()
                    .Include(x => x.User)
                    .Select(y => new TopicDto()
                {
                    Id = y.Id,
                    Title = y.Title,
                    Created = y.Created,

                    User = new UserDto()
                    {
                        Id = y.User.Id,
                        Name = y.User.Name,
                        Lastname = y.User.Lastname,
                        Picture = y.User.Picture
                    }

                }).OrderByDescending(x=>x.Created).ToList();
        }





        public TopicDto GetTopicWithInclude(long topicId)
        {
            return  _context.Topics
                    .AsNoTracking()
                    .Include(x => x.User)
                    .Include(x => x.TopicComments).ThenInclude(x => x.User)
                    .Where(x => x.Id == topicId)
                    .Select(y => new TopicDto()
                    {
                        Id = y.Id,
                        Title = y.Title,
                        Content = y.Content,
                        Created = y.Created,

                        User = new UserDto()
                        {
                            Id = y.UserId,
                            Name = y.User.Name,
                            Lastname = y.User.Lastname,
                            Picture = y.User.Picture,
                        },

                        //CHECK  USER != NULL
                        TopicComments = y.TopicComments.Select(z => new TopicCommentDto()
                        {
                            Id = z.Id,
                            Comment = z.Comment,
                            Created = z.Created,

                            User = new UserDto()
                            {
                                Id = z.UserId,
                                Name = z.User.Name,
                                Lastname = z.User.Lastname,
                                Picture = z.User.Picture
                            }

                        }).ToList()

                    }).FirstOrDefault();
        }
    }
}
