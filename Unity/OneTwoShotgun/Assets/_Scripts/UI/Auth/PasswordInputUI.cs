using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using HanSocket.VO.Auth;
using HanSocket.Data;

namespace HanSocket.Handlers.Auth
{
   public class PasswordInputUI : MonoBehaviour
   {
      private TMP_InputField _passwordInputField;


      private void Awake()
      {
         _passwordInputField = GetComponentInChildren<TMP_InputField>();
      }

      private void Update()
      {
         if (Input.GetKeyDown(KeyCode.Return))
         {
            string payload =
               new LoginVO(UserData.Instance.nickname,
                           _passwordInputField.text.Trim()).ToJson();
         
            WebSocketClient.Instance.Send("sign", payload);
            _passwordInputField.text = "";
         }
      }
   }
}