using System;

namespace HanSocket.VO.Player
{   
   [Serializable]
   public class FiredVO : ValueObject
   {
      public int id = -1;
      public float angle = 0.0f;

      /// <summary>
      /// type: fired
      /// </summary>
      public FiredVO(int id, float angle)
      {
         this.id = id;
         this.angle = angle;
      }
   }
}