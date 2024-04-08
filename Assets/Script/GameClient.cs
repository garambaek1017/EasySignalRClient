using Assets.Script.Network.Processor;
using Assets.Script.Protocols;
using System.Diagnostics;


namespace Assets.Script
{
    public class GameClient : Singleton<GameClient>
    {
        public GameUser GameUser { get; set; }
        public SignalR_Network Network { get; set; }

        public void SetNetwork()
        {
            this.Network = new SignalR_Network();
        }

        public void SetGameUser(long accountIdx, string nickname)
        {
            this.GameUser = new GameUser(accountIdx, nickname);
        }

        public void Stop()
        {
            Network.Stop();
        }

        public void Connect(string serverURl)
        {
            Network.DoConnect(serverURl, GameUser);
        }

        
        /// <summary>
        /// 채팅 메시지 보내는 함수
        /// </summary>
        /// <param name="msg"></param>
        public void SendChat(string msg)
        {

            var req = new SendChat
            {
                AccountIdx = GameUser.AccountIdx,
                Sender = GameUser.NickName,

                Version = "0.0.1",
                Message = msg,
            };

            Network.RequestToServer(req);

        }
    }
}
