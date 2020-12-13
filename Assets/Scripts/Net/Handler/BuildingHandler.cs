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
        }

        public override void RemoveListener ( )
        {
            HandlerMedia.RemoveAllListener ( OperationCode.BuildingClick );
        }

        void OnBuildingClickReceived ( OperationResponse response )
        {
            var buildingClickData = BinSerializer.ConvertData<ProtoData.BuildingClickData> ( response.Parameters );
            
            switch ( (ClickAction)buildingClickData.clickAction )
            {
                case ClickAction.MainBuildingTakeGold:
                    
                    break;
                default:
                    return;
            }

        }
    }
}
