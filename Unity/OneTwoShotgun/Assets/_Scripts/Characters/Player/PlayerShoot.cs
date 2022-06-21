using System.Collections;
using System.Collections.Generic;
using HanSocket;
using HanSocket.VO.Player;
using UnityEngine;

namespace Characters.Player
{
   public class PlayerShoot : MonoBehaviour
   {
      Camera mainCam;

      private void Start()
      {
         mainCam = Camera.main;
      }

      void Update()
      {
         if (Input.GetKeyDown(KeyCode.Mouse0))
         {

            Vector2 delta = mainCam.ScreenToWorldPoint(Input.mousePosition)
                           - transform.position;
            
            float angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;

            WebSocketClient.Instance.Send("fire",
               JsonUtility.ToJson(new FireVO(angle)));
         }
      }
   }
}