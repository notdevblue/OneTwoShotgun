using UnityEngine;
using System.Collections.Concurrent;
using HanSocket.VO.Player;

namespace HanSocket.Handlers.Player
{
   public class BulletDataHandler : HandlerBase
   {
      protected override string Type => "bulletdata";
      private ConcurrentQueue<BulletDataVO> _queue
         = new ConcurrentQueue<BulletDataVO>();


      protected override void OnArrived(string payload)
      {
         _queue.Enqueue(JsonUtility.FromJson<BulletDataVO>(payload));
      }

      protected override void OnFlag()
      {
         while (_queue.Count > 0)
         {
            if (_queue.TryDequeue(out var vo))
            {
               vo.bullets.ForEach(e => {
                  
               });
            }
         }
      }
   }
}