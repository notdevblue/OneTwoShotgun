using System;

namespace HanSocket.VO.Player
{   
   [Serializable]
   public class FireVO
   {
      public float angle = 0.0f;

      /// <summary>
      /// type: fire
      /// </summary>
      public FireVO(float angle)
      {
         this.angle = angle;
      }
   }
}