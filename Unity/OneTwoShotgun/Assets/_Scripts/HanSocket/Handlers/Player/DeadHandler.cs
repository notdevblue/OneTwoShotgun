using UnityEngine;
using HanSocket.VO.Player;
using HanSocket.Data;

namespace HanSocket.Handlers.Player   
{
   public class DeadHandler : HandlerBase
   {
      protected override string Type => "dead";
      private DeadVO vo = null;

      protected override void OnArrived(string payload)
      {
         vo = JsonUtility.FromJson<DeadVO>(payload);
      }

      protected override void OnFlag()
      {
         GameData.Instance.DeleteUser(vo.id);
      }
   }
}