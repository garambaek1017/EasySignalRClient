using Assets.Script.Protocols;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.Network.Processor
{
    public partial class SignalR_Network
    {
        public HubConnection HubConnection { get; set; }
        public IHubProxy Proxy { get; set; }

        public SignalR_Network() { }


        public void Stop()
        {
            HubConnection.Stop();
        }
        /// <summary>
        ///  서버에 커넥트
        /// </summary>
        /// <param name="serverEndPoint"></param>
        /// <param name="gameUser"></param>
        public async void DoConnect(string serverEndPoint, GameUser gameUser)
        {
            HubConnection = new HubConnection(serverEndPoint);

            HubConnection.Closed += OnClose;
            HubConnection.Error += OnError;
            HubConnection.Received += OnReceive;
            HubConnection.Reconnected += OnReConnect;
            // 순서 중요 
            ProxySetting();

            await HubConnection.Start().ContinueWith((task) =>
            {
                if (task.IsCompleted == true)
                {
                    Debug.Log($"> ConnectResult : {task.Status} : {DateTime.Now}");

                    // 연결 성공 여부 판단 
                    if (task.Status == TaskStatus.RanToCompletion) {
                        Debug.Log($"> Connection is success");
                    }
                    // 실패
                    else {
                        Debug.LogError($"> Connect Error : {task.Exception.GetBaseException().ToString()} : {DateTime.Now}");
                    }
                        
                }
            });
        }

        /// <summary>
        /// 서버로 부터 응답받을 프록시 설정 
        /// </summary>
        public void ProxySetting()
        {
            // 접속할 서버 hub설정  
            Proxy = HubConnection.CreateHubProxy("ChatHub");

            // receive 왔을때 받아서 쓰는 부분 
            Proxy.On<SendChatResult>(ChatHubMethodNames.SendChatResult, ReceiveChat);

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
            HubConnection.Stop();
            Debug.Log("Disconnect");
        }
    }
}
