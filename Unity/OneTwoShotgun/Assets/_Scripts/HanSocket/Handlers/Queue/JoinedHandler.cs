using System.Collections;
using System.Collections.Generic;
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
         GameData.Instance.AddUser(new User(vo.id, vo.nickname));
         JoinedCountUI.Instance.UpdateText();
      }
   }
}