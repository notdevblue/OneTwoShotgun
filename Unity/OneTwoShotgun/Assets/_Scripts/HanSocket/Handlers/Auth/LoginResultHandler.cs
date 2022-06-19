using HanSocket.VO.Auth;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HanSocket.Handlers.Auth
{

   public class LoginResultHandler : HandlerBase
   {
      protected override string Type => "loginresult";
      private LoginResultVO vo = null;

      protected override void OnArrived(string payload)
      {
         vo = JsonUtility.FromJson<LoginResultVO>(payload);
      }

      protected override void OnFlag()
      {
         if (!vo.success)
         {
            UIManager.Instance.ActivePanel("nickname");
            return;
         }

         SceneManager.LoadScene("QueueMenu");
      }
   }
}