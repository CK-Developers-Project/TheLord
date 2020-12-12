using System.Collections.Generic;

namespace Developers.Structure
{
    using Util;

    public enum ResourceType : byte
    {
        Gold,
        Cash,
    }

    public class PlayerInfo
    {
        public string Nickname { get; set; }

        public Race Race { get; set; }

        Dictionary<ResourceType, BigInteger> resource = new Dictionary<ResourceType, BigInteger> ( );

        public BigInteger GetResource(ResourceType type)
        {
            if ( !resource.ContainsKey ( type ) )
            {
                resource.Add ( type, new BigInteger ( 0 ) );
            }

            return resource[type];
        }

        public void SetResource(ResourceType type, BigInteger value)
        {
            if ( !resource.ContainsKey ( type ) )
            {
                resource.Add ( type, value );
            }
            else
            {
                resource[type] = value;
            }
        }
    }
}