using UnityEngine;
using TMPro;

namespace HanSocket.Data
{
   
   public class User : MonoBehaviour
   {
      public int id;
      public string nickname;

      [SerializeField]
      private TMP_Text _nicknameText;

      public void Init(int id, string nickname)
      {
         this.id = id;
         this.nickname = nickname;

         gameObject.name      = nickname;
         _nicknameText.text   = nickname;
      }

      public void Reset()
      {
         this.id = -1;
         this.nickname = "";
      }
   }
}