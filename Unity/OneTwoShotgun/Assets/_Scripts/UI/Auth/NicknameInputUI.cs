using UnityEngine;
using TMPro;
using HanSocket;
using HanSocket.VO.Auth;

namespace UI.Auth
{
   public class NicknameInputUI : MonoBehaviour
   {
      private TMP_InputField _nicknameInputField;

      private void Awake()
      {
         _nicknameInputField = GetComponentInChildren<TMP_InputField>();
      }

      private void FixedUpdate()
      {
         if (Input.GetKeyDown(KeyCode.Return))
         {
            string payload = new CheckNicknameVO(_nicknameInputField.text.Trim()).ToJson();
            WebSocketClient.Instance.Send("checknickname", payload);
            _nicknameInputField.text = "";
         }
      }
   }
}