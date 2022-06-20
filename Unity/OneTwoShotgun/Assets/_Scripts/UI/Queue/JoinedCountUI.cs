using UnityEngine;
using TMPro;
using HanSocket.Data;

namespace UI.Queue
{   
   public class JoinedCountUI : MonoSingleton<JoinedCountUI>
   {
      private TMP_Text _joinedCountText;

      private void Start()
      {
         _joinedCountText = GetComponentInChildren<TMP_Text>();
      }

      public void UpdateText()
      {
         _joinedCountText.text = $"{GameData.Instance.GetUserCount()} Connected";
      }
   }
}