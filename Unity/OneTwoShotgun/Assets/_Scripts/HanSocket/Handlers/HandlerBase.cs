using System.Collections;
using UnityEngine;

namespace HanSocket.Handlers
{   
   abstract public class HandlerBase : MonoBehaviour
   {
      protected Flag _flag = new Flag();
      protected WaitUntil wait;

      protected void Init(string type)
      {
         wait = new WaitUntil(_flag.Get);

         BufferHandler.Instance.AddHandler(type, payload => {
            OnArrived(payload);
            _flag.Set();
         });

         StartCoroutine(FlagSetted());
      }

      protected virtual IEnumerator FlagSetted()
      {
         while (true)
         {
            yield return wait;
         }
      }


      abstract protected void OnArrived(string payload);
      abstract protected void OnFlag();
   }
}