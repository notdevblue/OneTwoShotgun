using HanSocket;
using HanSocket.Data;
using HanSocket.VO.Player;
using UnityEngine;

namespace Characters.Player
{
   public class Shell : MonoBehaviour
   {
      private int _id = -1;

      private uint _frame = 0;

      private Vector2 _targetPos;
      private const float _t = (1.0f / (15.0f / 6.0f)) ; // 서버 프레임와 맞춤

      public void Process(uint frame, int id, Vector2 pos)
      {
         if (_id == -1)
         {
            this.transform.position = pos;
            this._id = id;
         }

         _frame = frame;
         _targetPos = pos;
      }

      public bool CheckFrame(uint frame)
      {
         if (_frame != frame)
         {
            gameObject.SetActive(false);
            
            return false;
         }

         return true;
      }


      private void Update()
      {
         transform.position = Vector2.Lerp(transform.position, _targetPos, _t);
      }

      private void OnDisable()
      {
         this._id = -1;
      }
   }
}