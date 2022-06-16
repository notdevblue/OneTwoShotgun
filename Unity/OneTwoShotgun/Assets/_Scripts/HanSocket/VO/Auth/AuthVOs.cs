using System;

namespace HanSocket.VO.Auth
{
   [Serializable]
   public class CheckNicknameVO : ValueObject
   {
      public string nickname;

      /// <summary>
      /// type: checknickname
      /// </summary>
      public CheckNicknameVO(string nickname)
      {
         this.nickname = nickname;
      }
   }

   [Serializable]
   public class OwnNickname : ValueObject
   {
      public bool own;

      /// <summary>
      /// type: ownnickname
      /// </summary>
      public OwnNickname(bool own)
      {
         this.own = own;
      }
   }

   [Serializable]
   public class Sign : ValueObject
   {
      public string password;

      /// <summary>
      /// type: sign
      /// </summary>
      public Sign(string password)
      {
         this.password = password;
      }
   }
}