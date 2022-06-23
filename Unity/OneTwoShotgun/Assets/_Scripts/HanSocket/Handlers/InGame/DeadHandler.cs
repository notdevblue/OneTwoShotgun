using UnityEngine;

namespace HanSocket.Handlers.InGame
{
   public class DeadHandler : HandlerBase
   {
      protected override string Type => "dead";

      protected override void OnArrived(string payload)
      {
         
      }

      protected override void OnFlag()
      {
         
      }
   }
}