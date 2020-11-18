using UnityEngine;
using ExitGames.Client.Photon;

namespace Developers.Net.Handler
{
    using Structure;

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
        }

        public override void RemoveListener ( )
        {
            HandlerMedia.RemoveAllListener ( OperationCode.Login );
        }

        void OnLoginReceived (OperationResponse response)
        {
            switch((NextAction)response.ReturnCode)
            {
                case NextAction.LoginSuccess:
                    // TODO : 내 메인로비로 이동
                    break;
                case NextAction.LoginFailed:
                    BasePage.OnMessageBox("로그인에 실패하셨습니다.", true, null, "확인");
                    break;
                case NextAction.UserCreateFail:
                    BasePage.OnMessageBox("ID길이가 잘못 되었습니다.\n(2글자 ~ 10글자)", true, null, "확인");
                    break;
                case NextAction.UserInfoCreate:
                    // TODO : 종족선택창으로 이동
                    break;
            }
        }
    }
}
