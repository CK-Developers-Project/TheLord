using System.Collections.Generic;
using UnityEngine;

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
            ProtoData.UserData userData = new ProtoData.UserData ( );
            userData.id = id;
            userData.password = password;
            PhotonEngine.Peer.OpCustom ( (byte)OperationCode.Login, BinSerializer.ConvertPacket ( userData ), true );
            Debug.Log ( "[LoginRequest] Send" );
        }
    }

    public class UserResistration : BaseProtocol
    {
        string nickname;
        int race;

        public UserResistration ( string nickname, int race ) => (this.nickname, this.race) = (nickname, race);

        public override void SendPacket ( )
        {
            ProtoData.UserData userData = new ProtoData.UserData ( );
            userData.nickname = nickname;
            userData.race = race;
            PhotonEngine.Peer.OpCustom ( (byte)OperationCode.UserResistration, BinSerializer.ConvertPacket ( userData ), true );
            Debug.Log ( "[UserResistration] Send" );
        }
    }
}
