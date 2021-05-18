using Project.Core.DataAccess.Repository;
using Project.Entities.Dtos;
using Project.Entities.Entities;
using System.Collections.Generic;

namespace Project.DataAccess.Abstract
{
    public interface IChatCommentRepository : IRepository<ChatComment>
    {
      public List<ChatDto> GetChatCommentsByChat(long chatId);
      public ChatComment GetChatCommentById(long chatCommentId);
    }
}
