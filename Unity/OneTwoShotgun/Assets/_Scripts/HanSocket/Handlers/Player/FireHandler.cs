using HanSocket.Data;
using HanSocket.VO.Player;
using UnityEngine;

namespace HanSocket.Handlers.Player
{
   public class FireHandler : HandlerBase
   {
      protected override string Type => "fired";
      private FiredVO vo = null;

      protected override void OnArrived(string payload)
      {
         vo = JsonUtility.FromJson<FiredVO>(payload);
      }

      protected override void OnFlag()
      {
         Vector3 dirVector
            = Quaternion.AngleAxis(vo.angle, Vector3.right)
               * Vector3.forward;

         dirVector.x = dirVector.z;
         dirVector.y *= -1.0f;
         dirVector.z = 0.0f;

         User user = GameData.Instance.GetUser(vo.id);

         // TODO: 히히 샷건 발사
      }
   }
}