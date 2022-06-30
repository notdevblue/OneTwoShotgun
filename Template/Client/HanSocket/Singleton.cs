using System;

namespace HanSocket
{
   abstract public class Singleton<T> where T : Singleton<T>
   {
      private static T _instance;
      public static T Instance
      {
         get
         {
            if (_instance == null)
            {
               _instance = Activator.CreateInstance<T>();
               GC.KeepAlive(_instance);
            }

            return _instance;
         }
      }
   }
}