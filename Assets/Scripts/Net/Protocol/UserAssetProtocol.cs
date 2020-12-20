using UnityEngine;

namespace Developers.Net.Protocol
{
    using Util;
    using Structure;

    public class ResourceRequest : BaseProtocol
    {
        public override void SendPacket ( bool isWait = false, bool reliable = true )
        {
            base.SendPacket ( isWait );
            Send ( OperationCode.RequestResource, null );
            Debug.Log ( "[ResourceRequest] Send" );
        }
    }
}