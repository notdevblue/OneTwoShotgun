using System.Collections;
using System.Collections.Generic;
using HanSocket;
using UnityEngine;
using UnityEngine.Events;

namespace Characters.Player
{
   public class PlayerHealth : MonoBehaviour, IDamageable
   {
      private int _hp;

      public UnityEvent<int> OnDamage;

      public void Damage()
      {
         WebSocketClient.Instance.Send("hit", "");
      }

      public void SetHP(int hp)
      {
         _hp = hp;

         if (_hp > hp)
            OnDamage?.Invoke(_hp);
      }
   }
}