using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Business.Abstract;
using Project.Core.Business.BusinessResultObjects.Enums;
using Project.Entities.Dtos.PostDtos;
using Project.WebAPI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class ChatsController : ControllerBase
  {
    private readonly IChatService _chatService;


    //DI
    public ChatsController(IChatService chatService)
    {
      _chatService = chatService;
    }



    //(TÜM KONUŞMALAR) ama mesaj yok içinde
    [HttpGet]
    public IActionResult GetChatsByCurrentUser()
    {

      var result = _chatService.GetChatsByUser();

      if (result.ResultType == ResultType.UnAuthorized)
      {
        return new UnauthorizedResult();
      }


      if (result.ResultType == ResultType.Success)
      {
        return Ok(result);
      }


      return BadRequest(result.Message);
    }




    //(MESAJ GÖNDER)
    [HttpPost]
    [ValidationFilter]
    public IActionResult CreateChatComment(AddChatCommentDto addChatCommentDto)
    {
      var result = _chatService.CreateChatComment(addChatCommentDto);


      if (result.ResultType == ResultType.UnAuthorized)
      {
        return Unauthorized();
      }


      if (result.ResultType == ResultType.Success)
      {
        return Ok(result);
      }


      return BadRequest(result.Message);
    }



    //(KONUŞMA VAR MI YOK MU KONTROL İÇİN) ıd gönderdigin konuşmayı ceker
    //HEDEF KULLANICI İLE OLAN KONUŞMASI VAR MI? VAR İSE ID DÖN
    [HttpGet]
    [Route("{receiverId}")]
    public IActionResult GetChatById(long receiverId)
    {
      var result = _chatService.GetChatById(receiverId);


      if (result.ResultType == ResultType.UnAuthorized)
      {
        return Unauthorized();
      }

      if (result.ResultType == ResultType.Success)
      {
        return Ok(result);
      }


      return BadRequest(result.Message);
    }





    //(MESAJLAR)
    [HttpGet]
    [Route("{chatId}/Comments")]
    public IActionResult GetChatCommentsByChat(long chatId)
    {
      var result = _chatService.GetChatCommentsByChat(chatId);


      if (result.ResultType == ResultType.UnAuthorized)
      {
        return Unauthorized();
      }


      if (result.ResultType == ResultType.Success)
      {
        return Ok(result);
      }


      return BadRequest(result.Message);
    }
  }
}
