using System.Collections.Generic;

namespace Developers.Net.Protocol
{
    using Structure;

    public abstract class BaseProtocol
    {
        public virtual void SendPacket ( bool isWait = false )
        {
            if ( isWait ) TransitionManager.Instance.OnWaitSigh ( isWait );
        }

        protected void Send(OperationCode code, Dictionary<byte, object> parameters, bool reliable)
        {
            if ( PhotonEngine.Peer == null || !PhotonEngine.Instance.isServerConnect )
            {
                return;
            }
            PhotonEngine.Peer.OpCustom ( (byte)code, parameters, reliable );
        }
    }
}
