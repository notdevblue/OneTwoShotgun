using HanSocket.Data;
using HanSocket.VO.Player;
using System.Collections.Concurrent;
using UnityEngine;

namespace HanSocket.Handlers.Player
{
   public class MoveToHandler : HandlerBase
   {
      protected override string Type => "moveto";

      private ConcurrentQueue<MoveToVO> _queue = new ConcurrentQueue<MoveToVO>();

      protected override void OnArrived(string payload)
      {
         _queue.Enqueue(JsonUtility.FromJson<MoveToVO>(payload));
      }

      protected override void OnFlag()
      {
         while (_queue.Count > 0)
         {
            if (_queue.TryDequeue(out var res))
            {
               GameData.Instance.GetUser(res.id)?.SetTargetPos(res.pos);
            }
         }
      }
   }
}