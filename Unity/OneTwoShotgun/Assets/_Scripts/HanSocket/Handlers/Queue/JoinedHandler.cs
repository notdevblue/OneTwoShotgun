using System.Collections;
using System.Collections.Generic;
using Characters.Player;
using Characters.Pool;
using HanSocket.Data;
using HanSocket.VO.Queue;
using UI.Queue;
using UnityEngine;

namespace HanSocket.Handlers.Queue
{
   public class JoinedHandler : HandlerBase
   {
      protected override string Type => "joined";
      private JoinedVO vo = null;

      protected override void OnArrived(string payload)
      {
         vo = JsonUtility.FromJson<JoinedVO>(payload);
      }

      protected override void OnFlag()
      {
         User usr = CharacterPool.Instance.Get();
         usr.Init(vo.id, vo.hp, vo.nickname, vo.pos);

         if (vo.id == UserData.Instance.id)
         {
            Camera cam = Camera.main;

            usr.gameObject.AddComponent<PlayerMove>();
            usr.gameObject.AddComponent<PlayerShoot>();
            cam.transform.SetParent(usr.transform);
            cam.transform.localPosition = new Vector3(0.0f, 0.0f, -10.0f);
            cam.farClipPlane = 30.0f;

            UserData.Instance.Player = usr;
         }

         usr.transform.SetParent(null);

         GameData.Instance.AddUser(usr);
         JoinedCountUI.Instance.UpdateText();
      }
   }
}