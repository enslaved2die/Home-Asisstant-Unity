using System;
using NativeWebSocket;
using UnityEngine;

namespace Whitepoint.HomeAssistant
{
  public class Connection : MonoBehaviour
  {
        //public Entity.LightEntity _lightEntity = new Entity.LightEntity();
        static WebSocket websocket;
        public string host = "ws://homeassistant:8123/api/websocket";
        public string token;

        Auth authentication = new Auth();
        public Subscribe allEvents = new Subscribe();
        public bool subscribeToAll = false;
        
        public static int currentID;

        async void Start()
        {
          websocket = new WebSocket(host);
          websocket.OnOpen += () =>
          {
            Debug.Log("Connection open!");
            
            //Authentication Phase
            authentication.access_token = token;
            websocket.SendText(JsonUtility.ToJson(authentication));
            if(subscribeToAll) websocket.SendText(JsonUtility.ToJson(allEvents));

          };
      
          websocket.OnError += (e) =>
          {
            Debug.Log("Error! " + e);
          };
      
          websocket.OnClose += (e) =>
          {
            Debug.Log("Connection closed!");
          };
      
          websocket.OnMessage += (bytes) =>
          {
            // Reading a plain text message
            var message = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log("Received OnMessage! (" + bytes.Length + " bytes) " + message);
            
            if (message.Contains("auth_ok"))
            {
              Debug.Log("Authentication OK!"); 
            }
          };
      
          // Keep sending messages at every 0.3s
          //InvokeRepeating("SendWebSocketMessage", 0.0f, 1f);
      
          await websocket.Connect();
        }
      
        void Update()
        {
          #if !UNITY_WEBGL || UNITY_EDITOR
            websocket.DispatchMessageQueue();
          #endif
        }
      
        async void SendWebSocketMessage()
        {
          if (websocket.State == WebSocketState.Open)
          {
            // Sending bytes
            await websocket.Send(new byte[] { 10, 20, 30 });
      
            // Sending plain text
            await websocket.SendText("plain text message");
          }
        }
        
        public static void CommandPhase(string msg)
        {
          websocket.SendText(msg);
        }

        async public void Ping()
        {
          await websocket.SendText(JsonUtility.ToJson(new PingService()));
        }
        
        private async void OnApplicationQuit()
        {
          await websocket.Close();
        }
        
        public class Auth
        {
          public string type = "auth";
          public string access_token;
        }
        
        public class Subscribe
        {
          public int id = currentID++;
          public string type = "subscribe_events";
          public string event_type = "state_changed";
        }

        public class PingService
        {
          public int id = currentID++;
          public string type = "ping";
        }
  }
}
