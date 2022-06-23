using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace HanSocket.Data
{
   
   public class User : MonoBehaviour
   {
      public int id;
      public int hp;
      public string nickname;

      [SerializeField]
      private TMP_Text _nicknameText;

      [SerializeField]
      private Slider _hpBar;

      public void Init(int id, int hp, string nickname)
      {
         this.id = id;
         this.hp = hp;

         this._hpBar.maxValue = hp;
         this._hpBar.value    = hp;

         this.nickname        = nickname;
         gameObject.name      = nickname;
         _nicknameText.text   = nickname;
      }

      public void Reset()
      {
         this.id = -1;
         this.nickname = "";
      }

      public void SetHP(int hp)
      {
         _hpBar.value = hp;
      }
   }
}