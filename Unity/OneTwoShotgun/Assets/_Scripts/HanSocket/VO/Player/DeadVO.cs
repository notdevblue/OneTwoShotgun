using System;

namespace HanSocket.VO.Player
{   
   [Serializable]
   public class DeadVO : ValueObject
   {
      public int id;

      /// <summary>
      /// type: dead
      /// </summary>
      public DeadVO(int id)
      {
         this.id = id;
      }
   }
}