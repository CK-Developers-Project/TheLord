using System.Collections.Generic;
using UnityEngine;

namespace Developers.Net.Protocol
{
    using Structure;

    public abstract class BaseProtocol
    {
        protected bool reliable;

        public virtual void SendPacket ( bool isWait = false, bool reliable = true )
        {
            this.reliable = reliable;
            if ( isWait ) TransitionManager.Instance.OnWaitSigh ( isWait );
        }

        protected void Send(OperationCode code, Dictionary<byte, object> parameters)
        {
            if ( PhotonEngine.Peer == null || !PhotonEngine.Instance.isServerConnect )
            {
                Debug.LogWarning ( "서버 연결이 안되어있습니다." );
                return;
            }
            PhotonEngine.Peer.OpCustom ( (byte)code, parameters, reliable );
        }
    }
}
