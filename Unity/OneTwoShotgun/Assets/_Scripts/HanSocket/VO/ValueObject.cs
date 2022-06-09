using UnityEngine;
using System;

namespace HanSocket.VO
{
   [Serializable]
   abstract class ValueObject
   {
      public string ToJson()
         => JsonUtility.ToJson(this);
   }
}