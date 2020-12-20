using ExitGames.Client.Photon;
using UnityEngine;
using System;

namespace Developers.Net.Event
{
    using Structure;
    using Util;
    using Net.Protocol;

    public class BuildingEvent : BaseEvent
    {
        public override void AddListener ( )
        {
            EventMedia.AddListener ( EventCode.UpdateBuilding, OnUpdateBuilding );
        }

        public override void RemoveListener ( )
        {
            EventMedia.RemoveAllListener ( EventCode.UpdateBuilding );
        }

        void OnUpdateBuilding ( EventData eventData )
        {
            Debug.Log ( "[OnUpdateBuilding]" );
            var data = BinSerializer.ConvertData<ProtoData.BuildingData> ( eventData.Parameters );

            MainLobbyGameMode gameMode = GameManager.Instance.GameMode as MainLobbyGameMode;
            if ( gameMode == null )
            {
                // 다른 씬임
                return;
            }

            int index = data.index;

            var building = gameMode.Buildings.Find ( x => (int)x.info.index == index );
            if ( building == null )
            {
                // 건물이 없음
                return;
            }

            building.info.LV = data.LV;
            building.info.workTime = GameUtility.String2DateTime ( data.tick );

            if ( !Mathf.Approximately ( building.info.workTime.Ticks, 0 ) )
            {
                Action @event;
                if (data.LV == 0)
                {
                    @event = ( ) =>
                    {
                        var packet = new BuildingConfirmRequest ( );
                        packet.index = (int)building.info.index;
                        packet.confirmAction = ConfirmAction.Build;
                        packet.SendPacket ( true, true );
                    };
                }
                else
                {
                    @event = ( ) =>
                    {
                        var packet = new BuildingConfirmRequest ( );
                        packet.index = (int)building.info.index;
                        packet.confirmAction = ConfirmAction.LevelUp;
                        packet.SendPacket ( true, true );
                    };
                }

                building.BuildUp ( building.info.workTime, @event );
            }
        }
    }
}
