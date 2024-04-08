using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Protocols
{
    // invoke할 함수 이름, (서버랑 같아야합니다잉)
    public class ChatHubMethodNames
    {
        public const string SendChat = "SendChat";
        public const string SendChatResult = "SendChatResult";

        public const string BroadcastMessage = "BroadcastMessage";
    }
}
