using UnityEngine;
using TMPro;
using HanSocket.Data;

namespace UI.Queue
{   
   public class JoinedCountUI : MonoBehaviour
   {
      private TMP_Text _joinedCountText;

      private void Awake()
      {
         _joinedCountText = GetComponentInChildren<TMP_Text>();
      }

      public void UpdateText()
      {
         _joinedCountText.text = $"{GameData.Instance.GetUserCount()} Connected";
      }
   }
}