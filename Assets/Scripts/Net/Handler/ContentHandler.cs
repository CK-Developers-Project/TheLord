using UnityEngine;
using ExitGames.Client.Photon;
using System;

namespace Developers.Net.Handler
{
    using Structure;
    using Util;
    using Protocol;
    using Table;

    public class ContentHandler : BaseHandler
    {
        public override void AddListener ( )
        {
            HandlerMedia.AddListener ( OperationCode.EnterContent, OnEnterContentReceived );
            HandlerMedia.AddListener ( OperationCode.EnterRaid, OnEnterRaidReceived );
        }

        public override void RemoveListener ( )
        {
            HandlerMedia.RemoveAllListener ( OperationCode.EnterContent );
            HandlerMedia.RemoveAllListener ( OperationCode.EnterRaid );
        }

        
        void OnEnterContentReceived(OperationResponse response)
        {
            var returnCode = (ReturnCode)response.ReturnCode;
            if(returnCode == ReturnCode.Success)
            {
                string sceneName = SceneName.Raid_1;
                TransitionManager.Instance.OnSceneTransition ( sceneName, TransitionType.Loading01_Blank, null );
            }
            else
            {
                BasePage.OnMessageBox ( "입장이 불가능합니다.", true, null, "확인" );
            }
        }

        void OnEnterRaidReceived( OperationResponse response )
        {
            ReturnCode rc = (ReturnCode)response.ReturnCode;
            if ( rc == ReturnCode.Failed )
            {
                string sceneName = SceneName.GetMainLobby ( GameManager.Instance.LocalPlayer.playerInfo.Race );
                TransitionManager.Instance.OnSceneTransition ( sceneName, TransitionType.Loading01_Blank, null );
                Debug.LogError ( "로비 데이터를 받는데 실패했습니다." );
                return;
            }

            GameManager.Instance.GameMode.OnSynchronize ( BinSerializer.ConvertData<ProtoData.RaidEnterData> ( response.Parameters ) );
        }
    }
}
