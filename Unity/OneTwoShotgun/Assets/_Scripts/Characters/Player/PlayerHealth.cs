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

      public UnityEvent<int> OnHpChange;

      public void SetHP(int hp)
      {
         _hp = hp;
         OnHpChange?.Invoke(_hp);
      }
   }
}