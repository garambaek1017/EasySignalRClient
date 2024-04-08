using Assets.Script.Protocols;
using System;

namespace Assets.Script.Network.Processor
{

    public partial class SignalR_Network
    {
        public delegate void DDisplayResult(string function, string message);
        public event DDisplayResult DisplayResult;

        public void ReceiveChat(SendChatResult result)
        {
            DisplayResult(result.MethodName, string.Format("{0} :: {1}", result.Sender, result.Message));
        }
    }
}
