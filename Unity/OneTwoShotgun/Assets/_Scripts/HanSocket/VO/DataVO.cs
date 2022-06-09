using System;

namespace HanSocket.VO
{
   [Serializable]
   class DataVO : ValueObject
   {
      public string type;
      public string payload;

      public DataVO(string type, string payload)
      {
         this.type = type;
         this.payload = payload;
      }
   }
}