using System.Collections;
using System.Collections.Generic;
using HanSocket;
using HanSocket.VO.Player;
using UnityEngine;

namespace Characters.Player
{
   public class PlayerMove : MonoBehaviour
   {
      const KeyCode UP     = KeyCode.W;
      const KeyCode LEFT   = KeyCode.A;
      const KeyCode RIGHT  = KeyCode.D;
      const KeyCode DOWN   = KeyCode.S;
      const KeyCode RUN    = KeyCode.LeftShift;

      private MoveVO vo = new MoveVO(Vector2.zero);

      private void Update()
      {
         vo.delta = Vector2.zero;
         vo.run = false;

         if (Input.GetKey(UP))
         {
            vo.delta.y += 1;
         }
         if (Input.GetKey(LEFT))
         {
            vo.delta.x -= 1;
         }
         if (Input.GetKey(RIGHT))
         {
            vo.delta.x += 1;
         }
         if (Input.GetKey(DOWN))
         {
            vo.delta.y -= 1;
         }
         if (Input.GetKey(RUN))
         {
            vo.run = true;
         }

         vo.delta.Normalize();

         WebSocketClient.Instance.Send("move", vo.ToJson());
      }
   }
}