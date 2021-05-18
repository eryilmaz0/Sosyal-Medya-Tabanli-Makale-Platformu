using Project.Business.Abstract;
using Project.Business.Messages;
using Project.Core.Business.BusinessResultObjects;
using Project.Core.Utilities.Business;
using Project.DataAccess.UnitOfWork;
using Project.Entities.Dtos;
using Project.Entities.Dtos.PostDtos;
using Project.Entities.Entities;
using System.Collections.Generic;

namespace Project.Business.Concrete
{
    public class ChatManager : IChatService
    {
    private readonly IProjectUnitOfWork _uow;
    private readonly IAuthService _authService;


    //DI
    public ChatManager(IProjectUnitOfWork uow, IAuthService authService)
    {
      _uow = uow;
      _authService = authService;
    }




    public IDataResult<List<ChatDto>> GetChatsByUser()
    {
      var errorResult = BusinessRules.Run(CheckAuthenticatedUserExist());

      if (errorResult != null)
      {
        return ResultConverter.ResultToDataResult<List<ChatDto>>(errorResult);
      }

      var user = _authService.GetAuthenticatedUser().Result.Data;

      return new SuccessDataResult<List<ChatDto>>(_uow.Chats.GetChatsByUser(user.Id));
    }





    public IDataResult<ChatComment> CreateChatComment(AddChatCommentDto addChatDto)
    {
      var errorResult = BusinessRules.Run(CheckAuthenticatedUserExist(), CheckReceiverIsExist(addChatDto.ReceiverId));

      if (errorResult != null)
      {
        return ResultConverter.ResultToDataResult<ChatComment>(errorResult);
      }


      var user = _authService.GetAuthenticatedUser().Result.Data;
      var chat = GetChatById(user.Id, addChatDto.ReceiverId).Data;


      if (chat == null)
      {
        var result = CreateChatwithComment(user, addChatDto);
        return new SuccessDataResult<ChatComment>(GetChatCommentById(result.Data).Data, Message.ChatCommentCreated);
      }


      var chatComment = new ChatComment() { UserId = user.Id, ChatId = chat.Id, Comment = addChatDto.Content };
      _uow.ChatComments.Add(chatComment);
      _uow.Commit();
      return new SuccessDataResult<ChatComment>(GetChatCommentById(chatComment.Id).Data, Message.ChatCommentCreated);

    }





    public IDataResult<List<ChatDto>> GetChatCommentsByChat(long chatId)
    {
      var user = _authService.GetAuthenticatedUser().Result.Data;

      var errorResult = BusinessRules.Run(CheckAuthenticatedUserExist(), CheckIsChatExist(chatId, user.Id));

      if (errorResult != null)
      {
        return ResultConverter.ResultToDataResult<List<ChatDto>>(errorResult);
      }


      return new SuccessDataResult<List<ChatDto>>(_uow.ChatComments.GetChatCommentsByChat(chatId));

    }




    //IDSİ GELEN KULLANICI İLE TOKENDAKİ KULLANICININ KONUŞMA GEÇMİŞİ VAR MI ? VAR İSE CHATID DÖN
    public IDataResult<long> GetChatById(long receiverId)
    {
      var errorResult = BusinessRules.Run(CheckAuthenticatedUserExist());

      if (errorResult != null)
      {
        return ResultConverter.ResultToDataResult<long>(errorResult);
      }


      return new SuccessDataResult<long>(CheckIsChatExistBetweenUsers(_authService.GetAuthenticatedUser().Result.Data.Id, receiverId).Data);
    }





    public IDataResult<ChatComment> GetChatCommentById(long chatCommentId)
    {
      return new SuccessDataResult<ChatComment>(_uow.ChatComments.GetChatCommentById(chatCommentId));
    }





    private IDataResult<long> CreateChatwithComment(User user, AddChatCommentDto addChatDto)
    {
      var chat = new Chat() { FirstUserId = user.Id, SecondUserId = addChatDto.ReceiverId };
      var chatComment = new ChatComment() { UserId = user.Id, Comment = addChatDto.Content };


      chat.ChatComments.Add(chatComment);

      _uow.Chats.Add(chat);
      _uow.Commit();

      return new SuccessDataResult<long>(chatComment.Id);
    }



    //


    private IResult CheckAuthenticatedUserExist()
    {

      var user = _authService.GetAuthenticatedUser().Result.Data;

      if (user == null)
      {
        return new UnauthorizedResult();
      }

      return new SuccessResult();

    }





    private IResult CheckIsChatExist(long chatId, long userId)
    {
      var chat = _uow.Chats.Get(x => x.Id == chatId);

      if ((chat == null) ||
          ((chat.FirstUserId != userId) && (chat.SecondUserId != userId) ||
           ((_authService.GetUser(chat.FirstUserId).Result.Data == null) || _authService.GetUser(chat.SecondUserId).Result.Data == null)))

      {
        return new ErrorResult(Message.ChatNotFound);
      }

      return new SuccessResult();
    }





    private IDataResult<long> CheckIsChatExistBetweenUsers(long authenticatedUserId, long receiverId)
    {
      var chat = _uow.Chats.Get(x => x.FirstUserId == authenticatedUserId && x.SecondUserId == receiverId || x.SecondUserId == authenticatedUserId && x.FirstUserId == receiverId);

      if(chat == null)
      {
        return new SuccessDataResult<long>(0);
      }

      return new SuccessDataResult<long>(chat.Id);
    }




    private IDataResult<Chat> GetChatById(long senderId, long receiverId)
    {
      var chat = _uow.Chats.Get(x =>
          (x.FirstUserId == senderId && x.SecondUserId == receiverId) ||
          (x.FirstUserId == receiverId && x.SecondUserId == senderId));

      if (chat == null)
      {
        return new ErrorDataResult<Chat>();
      }

      return new SuccessDataResult<Chat>(chat);


    }




    private IResult CheckReceiverIsExist(long receiverId)
    {
      var receiver = _authService.GetUser(receiverId).Result.Data;

      if (receiver == null)
      {
        return new ErrorResult(Message.ReceiverNotFound);
      }

      return new SuccessResult();
    }
  }
}
