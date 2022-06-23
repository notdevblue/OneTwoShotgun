using System;

namespace HanSocket.VO.Queue
{
   [Serializable]
   public class JoinedVO : ValueObject
   {
      public int id;
      public int hp;
      public string nickname;

      /// <summary>
      /// type; joiend
      /// </summary>
      public JoinedVO(int id, string nickname)
      {
         this.id = id;
         this.nickname = nickname;
      }
   }
}