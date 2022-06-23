using UnityEngine;
using System.Collections.Concurrent;
using HanSocket.VO.Player;
using HanSocket.Data;
using Characters.Player;

namespace HanSocket.Handlers.Player
{
   public class HitHandler : HandlerBase
   {
      protected override string Type => "hit";
      private ConcurrentQueue<HitVO> _queue = new ConcurrentQueue<HitVO>();

      protected override void OnArrived(string payload)
      {
         _queue.Enqueue(JsonUtility.FromJson<HitVO>(payload));
      }

      protected override void OnFlag()
      {
         while (_queue.Count > 0)
         {
            if (_queue.TryDequeue(out var vo))
            {
               Debug.LogError("피격 처리!!!");
               GameData.Instance.GetUser(vo.id)
                  ?.GetComponent<IDamageable>()
                  ?.SetHP(vo.hp);
            }
         }
      }
   }
}