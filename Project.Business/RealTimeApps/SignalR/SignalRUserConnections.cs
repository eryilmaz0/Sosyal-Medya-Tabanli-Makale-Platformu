using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Business.RealTimeApps.SignalR
{
  public static class SignalRUserConnections
  {
    //USERID VE CONNECTIONID TUTAN DICTIONARY
    private static readonly Dictionary<long, string> _userConnections = new Dictionary<long, string>();




    public static void Add(long userId, string connectionId)
    {

      string existConnectionId;
      _userConnections.TryGetValue(userId, out existConnectionId);


      //CONNECTIONID HALA GEÇERLİ
      if (!string.IsNullOrEmpty(existConnectionId) && existConnectionId == connectionId)
      {
        return;
      }


      //DICTIONARY'E DAHA ÖNCE EKLENMİŞ, GEÇERSİZ
      if (!string.IsNullOrEmpty(existConnectionId) && existConnectionId != connectionId)
      {
        _userConnections.Remove(userId);
        //ESKİSİNİ SİL
      }

      //DAHA ÖNCE EKLENMEMİŞ
      _userConnections.Add(userId, connectionId);
    }




    public static string GetConnection(long userId)
    {
      //VARSA CONNECTIONID'Yİ, YOKSA NULL DÖN
      return _userConnections.FirstOrDefault(x => x.Key == userId).Value;
    }
  }
}
