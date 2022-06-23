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
         usr.Init(vo.id, vo.hp, vo.nickname);
         
         if (vo.id == UserData.Instance.id)
         {
            usr.gameObject.AddComponent<PlayerMove>();
            usr.gameObject.AddComponent<PlayerShoot>();
            usr.gameObject.AddComponent<PlayerHealth>();
            UserData.Instance.Player = usr;
         }

         usr.transform.SetParent(null);

         GameData.Instance.AddUser(usr);
         JoinedCountUI.Instance.UpdateText();
      }
   }
}