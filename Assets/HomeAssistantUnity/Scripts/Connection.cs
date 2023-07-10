using System;
using NativeWebSocket;
using UnityEngine;

namespace Whitepoint.HomeAssistant
{
  public class Connection : MonoBehaviour
  {
        //public Entity.LightEntity _lightEntity = new Entity.LightEntity();
        static WebSocket _websocket;
        public string host = "ws://homeassistant:8123/api/websocket";
        public string token;

        Auth authentication = new Auth();
        public Subscribe allEvents = new Subscribe();
        public bool subscribeToAll = false;
        
        public static int CurrentID;
        public static string messageQueue = null;
        [TextArea]
        public string lastMsg;
        public float slowSendCycle;

        async void Start()
        {
          _websocket = new WebSocket(host);
          _websocket.OnOpen += () =>
          {
            Debug.Log("Connection open!");
            
            //Authentication Phase
            authentication.access_token = token;
            _websocket.SendText(JsonUtility.ToJson(authentication));
            if(subscribeToAll) _websocket.SendText(JsonUtility.ToJson(allEvents));

          };
      
          _websocket.OnError += (e) =>
          {
            Debug.Log("Error! " + e);
          };
      
          _websocket.OnClose += (e) =>
          {
            Debug.Log("Connection closed!");
          };
      
          _websocket.OnMessage += (bytes) =>
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
          // InvokeRepeating("SendWebSocketMessage", 0.0f, 1f);
          InvokeRepeating("SlowCommandPhase", 0.0f, slowSendCycle);

      
          await _websocket.Connect();
        }
      
        void Update()
        {
          #if !UNITY_WEBGL || UNITY_EDITOR
            _websocket.DispatchMessageQueue();
          #endif
          
        }
      
        async void SendWebSocketMessage()
        {
          if (_websocket.State == WebSocketState.Open)
          {
            // Sending bytes
            await _websocket.Send(new byte[] { 10, 20, 30 });
      
            // Sending plain text
            await _websocket.SendText("plain text message");
          }
        }
        
        public static void CommandPhase(string msg)
        {
          _websocket.SendText(msg);
        }
        
        async void SlowCommandPhase()
        {
          if (messageQueue != null)
          {
            await _websocket.SendText(messageQueue);
            lastMsg = messageQueue;
            messageQueue = null;
          }
        }

        async public void Ping()
        {
          await _websocket.SendText(JsonUtility.ToJson(new PingService()));
        }
        
        private async void OnApplicationQuit()
        {
          await _websocket.Close();
        }
        
        public class Auth
        {
          public string type = "auth";
          public string access_token;
        }
        
        public class Subscribe
        {
          public int id = CurrentID++;
          public string type = "subscribe_events";
          public string event_type = "state_changed";
        }

        public class PingService
        {
          public int id = CurrentID++;
          public string type = "ping";
        }
  }
}
