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

      public void Process(uint frame, int id, Vector2 pos)
      {
         if (_id == -1)
            this._id = id;

         _frame = frame;

         transform.position = pos;
      }

      public void CheckFrame(uint frame)
      {
         if (_frame != frame)
         {
            gameObject.SetActive(false);
         }
      }
   }
}