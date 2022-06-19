using System;

namespace HanSocket.VO
{
   [Serializable]
   public class InitVO : ValueObject
   {
      public int id;

      public InitVO(int id)
      {
         this.id = id;
      }
   }
}