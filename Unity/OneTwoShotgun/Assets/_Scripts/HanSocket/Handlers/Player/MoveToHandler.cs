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

         Debug.LogError(vo.id);
         Debug.LogError(vo.pos); 여기 pos 에러 있음
         _queue.Enqueue(vo);
      }

      protected override void OnFlag()
      {
         if (_queue.TryDequeue(out var res))
         {
            if (res.id == UserData.Instance.id) {
               Debug.LogError("AAAA");
               UserData.Instance.player.transform.position = res.pos;
            }
         }
      }
   }
}