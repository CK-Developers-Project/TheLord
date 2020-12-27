using ExitGames.Client.Photon;
using UnityEngine;
using System;

namespace Developers.Net.Event
{
    using Structure;
    using Util;

    public class BossEvent : BaseEvent
    {
        public override void AddListener ( )
        {
            EventMedia.AddListener ( EventCode.UpdateRaidBoss, OnUpdateRaidBoss );
        }

        public override void RemoveListener ( )
        {
            EventMedia.RemoveAllListener ( EventCode.UpdateRaidBoss );
        }

        void OnUpdateRaidBoss ( EventData eventData )
        {
            var data = BinSerializer.ConvertData<ProtoData.RaidBossData> ( eventData.Parameters );
            switch ( GameManager.Instance.GameMode )
            {
                case MainLobbyGameMode mainLobby:
                    {
                        MainLobbyPage page = mainLobby.CurrentPage as MainLobbyPage;
                        if(page== null)
                        {
                            return;
                        }
                        page.rankingPopup.SetHP ( data.hp, data.index );
                    }
                    break;
                case RaidGameMode raid:
                    {
                        RaidBattlePage page = raid.CurrentPage as RaidBattlePage;
                        if ( page == null )
                        {
                            return;
                        }

                    }
                    break;
            }
        }
    }
}
