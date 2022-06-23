using UnityEngine;

namespace Characters.Player
{   
   public interface IDamageable
   {
      public void Damage();
      public void SetHP(int hp);
   }
}