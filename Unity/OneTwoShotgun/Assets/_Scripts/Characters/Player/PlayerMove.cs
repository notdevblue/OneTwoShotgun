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
      private WaitForSeconds wait;

      [SerializeField] private float fps = 30.0f;

      private void Awake()
      {
         wait = new WaitForSeconds(1.0f / fps);
         StartCoroutine(SendToServer());
      }

      private void Update()
      {
         vo.run = false;

         if (Input.GetKey(UP))
         {
            vo.delta.y = 1;
         }
         if (Input.GetKey(LEFT))
         {
            vo.delta.x = -1;
         }
         if (Input.GetKey(RIGHT))
         {
            vo.delta.x = 1;
         }
         if (Input.GetKey(DOWN))
         {
            vo.delta.y = -1;
         }
         if (Input.GetKey(RUN))
         {
            vo.run = true;
         }
      }

      IEnumerator SendToServer()
      {
         while (true)
         {
            vo.delta.Normalize();
            WebSocketClient.Instance.Send("move", vo.ToJson(), true);
            vo.delta = Vector2.zero;

            yield return wait;
         }
      }

   }
}