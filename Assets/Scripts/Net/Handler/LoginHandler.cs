﻿using UnityEngine;
using ExitGames.Client.Photon;

namespace Developers.Net.Handler
{
    using Structure;
    using Util;

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
        }

        public override void RemoveListener ( )
        {
            HandlerMedia.RemoveAllListener ( OperationCode.Login );
            HandlerMedia.RemoveAllListener ( OperationCode.UserResistration );
        }

        void OnLoginReceived (OperationResponse response)
        {
            switch((NextAction)response.ReturnCode)
            {
                case NextAction.LoginSuccess:
                    byte[] bytes = (byte[])DictionaryTool.GetValue<byte, object> ( response.Parameters, 1 );
                    ProtoData.UserData data = BinSerializer.Deserialize<ProtoData.UserData> ( bytes );
                    GameManager.Instance.Join ( data.nickname, (Race)data.race );
                    TransitionManager.Instance.OnSceneTransition ( SceneName.GetMainLobby ( (Race)data.race ), TransitionType.Slide, null );
                    break;
                case NextAction.LoginFailed:
                    BasePage.OnMessageBox("로그인에 실패하셨습니다.", true, null, "확인");
                    break;
                case NextAction.UserCreateFail:
                    BasePage.OnMessageBox("ID길이가 잘못 되었습니다.\n(2글자 ~ 10글자)", true, null, "확인");
                    break;
                case NextAction.UserInfoCreate:
                    LoginGameMode gameMode = GameManager.Instance.GameMode as LoginGameMode;
                    GameManager.Instance.GameMode.CurrentPage = gameMode.raceSelectPage;
                    break;
            }
        }

        void OnUserResistrationReceived( OperationResponse response )
        {
            ReturnCode rc = (ReturnCode)response.ReturnCode;
            if(rc == ReturnCode.Success)
            {
                byte[] bytes = (byte[])DictionaryTool.GetValue<byte, object> ( response.Parameters, 1 );
                ProtoData.UserData data = BinSerializer.Deserialize<ProtoData.UserData> ( bytes );
                GameManager.Instance.Join ( data.nickname, (Race)data.race );
                TransitionManager.Instance.OnSceneTransition ( SceneName.GetMainLobby ( GameManager.Instance.LocalPlayer.playerInfo.Race ), TransitionType.Slide, null );
            }
            else
            {
                BasePage.OnMessageBox ( "닉네임의 길이가 잘못 되었습니다.\n(3글자 ~ 6글자)", true, null, "확인" );
            }
        }
    }
}