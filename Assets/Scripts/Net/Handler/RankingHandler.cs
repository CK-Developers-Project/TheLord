using System;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;

namespace Developers.Net.Handler
{
    using Structure;

    public class RankingHandler : BaseHandler
    {
        public override void AddListener ( )
        {
            //HandlerMedia.AddListener ( OperationCode.RequestRaidRanking, OnRequestRaidRankingReceived );
        }

        public override void RemoveListener ( )
        {
            //HandlerMedia.RemoveAllListener ( OperationCode.RequestRaidRanking );
        }

        /*void OnRequestRaidRankingReceived( OperationResponse response )
        {
        }
        */
    }
}