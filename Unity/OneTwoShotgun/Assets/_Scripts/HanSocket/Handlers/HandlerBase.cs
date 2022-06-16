using System.Collections;
using UnityEngine;

namespace HanSocket.Handlers
{   
   abstract public class HandlerBase : MonoBehaviour
   {
      private Flag _flag = new Flag();
      private WaitUntil wait;

      protected void Init(string type)
      {
         wait = new WaitUntil(_flag.Get);

         BufferHandler.Instance.AddHandler(type, payload => {
            OnArrived(payload);
            _flag.Set();
         });

         StartCoroutine(FlagAwaiter());
      }

      private IEnumerator FlagAwaiter()
      {
         while (true)
         {
            yield return wait;
            OnFlag();
         }
      }


      abstract protected void OnArrived(string payload);
      abstract protected void OnFlag();
   }
}