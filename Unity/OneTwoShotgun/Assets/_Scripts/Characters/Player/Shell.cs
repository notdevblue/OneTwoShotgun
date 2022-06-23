using HanSocket;
using HanSocket.Data;
using HanSocket.VO.Player;
using UnityEngine;

namespace Characters.Player
{   
   [RequireComponent(typeof(Rigidbody2D))]
   public class Shell : MonoBehaviour
   {
      // private Rigidbody2D _rigid;
      // private Rigidbody2D Rigid {
      //    get {
      //       if (_rigid == null)
      //          _rigid = GetComponent<Rigidbody2D>();

      //       return _rigid;
      //    }
      // }

      // private const string PLYAER = "PLAYER";

      // private Vector2 _firedAt;
      // private int _firedby = -1;

      private int _id = -1;

      public void Process(int id, Vector2 pos)
      {
         if (_id == -1)
            this._id = id;

         transform.position = pos;
      }

      // public void Fire(Vector2 pos, Vector2 dir, float speed, int firedby, float alivefor = 10.0f)
      // {
      //    transform.position   = pos;
      //    _firedAt             = pos;
      //    _firedby             = firedby;

      //    Rigid.velocity = dir.normalized * speed;
      //    Invoke(nameof(Disable), alivefor);
      // }

      // private void Disable()
      // {
      //    gameObject.SetActive(false);
      // }

      // private void OnDisable()
      // {
      //    CancelInvoke();
      //    Rigid.velocity = Vector2.zero;
      //    transform.position = Vector3.zero;
      // }

      // private void OnTriggerEnter2D(Collider2D other)
      // {
      //    if (other.GetComponent<User>().id == _firedby) return;

      //    other.GetComponent<IDamageable>()?.Damage();
      //    gameObject.SetActive(false);
      // }

   }
}