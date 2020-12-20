using UnityEngine;
using ExitGames.Client.Photon;

namespace Developers.Net.Handler
{
    using Structure;
    using Util;
    using Protocol;

    public class BuildingHandler : BaseHandler
    {
        public override void AddListener ( )
        {
            HandlerMedia.AddListener ( OperationCode.BuildingClick, OnBuildingClickReceived );
            HandlerMedia.AddListener ( OperationCode.BuildingConfirm, OnBuildingConfirmReceived );
        }

        public override void RemoveListener ( )
        {
            HandlerMedia.RemoveAllListener ( OperationCode.BuildingClick );
            HandlerMedia.RemoveAllListener ( OperationCode.BuildingConfirm );
        }

        void OnBuildingClickReceived ( OperationResponse response )
        {
            var buildingClickData = BinSerializer.ConvertData<ProtoData.BuildingClickData> ( response.Parameters );
            
            switch ( (ClickAction)buildingClickData.clickAction )
            {
                case ClickAction.MainBuildingTakeGold:
                    break;
                case ClickAction.BuildingBuild:
                    ClickAction_BuildingBuild ( );
                    break;
                case ClickAction.BuildingLevelUp:
                    ClickAction_BuildingLevelUp ( );
                    break;
                case ClickAction.CharacterHire:
                    ClickAction_CharacterHire ( );
                    break;
                default:
                    return;
            }

        }

        void ClickAction_BuildingBuild()
        {

        }
        
        void ClickAction_BuildingLevelUp()
        {

        }
       
        void ClickAction_CharacterHire()
        {

        }

        void OnBuildingConfirmReceived( OperationResponse response )
        {
            var buildingConfirmData = BinSerializer.ConvertData<ProtoData.BuildingConfirmData> ( response.Parameters );
            switch ( (ConfirmAction)buildingConfirmData.confirmAction )
            {
                case ConfirmAction.Build:
                    ConfirmAction_Build ( (ReturnCode)response.ReturnCode, buildingConfirmData );
                    break;
                case ConfirmAction.LevelUp:
                    ConfirmAction_LevelUp ( );
                    break;
                default:
                    return;
            }
        }

        void ConfirmAction_Build ( ReturnCode returnCode, ProtoData.BuildingConfirmData buildingConfirmData )
        {
            if ( ReturnCode.Success == returnCode )
            {
                MainLobbyGameMode gameMode = GameManager.Instance.GameMode as MainLobbyGameMode;
                if(gameMode== null)
                {
                    // 다른 씬임
                    return;
                }
                var building = gameMode.Buildings.Find ( x => (int)x.info.index == buildingConfirmData.index );
                if( building == null)
                {
                    // 건물이 없음
                    return;
                }
                building.OnBuild ( );
            }
            else
            {
                // 실패
            }
        }

        void ConfirmAction_LevelUp ( )
        {

        }
    }
}
