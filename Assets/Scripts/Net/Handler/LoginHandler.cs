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

                    break;
                case NextAction.LoginFailed:

                    break;
                case NextAction.UserCreateFail:

                    break;
                case NextAction.UserInfoCreate:

                    break;
            }
        }
    }
}
