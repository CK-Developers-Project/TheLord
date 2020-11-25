namespace Developers.Net.Protocol
{
    public abstract class BaseProtocol
    {
        public abstract void SendPacket ( bool isWait = false );
    }
}
