using UnityEngine;
namespace Developers.Net.Protocol
{
    using Util;
    using Structure;

    public enum ClickAction : int
    {
        MainBuildingTakeGold,   // 골드를 받다.
    }

    public class BuildingClickRequest : BaseProtocol
    {
        public int index;
        public ClickAction clickAction;
        public int value;

        public override void SendPacket ( bool isWait = false )
        {
            base.SendPacket ( isWait );
            var buildingClickData = new ProtoData.BuildingClickData ( );
            buildingClickData.index = index;
            buildingClickData.clickAction = (int)clickAction;
            buildingClickData.value = value;

            Send ( OperationCode.BuildingClick, BinSerializer.ConvertPacket( buildingClickData ), false );
            Debug.Log ( "[BuildingClickRequest] Send" );
        }
    }
}
