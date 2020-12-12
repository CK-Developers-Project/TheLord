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

        public override void SendPacket ( bool isWait = false )
        {
            base.SendPacket ( isWait );
            ProtoData.UserData userData = new ProtoData.UserData ( );
            userData.id = id;
            userData.password = password;
            PhotonEngine.Peer.OpCustom ( (byte)OperationCode.Login, BinSerializer.ConvertPacket ( userData ), true );
            Debug.Log ( "[LoginRequest]" );
        }
    }

    public class UserResistrationRequest : BaseProtocol
    {
        string nickname;
        int race;

        public UserResistrationRequest ( string nickname, int race ) => (this.nickname, this.race) = (nickname, race);

        public override void SendPacket ( bool isWait = false )
        {
            base.SendPacket ( isWait );
            ProtoData.UserData userData = new ProtoData.UserData ( );
            userData.nickname = nickname;
            userData.race = race;
            PhotonEngine.Peer.OpCustom ( (byte)OperationCode.UserResistration, BinSerializer.ConvertPacket ( userData ), true );
            Debug.Log ( "[UserResistrationRequest]" );
        }
    }

    public class LobbyEnterRequest : BaseProtocol
    {
        public override void SendPacket ( bool isWait = true )
        {
            base.SendPacket ( isWait );
            PhotonEngine.Peer.OpCustom ( (byte)OperationCode.LobbyEnter, null, true );
            Debug.Log ( "[LobbyEnterRequest]" );
        }
    }
}
