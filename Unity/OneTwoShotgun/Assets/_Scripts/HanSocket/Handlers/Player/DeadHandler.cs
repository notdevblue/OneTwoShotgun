using UnityEngine;
using HanSocket.VO.Player;
using HanSocket.Data;
using UI.Queue;

namespace HanSocket.Handlers.Player   
{
   public class DeadHandler : HandlerBase
   {
      protected override string Type => "dead";
      private DeadVO vo = null;
      private LeaderBoardUI _leaderBoard;

      private void Start()
      {
         _leaderBoard = FindObjectOfType<LeaderBoardUI>();
      }

      protected override void OnArrived(string payload)
      {
         vo = JsonUtility.FromJson<DeadVO>(payload);
      }

      protected override void OnFlag()
      {
         ++GameData.Instance.GetUser(vo.killedby).kills;

         GameData.Instance.DeleteUser(vo.id);
         _leaderBoard.UpdateLeaderboard();

         if (vo.id == UserData.Instance.id)
            Application.Quit(); // 작년 동아리 산출물 리스팩트
      }
   }
}