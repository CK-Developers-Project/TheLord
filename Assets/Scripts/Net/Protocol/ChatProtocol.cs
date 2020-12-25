using UnityEngine;

namespace Developers.Net.Protocol
{
    using Util;
    using Structure;

    public class SendChat : BaseProtocol
    {
        public int index;
        public string nick;
        public string msg;

        public override void SendPacket ( bool reliable,  bool isWait = false )
        {
            base.SendPacket ( isWait );
            var sendChatData = new ProtoData.ChatData ( );
            sendChatData.index = index;
            sendChatData.nickname = nick;
            sendChatData.msg = msg;

            Send ( OperationCode.Chat, BinSerializer.ConvertPacket(sendChatData) );
            Debug.Log ("[SendChat] Send");
        }
    }
}
