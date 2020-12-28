using UnityEngine;
using ExitGames.Client.Photon;
using System;

namespace Developers.Net.Handler
{
    using Structure;
    using Util;
    using Protocol;

    public class LoginHandler : BaseHandler
    {
        enum NextAction : short
        {
            LoginSuccess = 0,   // 로그인 성공
            LoginFailed,        // 로그인 실패
            UserCreateFail,     // 유저생성 실패
            UserInfoCreate,     // 유저정보 생성
        }

        public override void AddListener ( )
        {
            HandlerMedia.AddListener ( OperationCode.Login, OnLoginReceived );
            HandlerMedia.AddListener ( OperationCode.UserResistration, OnUserResistrationReceived );
            HandlerMedia.AddListener ( OperationCode.LobbyEnter, OnLobbyEnterRecevied );
        }

        public override void RemoveListener ( )
        {
            HandlerMedia.RemoveAllListener ( OperationCode.Login );
            HandlerMedia.RemoveAllListener ( OperationCode.UserResistration );
            HandlerMedia.RemoveAllListener ( OperationCode.LobbyEnter );
        }

        void OnLoginReceived (OperationResponse response)
        {
            Debug.Log ( "[OnLoginReceived]" );
            switch ( (NextAction)response.ReturnCode)
            {
                case NextAction.LoginSuccess:
                    byte[] bytes = (byte[])DictionaryTool.GetValue<byte, object> ( response.Parameters, 1 );
                    ProtoData.UserData data = BinSerializer.Deserialize<ProtoData.UserData> ( bytes );
                    GameManager.Instance.Join ( data.nickname, (Race)data.race );
                    TransitionManager.Instance.OnSceneTransition ( SceneName.GetMainLobby ( (Race)data.race ), TransitionType.Loading01_Slide, null );
                    break;
                case NextAction.LoginFailed:
                    BasePage.OnMessageBox("로그인에 실패하셨습니다.", true, null, "확인");
                    break;
                case NextAction.UserCreateFail:
                    BasePage.OnMessageBox("ID길이가 잘못 되었습니다.\n(2글자 ~ 10글자)", true, null, "확인");
                    break;
                case NextAction.UserInfoCreate:
                    Action action = ( ) =>
                    {
                        LoginGameMode gameMode = GameManager.Instance.GameMode as LoginGameMode;
                        GameManager.Instance.GameMode.CurrentPage = gameMode.cutScenePage;
                    };
                    TransitionManager.Instance.OnTransition ( TransitionType.Blank, TransitionType.Slide, action, null );
                    break;
            }
        }

        void OnUserResistrationReceived( OperationResponse response )
        {
            Debug.Log ( "[OnUserResistrationReceived]" );
            ReturnCode rc = (ReturnCode)response.ReturnCode;
            if(rc == ReturnCode.Success)
            {
                byte[] bytes = (byte[])DictionaryTool.GetValue<byte, object> ( response.Parameters, 1 );
                ProtoData.UserData data = BinSerializer.Deserialize<ProtoData.UserData> ( bytes );
                GameManager.Instance.Join ( data.nickname, (Race)data.race );
                TransitionManager.Instance.OnSceneTransition ( SceneName.GetMainLobby ( GameManager.Instance.LocalPlayer.playerInfo.Race ), TransitionType.Loading01_Slide, null );
            }
            else
            {
                BasePage.OnMessageBox ( "닉네임의 길이가 잘못 되었습니다.\n(3글자 ~ 6글자)", true, null, "확인" );
            }
        }

        void OnLobbyEnterRecevied ( OperationResponse response )
        {
            Debug.Log ( "[OnLobbyEnterRecevied]" );
            ReturnCode rc = (ReturnCode)response.ReturnCode;
            if ( rc == ReturnCode.Failed )
            {
                new LobbyEnterRequest ( ).SendPacket();
                Debug.LogError ( "로비 데이터를 받는데 실패했습니다." );
                return;
            }

            var data = BinSerializer.ConvertData<ProtoData.DBLoadData> ( response.Parameters );

            if ( data.buildingDataList == null || data.buildingDataList.Count == 0 )
            {
                new LobbyEnterRequest ( ).SendPacket ( );
                Debug.LogError ( "건물 데이터를 받는데 실패했습니다." );
            }
            else if ( data.resourceData == null )
            {
                new LobbyEnterRequest ( ).SendPacket ( );
                Debug.LogError ( "유저 자원 데이터를 받는데 실패했습니다." );
            }

            GameManager.Instance.GameMode.OnSynchronize ( data );
        }
    }
}
