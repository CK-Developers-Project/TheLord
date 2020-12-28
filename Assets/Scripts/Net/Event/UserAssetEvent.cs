using ExitGames.Client.Photon;
using UnityEngine;

namespace Developers.Net.Event
{
    using Structure;
    using Util;

    public class UserAssetEvent : BaseEvent
    {
        public override void AddListener ( )
        {
            EventMedia.AddListener ( EventCode.UpdateResource, OnUpdateResource );
        }

        public override void RemoveListener ( )
        {
            EventMedia.RemoveAllListener ( EventCode.UpdateResource );
        }

        void OnUpdateResource(EventData eventData)
        {
            MainLobbyGameMode gameMode = GameManager.Instance.GameMode as MainLobbyGameMode;
            if( gameMode == null)
            {
                return;
            }

            var data = BinSerializer.ConvertData<ProtoData.ResourceData> ( eventData.Parameters );
            GamePlayer localPlayer = GameManager.Instance.LocalPlayer;
            localPlayer.SetGold ( ResourceType.Gold, data.gold );
            localPlayer.SetGold ( ResourceType.Cash, data.cash );
            localPlayer.playerInfo.Tier = (TierType)data.tier;

            MainLobbyPage page = gameMode.CurrentPage as MainLobbyPage;
            if(page == null)
            {
                return;
            }
            GameManager.Instance.GameMode.CurrentPage.OnUpdate ( );
        }
    }
}
