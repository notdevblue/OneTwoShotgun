using System.Collections;
using System.Collections.Generic;
using HanSocket.Data;
using UI;
using UnityEngine;

namespace HanSocket.Handlers.Auth
{
   public class SignupHandler : HandlerBase
   {
      protected override string Type => "asksignup";

      protected override void OnArrived(string payload)
      {
         
      }

      protected override void OnFlag()
      {
         UserData.Instance.signup = true;
         UIManager.Instance.ActivePanel("signup");
      }
   }
}