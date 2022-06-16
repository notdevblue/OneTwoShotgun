using HanSocket;
using HanSocket.VO.Auth;
using UnityEngine;
using TMPro;

namespace UI.Auth
{
   public class LoginUI : MonoBehaviour
   {
      private TMP_InputField _passwordInputField;

      private void Awake()
      {
         _passwordInputField = GetComponentInChildren<TMP_InputField>();
      }

      private void FixedUpdate()
      {
         if (Input.GetKeyDown(KeyCode.Return))
         {
            
         }
      }
   }
}