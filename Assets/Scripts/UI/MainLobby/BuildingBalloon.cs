using UnityEngine;
using Developers.Structure;
using Developers.Table;
using Developers.Util;
using Developers.Net.Protocol;

public class BuildingBalloon : MonoBehaviour
{
    public Building Owner { get; set; }
    [SerializeField] GameObject balloon;
    public bool isActive { get => balloon.activeInHierarchy; set => balloon.SetActive ( value ); }

    public void OnPurchase()
    {
        SoundManager.Instance.play ( LoadManager.Instance.GetSFXData ( SFXType.Tabsound ).clip, AudioSettings.dspTime + Time.deltaTime, 0F, 1F );
        MainLobbyPage page = GameManager.Instance.GameMode.CurrentPage as MainLobbyPage;
        if ( page == null )
        {
            return;
        }
        var sheet = TableManager.Instance.BuildingTable.BuildingInfoSheet;
        var record = BaseTable.Get ( sheet, "index", (int)Owner.info.index );

        string noticeMsg = string.Format ( "{0}를 건설하시겠습니까?", Owner.info.name );
        BigInteger price = new BigInteger ( (int)record["cost"] );

        page.OnPurchaseInfo ( noticeMsg, GameUtility.Ordinal ( price ), BuildOK, null );
        page.OnUpdate ( );
        isActive = false;
    }

    void BuildOK ( )
    {
        BuildingClickRequest packet = new BuildingClickRequest ( );
        packet.index = (int)Owner.info.index;
        packet.clickAction = ClickAction.BuildingBuild;
        packet.SendPacket ( true, true );
    }

    private void Awake ( )
    {
        Owner = GetComponentInParent<Building> ( );
        isActive = false;
    }
}
