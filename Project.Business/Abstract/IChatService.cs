using Project.Core.Business.BusinessResultObjects;
using Project.Entities.Dtos;
using Project.Entities.Dtos.PostDtos;
using Project.Entities.Entities;
using System.Collections.Generic;

namespace Project.Business.Abstract
{
    public interface IChatService
    {
        IDataResult<List<ChatDto>> GetChatsByUser();
        IDataResult<ChatComment> CreateChatComment(AddChatCommentDto addChatDto);
        IDataResult<List<ChatDto>> GetChatCommentsByChat(long chatId);
        IDataResult<long> GetChatById(long receiverId);
        IDataResult<ChatComment> GetChatCommentById(long chatCommentId);
    }
}
