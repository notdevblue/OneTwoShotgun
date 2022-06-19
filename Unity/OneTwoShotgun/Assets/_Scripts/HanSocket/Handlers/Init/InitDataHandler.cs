using UnityEngine;
using HanSocket.VO;
using HanSocket.Data;

namespace HanSocket.Handlers.Init
{

   public class InitDataHandler : HandlerBase
   {
      protected override string Type => "init";
      private InitVO vo = null;

      protected override void OnArrived(string payload)
      {
         vo = JsonUtility.FromJson<InitVO>(payload);
      }

      protected override void OnFlag()
      {
         UserData.Instance.id = vo.id;
      }
   }
}