using UnityEngine;

namespace Developers.Net.Protocol
{
    using Util;
    using Structure;

    public class ResourceRequest : BaseProtocol
    {
        public override void SendPacket ( bool isWait = false )
        {
            base.SendPacket ( isWait );
            //PhotonEngine.Peer.OpCustom ( (byte)OperationCode.Login, BinSerializer.ConvertPacket ( userData ), true );
            Debug.Log ( "[ResourceRequest] Send" );
        }
    }
}