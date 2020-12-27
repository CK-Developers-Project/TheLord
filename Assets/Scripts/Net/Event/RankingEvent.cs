using ExitGames.Client.Photon;
using UnityEngine;
using System;

namespace Developers.Net.Event
{
    using Structure;
    using Util;

    public class RankingEvent : BaseEvent
    {
        public override void AddListener ( )
        {
            EventMedia.AddListener ( EventCode.UpdateRaidRanking, OnUpdateRaidRanking );
        }

        public override void RemoveListener ( )
        {
            EventMedia.RemoveAllListener ( EventCode.UpdateRaidRanking );
        }

        void OnUpdateRaidRanking ( EventData eventData )
        {
            var data = BinSerializer.ConvertData<ProtoData.RaidRankingData> ( eventData.Parameters );
            TransitionManager.Instance.OnWaitSigh ( false );

            MainLobbyGameMode gameMode = GameManager.Instance.GameMode as MainLobbyGameMode;
            if ( gameMode == null )
            {
                return;
            }

            MainLobbyPage page = gameMode.CurrentPage as MainLobbyPage;
            if ( page == null )
            {
                return;
            }

            RankingPopup popup = page.rankingPopup;

            popup.SetRemainTime ( data.tick );
            popup.SetInfoForMyRanking ( data.myRankingData );
            popup.SetInfoForLastHitRnaking ( data.lastHitRankingData );
            popup.SetInfo ( data.rankingDataList );
            page.OnUpdate ( );
        }
    }
}
