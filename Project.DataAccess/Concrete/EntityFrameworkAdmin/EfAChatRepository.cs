using Microsoft.EntityFrameworkCore;
using Project.DataAccess.Abstract;
using Project.Entities.DbContext;
using Project.Entities.Dtos;
using Project.Entities.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Project.DataAccess.EntityFramework
{
    public class EfAChatRepository : EfARepository<Chat>, IChatRepository
    {
        private readonly ProjectDbContext _context;


        //DI
        public EfAChatRepository(ProjectDbContext context) : base(context)
        {
            _context = context;
        }


       public List<ChatDto> GetChatsByUser(long userId)
    {
      return _context.Chats
              .AsNoTracking()
              .Include(x => x.FirstUser)
              .Include(x => x.SecondUser)
              .Include(x => x.ChatComments)
              .Where(x => x.FirstUserId == userId || x.SecondUserId == userId)
              .Select(y => new ChatDto()
              {
                Id = y.Id,
                Created = y.Created,
                FirstUser = new UserDto() { Id = y.FirstUserId, Name = y.FirstUser.Name, Lastname = y.FirstUser.Lastname, Picture = y.FirstUser.Picture },
                SecondUser = new UserDto() { Id = y.SecondUserId, Name = y.SecondUser.Name, Lastname = y.SecondUser.Lastname, Picture = y.SecondUser.Picture },
                FirstComment = y.ChatComments.OrderByDescending(x => x.Created).FirstOrDefault()  //HER CHATIN İLK MESAJI

              }).OrderByDescending(x => x.FirstComment.Created).ToList();
    }
  }
}
