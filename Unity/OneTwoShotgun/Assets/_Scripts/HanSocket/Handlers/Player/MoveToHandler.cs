using HanSocket.Data;
using HanSocket.VO.Player;
using System.Collections.Concurrent;
using UnityEngine;

namespace HanSocket.Handlers.Player
{
   public class MoveToHandler : HandlerBase
   {
      protected override string Type => "moveto";
      private MoveToVO vo = null;

      private ConcurrentQueue<MoveToVO> _queue = new ConcurrentQueue<MoveToVO>();

      protected override void OnArrived(string payload)
      {
         vo = JsonUtility.FromJson<MoveToVO>(payload);

         _queue.Enqueue(vo);
      }

      protected override void OnFlag()
      {
         while (_queue.Count > 0)
         {
            if (_queue.TryDequeue(out var res))
            {
               if (vo.id == UserData.Instance.id)
               {
                  if (UserData.Instance.Player != null)
                     UserData.Instance.Player.transform.position = res.pos;
               }
               else
               {
                  User user = GameData.Instance.GetUser(res.id);
                  if (user != null)
                     user.transform.position = res.pos;
               }
            }
         }
      }
   }
}