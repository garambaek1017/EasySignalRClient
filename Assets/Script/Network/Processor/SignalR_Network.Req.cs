using Assets.Script.Protocols;

namespace Assets.Script.Network.Processor
{

    public partial class SignalR_Network
    {
        public void RequestToServer(BasePacket req)
        {
            // 클라 -> 서버로 요청할때 Invoke로 호출 하면 됩니다. 
            Proxy.Invoke(req.MethodName, req);
        }

        public void DisConnect()
        {
            if (HubConnection != null)
                HubConnection.Stop();
        }
    }
}
