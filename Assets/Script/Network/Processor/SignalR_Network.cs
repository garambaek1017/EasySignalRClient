using Assets.Script.Protocols;
using Microsoft.AspNet.SignalR.Client;
using System;
using UnityEngine;

namespace Assets.Script.Network.Processor
{
    public partial class SignalR_Network
    {
        public HubConnection hubConnection { get; set; }
        public IHubProxy proxy { get; set; }

        public SignalR_Network() { }


        public void Stop()
        {
            hubConnection.Stop();
        }
        /// <summary>
        ///  서버에 커넥트
        /// </summary>
        /// <param name="serverUrl"></param>
        /// <param name="gameUser"></param>
        public async void DoConnect(string serverUrl, GameUser gameUser)
        {
            hubConnection = new HubConnection(serverUrl);

            hubConnection.Closed += OnClose;
            hubConnection.Error += OnError;
            hubConnection.Received += OnReceive;
            hubConnection.Reconnected += OnReConnect;
            // 순서 중요 
            ProxySetting();

            await hubConnection.Start().ContinueWith((task) =>
            {
                if(task.IsCompleted == true)
                {
                    Debug.Log(string.Format("> Connection Result : {0}", task.Status));
                }
            });
        }

        /// <summary>
        /// 서버로 부터 응답받을 프록시 설정 
        /// </summary>
        public void ProxySetting()
        {
            // 접속할 서버 hub설정  
            proxy = hubConnection.CreateHubProxy("ChatHub");

            // receive 왔을때 받아서 쓰는 부분 
            proxy.On<SendChatResult>(ChatHubMethodNames.SendChatResult, ReceiveChat);

        }

        static void OnError(Exception e)
        {
            Debug.Log(string.Format("Error: {0}", e.Message));
        }

        static void OnReceive(string data)
        {
            Debug.Log("received : " + data);
        }

        static void OnReConnect()
        {
            Debug.Log("Reconnect");
        }

        static void OnClose()
        {
            Debug.Log("Connection Close");
        }

        public void DisconnectFromServer()
        {
            hubConnection.Stop();
            Debug.Log("Disconnect");
        }
    }
}
