using UnityEngine.SceneManagement;

namespace HanSocket.Handlers.InGame
{
   public class GameStartHandler : HandlerBase
   {
      protected override string Type => "gamestart";

      protected override void OnArrived(string payload)
      {
         
      }

      protected override void OnFlag()
      {
         SceneManager.LoadScene("InGame");
      }
   }
}