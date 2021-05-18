using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Project.Business.Abstract;
using Project.Entities.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Project.Business.RealTimeApps.SignalR;
using Project.Core.Extensions;

namespace Project.Business.RealTimeApps.SignalR
{
  [Authorize]
  public class ChatHub : Hub
  {

    //BİR BAĞLANTI GERÇEKLEŞTİĞİNDE TETİKLENECEK METHOD
    public override async Task OnConnectedAsync()
    {
      var claims = Context.User.Claims;
      var userId = Convert.ToInt64(claims.FirstOrDefault(x => x.Type == "userId").Value);

      //HER BİR CLIENT BAĞLANTI GERÇEKLEŞTİRDİĞİNDE, CONID'Yİ SİSTEME EKLE
      SignalRUserConnections.Add(userId, Context.ConnectionId);

      await base.OnConnectedAsync();
    }



    public async Task SendLastAddedMessage(ChatComment chatComment, long receiverId)
    {
      var claims = Context.User.Claims;
      var userId = Convert.ToInt64(claims.FirstOrDefault(x => x.Type == "userId").Value);



      //KULLANICILARI DOĞRULA
      if (!CheckUsers(chatComment.Chat, userId, receiverId))
      {
        await Task.CompletedTask;
      }


      //HEDEF USER CONNECTION ID 
      string targetConnectionId = SignalRUserConnections.GetConnection(receiverId);
      if (string.IsNullOrEmpty(targetConnectionId))
      {
        await Task.CompletedTask;
      }


      //KULLANICILARA GÖNDER
      await Clients.Client(targetConnectionId).SendAsync("chatChannel", JsonSerializeExtensions.CamelCaseSerialize(chatComment));
      await Clients.Caller.SendAsync("chatChannel", JsonSerializeExtensions.CamelCaseSerialize(chatComment));
    }




    private bool CheckUsers(Chat chat, long senderId, long receiverId)
    {
      return (chat.FirstUserId == senderId && chat.SecondUserId == receiverId) ||
             (chat.FirstUserId == receiverId && chat.SecondUserId == senderId)
              ? true
              : false;
    }
  }
}
