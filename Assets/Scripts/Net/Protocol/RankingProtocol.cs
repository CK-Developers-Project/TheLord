using UnityEngine;

namespace Developers.Net.Protocol
{
    using Util;
    using Structure;

    public class RequestRaidRankingRequest : BaseProtocol
    {
        public override void SendPacket ( bool isWait = true, bool reliable = true )
        {
            base.SendPacket ( isWait, reliable );
            Send ( OperationCode.RequestRaidRanking, null );
            Debug.Log ( "[RequestRaidRankingRequest] Send" );
        }
    }

    public class ResultRaidRankingRequest : BaseProtocol
    {
        public int score;

        public override void SendPacket ( bool isWait = false, bool reliable = true )
        {
            base.SendPacket ( isWait, reliable );
            var packet = new ProtoData.RaidRankingScoreData ( );
            packet.score = score;
            Send ( OperationCode.ResultRaidRanking, BinSerializer.ConvertPacket( packet ) );
            Debug.Log ( "[ResultRaidRankingRequest] Send" );
        }
    }
}
