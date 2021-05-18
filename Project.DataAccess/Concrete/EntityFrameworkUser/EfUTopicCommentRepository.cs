using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project.DataAccess.Abstract;
using Project.Entities.DbContext;
using Project.Entities.Dtos;
using Project.Entities.Entities;

namespace Project.DataAccess.EntityFrameworkWithSoftDelete
{
    public class EfUTopicCommentRepository : EfURepository<TopicComment>, ITopicCommentRepository
    {
        private readonly ProjectDbContext _context;

        
        //DI
        public EfUTopicCommentRepository(ProjectDbContext context):base(context)
        {
            _context = context;
        }




        public TopicCommentDto GetTopicCommentById(long topicCommentId)
        {
            return  _context.TopicComments
                    .AsNoTracking()
                    .Include(x => x.User)
                    .Where(x => x.Id == topicCommentId)
                    .Select(
                    y => new TopicCommentDto()
                    {
                        Id = y.Id,
                        Comment = y.Comment,
                        Created = y.Created,

                        User = new UserDto()
                        {
                            Id = y.UserId,
                            Name = y.User.Name,
                            Lastname = y.User.Lastname,
                            Picture = y.User.Picture
                        }

                    }).FirstOrDefault();
        }





        public List<TopicCommentDto> GetTopicCommentsByTopic(long topicId)
        {

             return _context
            .TopicComments
            .AsNoTracking()
            .Include(x=>x.User)
            .Where(x=>x.TopicId == topicId && x.User != null)
            .Select(x=> new TopicCommentDto
                {
                    Id=x.Id,
                    Comment = x.Comment,
                    Created = x.Created,
                    User = new UserDto() { Id = x.UserId, Name = x.User.Name, Lastname = x.User.Lastname, Picture = x.User.Picture}

                }).OrderBy(x=>x.Created).ToList();

            }


    }
}
