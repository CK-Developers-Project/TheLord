using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;

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
            byte[] bytes = (byte[])DictionaryTool.GetValue<byte, object> ( eventData.Parameters, 1 );
            ProtoData.ResourceData data = BinSerializer.Deserialize<ProtoData.ResourceData> ( bytes );
            GamePlayer localPlayer = GameManager.Instance.LocalPlayer;
            localPlayer.SetGold ( ResourceType.Gold, data.gold );
            localPlayer.SetGold ( ResourceType.Cash, data.cash );
            GameManager.Instance.GameMode.CurrentPage.OnUpdate ( );
        }
    }
}
