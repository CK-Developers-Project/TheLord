namespace Developers.Net.Protocol
{
    public abstract class BaseProtocol
    {
        public virtual void SendPacket ( bool isWait = false )
        {
            if ( isWait ) TransitionManager.Instance.OnWaitSigh ( isWait );
        }
    }
}
