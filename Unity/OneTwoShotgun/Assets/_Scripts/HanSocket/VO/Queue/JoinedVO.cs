using System;

namespace HanSocket.VO.Queue
{
   [Serializable]
   public class JoinedVO : ValueObject
   {
      public int id;
      public string nickname;

      public JoinedVO(int id, string nickname)
      {
         this.id = id;
         this.nickname = nickname;
      }
   }
}