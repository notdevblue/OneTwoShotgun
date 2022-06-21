using Characters.Pool;
using HanSocket.Data;
using HanSocket.VO.Queue;
using UI.Queue;
using UnityEngine;

namespace HanSocket.Handlers.Queue
{
   public class RoomDataHandler : HandlerBase
   {
      protected override string Type => "roomdata";
      private RoomDataVO vo = null;


      protected override void OnArrived(string payload)
      {
         vo = JsonUtility.FromJson<RoomDataVO>(payload);
      }

      protected override void OnFlag()
      {
         vo.userlist.ForEach(e => {
            User usr = CharacterPool.Instance.Get();
            usr.Init(e.id, e.nickname);
            usr.transform.SetParent(null);
            GameData.Instance.AddUser(usr);
         });

         JoinedCountUI.Instance.UpdateText();
      }
   }
}