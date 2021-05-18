using Project.Core.DataAccess.Repository;
using Project.Entities.Dtos;
using Project.Entities.Entities;
using System.Collections.Generic;

namespace Project.DataAccess.Abstract
{
    public interface IChatRepository : IRepository<Chat>
    {
      public List<ChatDto> GetChatsByUser(long userId);
    }
}
