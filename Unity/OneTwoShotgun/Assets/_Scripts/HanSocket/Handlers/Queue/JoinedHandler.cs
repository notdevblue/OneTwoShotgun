using System.Collections;
using System.Collections.Generic;
using HanSocket.VO.Queue;
using UI.Queue;
using UnityEngine;

namespace HanSocket.Handlers.Queue
{
   public class JoinedHandler : HandlerBase
   {
      protected override string Type => "joined";
      private JoinedCountUI _joinedCountUI = null;
      private JoinedVO vo = null;

      private void Start()
      {
         _joinedCountUI = FindObjectOfType<JoinedCountUI>();
      }

      protected override void OnArrived(string payload)
      {
         vo = JsonUtility.FromJson<JoinedVO>(payload);
      }

      protected override void OnFlag()
      {
         유저 접속 처리 해야 함
      }
   }
}