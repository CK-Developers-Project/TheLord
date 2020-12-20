using UnityEngine;
namespace Developers.Net.Protocol
{
    using Util;
    using Structure;

    public enum ClickAction : int
    {
        MainBuildingTakeGold,   // 골드를 받다.
        BuildingBuild,          // 건물을 짓다.
        BuildingLevelUp,        // 건물 레벨업
        CharacterHire,          // 캐릭터를 고용
    }

    public enum ConfirmAction : int
    {
        Build,                  // 건물 지음 확인
        LevelUp,                // 건물 레벨업
    }

    public class BuildingClickRequest : BaseProtocol
    {
        public int index;
        public ClickAction clickAction;
        public int value;

        public override void SendPacket ( bool reliable,  bool isWait = false )
        {
            base.SendPacket ( isWait );
            var buildingClickData = new ProtoData.BuildingClickData ( );
            buildingClickData.index = index;
            buildingClickData.clickAction = (int)clickAction;
            buildingClickData.value = value;

            Send ( OperationCode.BuildingClick, BinSerializer.ConvertPacket( buildingClickData ) );
            Debug.Log ( "[BuildingClickRequest] Send" );
        }
    }

    public class BuildingConfirmRequest : BaseProtocol
    {
        public int index;
        public ConfirmAction confirmAction;

        public override void SendPacket ( bool reliable, bool isWait = false )
        {
            base.SendPacket ( isWait );
            var buildingConfirmData = new ProtoData.BuildingConfirmData ( );
            buildingConfirmData.index = index;
            buildingConfirmData.confirmAction = (int)confirmAction;

            Send ( OperationCode.BuildingConfirm, BinSerializer.ConvertPacket ( buildingConfirmData ) );
            Debug.Log ( "[BuildingConfirmRequest] Send" );
        }
    }
}
