using UnityEngine;
using HanSocket;
using System.Collections;
using UI;

namespace HanSocket.Handlers.Auth
{
   public class LoginRequestHandler : HandlerBase
   {
      protected override string Type => "loginrequest";

      protected override void OnArrived(string payload)
      {
         
      }

      protected override void OnFlag()
      {
         UIManager.Instance.ActivePanel("password");
      }
   }
}