using UnityEngine;

namespace Developers.Net.Protocol
{
    using Util;
    using Structure;

    public class EnterContentRequest : BaseProtocol
    {
        public override void SendPacket ( bool isWait = false, bool reliable = true )
        {
            base.SendPacket ( isWait, reliable );
            Send ( OperationCode.EnterContent, null );
            Debug.Log ( "[EnterContentRequest] Send" );
        }
    }

    public class EnterRaidRequest : BaseProtocol
    {
        public override void SendPacket ( bool isWait = false, bool reliable = true )
        {
            base.SendPacket ( isWait, reliable );
            Send ( OperationCode.EnterRaid, null );
            Debug.Log ( "[EnterRaidRequest] Send" );
        }
    }
}
