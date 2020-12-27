using ExitGames.Client.Photon;
using UnityEngine;
using System;

namespace Developers.Net.Event
{
    using Structure;
    using Util;

    public class ChatEvent : BaseEvent
    {
        public override void AddListener ( )
        {
            EventMedia.AddListener ( EventCode.UpdateChat, OnUpdateChat );
        }

        public override void RemoveListener ( )
        {
            EventMedia.RemoveAllListener ( EventCode.UpdateChat );
        }

        void OnUpdateChat ( EventData eventData )
        {
            var data = BinSerializer.ConvertData<ProtoData.ChatData> ( eventData.Parameters );

            MainLobbyPage page = GameManager.Instance.GameMode.CurrentPage as MainLobbyPage;
            if ( page == null )
            {
                return;
            }
            page.chatPopup.AddChat ( data.index, data.nickname, data.msg );
            GameManager.Instance.GameMode.CurrentPage.OnUpdate ( );
        }
    }
}
