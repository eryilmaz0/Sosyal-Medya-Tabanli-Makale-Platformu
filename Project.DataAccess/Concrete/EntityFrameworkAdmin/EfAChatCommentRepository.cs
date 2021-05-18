using Microsoft.EntityFrameworkCore;
using Project.DataAccess.Abstract;
using Project.Entities.DbContext;
using Project.Entities.Dtos;
using Project.Entities.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Project.DataAccess.EntityFramework
{
    public class EfAChatCommentRepository : EfARepository<ChatComment>, IChatCommentRepository
    {
        private readonly ProjectDbContext _context;


        //DI
        public EfAChatCommentRepository(ProjectDbContext context) : base(context)
        {
            _context = context;
        }


        public List<ChatDto> GetChatCommentsByChat(long chatId)
        {
          return _context.Chats
                  .AsNoTracking()
                  .Include(x => x.FirstUser)
                  .Include(x => x.SecondUser)
                  .Include(x => x.ChatComments).ThenInclude(x => x.User)
                  .Where(x => x.Id == chatId)
                  .Select(y => new ChatDto()
                  {
                    Id = y.Id,
                    FirstUser = new UserDto() { Id = y.FirstUser.Id, Name = y.FirstUser.Name, Lastname = y.FirstUser.Lastname, Picture = y.FirstUser.Picture },
                    SecondUser = new UserDto() { Id = y.SecondUser.Id, Name = y.SecondUser.Name, Lastname = y.SecondUser.Lastname, Picture = y.SecondUser.Picture },
                    ChatComments = y.ChatComments.OrderBy(x => x.Created).Select(z => new ChatCommentDto()
                    {
                      User = new UserDto() { Id = z.User.Id, Name = z.User.Name, Lastname = z.User.Lastname, Picture = z.User.Picture },
                      Comment = z.Comment,
                      Created = z.Created

                    }).ToList()


                  }).ToList();
        }




        public ChatComment GetChatCommentById(long chatCommentId)
        {
          return _context.ChatComments
              .AsNoTracking()
              .Include(x => x.User)
              .Include(x => x.Chat)
              .Where(x => x.Id == chatCommentId)
              .OrderByDescending(x => x.Created).FirstOrDefault();
        }
  }
}
