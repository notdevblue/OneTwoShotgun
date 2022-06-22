using UnityEngine;

namespace Characters.Player
{   
   [RequireComponent(typeof(Rigidbody2D))]
   public class Shell : MonoBehaviour
   {
      private Rigidbody2D _rigid;
      private Rigidbody2D Rigid {
         get {
            if (_rigid == null)
               _rigid = GetComponent<Rigidbody2D>();

            return _rigid;
         }
      }

      private Vector2 _firedAt;

      public void Fire(Vector2 pos, Vector2 dir, float speed, float alivefor = 10.0f)
      {
         transform.position   = pos;
         _firedAt             = pos;

         Rigid.velocity = dir.normalized * speed;
         Invoke(nameof(Disable), alivefor);
      }

      private void Disable()
      {
         gameObject.SetActive(false);
      }

      private void OnDisable()
      {
         CancelInvoke();
         Rigid.velocity = Vector2.zero;
         transform.position = Vector3.zero;
      }


   }
}