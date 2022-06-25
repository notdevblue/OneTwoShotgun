using UnityEngine;
using TMPro;
using HanSocket;

namespace UI.Auth
{
   public class ConnectToServer : MonoBehaviour
   {
      public TMP_InputField ipInputField;
      public TMP_InputField portInputField;

      private void Update()
      {
         if (Input.GetKeyDown(KeyCode.Return))
            Connect();
      }

      private void Connect()
      {
         WebSocketClient
            .Instance
            .Connect(ipInputField.text.Trim(),
                     portInputField.text.Trim());

         UIManager.Instance.ActivePanel("nickname");
      }
   }
}