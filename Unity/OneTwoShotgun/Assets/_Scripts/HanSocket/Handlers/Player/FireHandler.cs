using HanSocket.Data;
using HanSocket.VO.Player;
using UnityEngine;
using System.Collections.Concurrent;
using Characters.Player.Pool;

namespace HanSocket.Handlers.Player
{
   public class FireHandler : HandlerBase
   {
      protected override string Type => "fired";

      private ConcurrentQueue<FiredVO> _queue
         = new ConcurrentQueue<FiredVO>();

      protected override void OnArrived(string payload)
      {
         _queue.Enqueue(JsonUtility.FromJson<FiredVO>(payload));
      }

      protected override void OnFlag()
      {
         while (_queue.Count > 0)
         {
            if (_queue.TryDequeue(out var vo))
            {
               vo.angles.ForEach(e => {
                  Vector3 dirVector
                     = Quaternion.AngleAxis(e, Vector3.right)
                        * Vector3.forward;

                  dirVector.x = dirVector.z;
                  dirVector.y *= -1.0f;
                  dirVector.z = 0.0f;

                  User user = GameData.Instance.GetUser(vo.id);
                  Debug.DrawRay(vo.firedPos, dirVector * 1.0f, Color.red, 1.0f);

                  // ShellPool.Instance.Get().Fire(vo.firedPos, dirVector, vo.speed, vo.id, vo.alivefor);
               });
            }
         }
      }
   }
}