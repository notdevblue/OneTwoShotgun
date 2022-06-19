using UnityEngine;
using TMPro;
using HanSocket;
using HanSocket.VO.Auth;
using HanSocket.Data;

namespace UI.Auth
{
   public class NicknameInputUI : MonoBehaviour
   {
      private TMP_InputField _nicknameInputField;

      private void Awake()
      {
         _nicknameInputField = GetComponentInChildren<TMP_InputField>();
      }

      private void Update()
      {
         if (Input.GetKeyDown(KeyCode.Return))
         {
            string nickname = _nicknameInputField.text.Trim();
            string payload = new CheckNicknameVO(nickname).ToJson();
            
            WebSocketClient.Instance.Send("checknickname", payload);
            UserData.Instance.nickname = nickname;
            _nicknameInputField.text = "";
         }
      }
   }
}