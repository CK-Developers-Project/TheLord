using UnityEngine;
using System.Collections.Generic;
using Developers.Net.Protocol;
using System.Collections;
using Cinemachine;
using Developers.Structure;

public class MainLobbyGameMode : BaseGameMode
{
    [SerializeField] MainLobbyPage mainLobbyPage = null;
    [SerializeField] CinemachineVirtualCamera mainLobbyCV = null;
    [SerializeField] Transform cameraTarget = null;

    List<Building> buildings = new List<Building> ( );
    public List<Building> Buildings { get => buildings; }

    bool isDrag = false;

    public override IEnumerator OnStart ( )
    {
        new LobbyEnterRequest ( ).SendPacket ( false );
        yield return new WaitUntil ( ( ) => GameManager.Instance.IsSynchronized );
        yield return StartCoroutine ( base.OnStart ( ) );
    }

    public override void OnSynchronize<T> ( T data )
    {
        ProtoData.DBLoadData DBLoadData = data as ProtoData.DBLoadData;
        if(DBLoadData == null)
        {
            Debug.LogError ( "[MainLobbyGameMode] 싱크로할 수 없습니다." );
            return;
        }
        GameManager.Instance.LocalPlayer.SetGold ( ResourceType.Gold, DBLoadData.resourceData.gold );
        GameManager.Instance.LocalPlayer.SetGold ( ResourceType.Cash, DBLoadData.resourceData.cash );

        List<BuildingInfo> buildingInfoList = new List<BuildingInfo> ( );
        foreach(var buildingData in DBLoadData.buildingDataList )
        {
            BuildingInfo info = new BuildingInfo ( );
            info.index = (BuildingType)buildingData.index;
            info.LV = buildingData.LV;
            info.workTime = new System.DateTime( buildingData.tick );
            buildingInfoList.Add ( info );
        }
        GameManager.Instance.synchronizeData.SetBuildingInfo ( buildingInfoList );

        base.OnSynchronize ( data );
    }

    public override void Load ( )
    {
        base.Load ( );
    }


    void TouchEvent ( IActor target )
    {
        target.OnSelect ( );
    }

    public override void RegisterInput ( )
    { 
        var manager = InputManager.Instance.MainInputActions;
        if(manager.Main.enabled)
        {
            return;
        }
        InputManager.Instance.layerMask = Physics2D.DefaultRaycastLayers;
        InputManager.Instance.TouchEvent += TouchEvent;
        manager.Main.Enable ( );
    }

    public override void ReleaseInput ( )
    {
        var manager = InputManager.Instance.MainInputActions;
        if ( !manager.Main.enabled )
        {
            return;
        }
        InputManager.Instance.layerMask = 0;
        InputManager.Instance.TouchEvent -= TouchEvent;
        manager.Main.Disable ( );
    }

    public override void OnEnter ( )
    {
        SetPage ( mainLobbyPage );
        RegisterInput ( );
    }

    public override void OnUpdate ( )
    {
        InputManager manager = InputManager.Instance;

        if ( manager.isPressed )
        {
            if( isDrag == false)
            {
                isDrag = true;
                cameraTarget.position = GameManager.Instance.MainCamera.transform.position;
            }    

            Vector3 start = GameManager.Instance.MainCamera.ScreenToWorldPoint ( manager.Position );
            Vector3 end = GameManager.Instance.MainCamera.ScreenToWorldPoint ( manager.StartPoint );
            Vector3 drag = start - end;
            cameraTarget.position = cameraTarget.position + new Vector3 ( drag.x, 0F, 0F );
            manager.StartPoint = manager.Position;
        }
        else
        {
            isDrag = false;
        }
    }

    public override void OnExit ( )
    {
        ReleaseInput ( );
    }
}

