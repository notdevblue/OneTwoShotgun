using UnityEngine;
using HanSocket;
using System.Collections;

namespace HanSocket.Handlers.Auth
{
   public class LoginRequestHandler : HandlerBase
   {
      private void Awake()
      {
         Init("loginrequest");
      }

      protected override void OnArrived(string payload)
      {
         Debug.Log("A")
      }

      protected override void OnFlag()
      {
         
      }
   }
}