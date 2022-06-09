using System.Collections;
using System.Collections.Generic;
using HanSocket.VO;
using UnityEngine;
using WebSocketSharp;

namespace HanSocket
{
   public class WebSocketClient : MonoBehaviour
   {
      [Header("Server Address")]
      public string ipAddr;

      [Header("Server Port")]
      public string port;

      private WebSocket ws;

      private void Awake()
      {
         ws = new WebSocket($"ws://{ipAddr}:{port}");

         ws.OnMessage += (sender, e) => {
            DataVO data = JsonUtility.FromJson<DataVO>(e.Data);
            BufferHandler.Instance.Handle(data.type, data.payload);
         };
      }

      public void Connect()
      {
         ws.Connect();
      }

      public void Disconnect()
      {
         ws.Close(CloseStatusCode.Normal, "Client disconnected");
      }


   }
}