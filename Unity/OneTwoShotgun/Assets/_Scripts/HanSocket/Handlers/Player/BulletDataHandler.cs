using UnityEngine;
using System.Collections.Concurrent;
using HanSocket.VO.Player;
using Characters.Player.Pool;
using System.Collections.Generic;
using Characters.Player;

namespace HanSocket.Handlers.Player
{
   public class BulletDataHandler : HandlerBase
   {
      protected override string Type => "bulletdata";
      private ConcurrentQueue<BulletDataVO> _queue
         = new ConcurrentQueue<BulletDataVO>();

      private Dictionary<int, Shell> _shells
         = new Dictionary<int, Shell>();

      private uint _frame = 0;

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
                  Shell shell;
                  if (!_shells.ContainsKey(e.id))
                  {
                     shell = ShellPool.Instance.Get();
                     _shells.Add(e.id, shell);
                  }
                  else
                     shell = _shells[e.id];

                  shell.Process(_frame, e.id, e.pos);
               });
            }

            foreach (var e in _shells)
            {
               e.Value.CheckFrame(_frame);
            }

            ++_frame;
         }
      }
   }
}