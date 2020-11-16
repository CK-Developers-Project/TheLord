using System.Collections.Generic;

namespace Developers.Net.Protocol
{
    using Util;
    using Structure;

    public class LoginRequest : BaseProtocol
    {
        string id;
        string password;

        public LoginRequest ( string id, string password ) => (this.id, this.password) = (id, password);

        public override void SendPacket ( )
        {
            ProtoBuf2Data.UserID userId = new ProtoBuf2Data.UserID ( );
            userId.Id = id;
            userId.Password = password;
            Dictionary<byte, object> data = new Dictionary<byte, object>
            {
                { 1,  BinSerializer.Serialize(userId) }
            };
            PhotonEngine.Peer.OpCustom ( (byte)OperationCode.Login, data, true );
            UnityEngine.Debug.Log ( "send" );
        }
    }
}
