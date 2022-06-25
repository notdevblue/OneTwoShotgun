using System.Text;
using UnityEngine;
using TMPro;
using HanSocket.Data;

namespace UI.Queue
{   
   public class LeaderBoardUI : MonoBehaviour
   {
      private TMP_Text _leaderboardText;

      private void Awake()
      {
         _leaderboardText = GetComponentInChildren<TMP_Text>();
      }


      public void UpdateLeaderboard()
      {
         StringBuilder sb = new StringBuilder();
         var users = GameData.Instance.GetAllUser();
         var player = UserData.Instance.Player;

         users.Sort((x, y) => y.kills.CompareTo(x.kills));
         int count = users.Count > 5 ? 5 : users.Count;

         for (int i = 0; i < count; ++i)
         {
            sb.Append($"{users[i].nickname}: {users[i].kills} kills\r\n");
         }

         sb.Append($"\r\n{player.nickname}: {player.kills} kills");


         _leaderboardText.text = sb.ToString();
      }
   }
}