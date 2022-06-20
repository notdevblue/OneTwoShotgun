using HanSocket.Data;
using HanSocket.VO.Queue;
using UI.Queue;
using UnityEngine;

namespace HanSocket.Handlers.Queue
{
   public class LeftHandler : HandlerBase
   {
      protected override string Type => "left";
      private LeftVO vo = null;

      protected override void OnArrived(string payload)
      {
         vo = JsonUtility.FromJson<LeftVO>(payload);
      }

      protected override void OnFlag()
      {
         GameData.Instance.DeleteUser(vo.id);
         JoinedCountUI.Instance.UpdateText();
      }
   }
}