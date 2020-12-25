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
            EventMedia.AddListener(EventCode.UpdateChat, OnUpdateChat);
        }

        public override void RemoveListener ( )
        {
            EventMedia.RemoveAllListener ( EventCode.UpdateResource );
            EventMedia.RemoveAllListener(EventCode.UpdateChat);
        }

        void OnUpdateResource(EventData eventData)
        {
            Debug.Log ( "[OnUpdateResource]" );
            var data = BinSerializer.ConvertData<ProtoData.ResourceData> ( eventData.Parameters );
            GamePlayer localPlayer = GameManager.Instance.LocalPlayer;
            localPlayer.SetGold ( ResourceType.Gold, data.gold );
            localPlayer.SetGold ( ResourceType.Cash, data.cash );
            GameManager.Instance.GameMode.CurrentPage.OnUpdate ( );
        }

        void OnUpdateChat(EventData eventData)
        {
            Debug.Log("[OnUpdateChat]");
            var data = BinSerializer.ConvertData<ProtoData.ChatData>(eventData.Parameters);

            MainLobbyPage page = GameManager.Instance.GameMode.CurrentPage as MainLobbyPage;
            if (page == null)
            {
                return;
            }
            page.chatPopup.AddChat(data.index, data.nickname, data.msg);
            GameManager.Instance.GameMode.CurrentPage.OnUpdate();
        }
    }
}
