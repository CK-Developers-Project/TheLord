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
        public TierType Tier { get; set; }

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

    public enum TierType : int
    {
        Unrank = 0,
        Challanger,
        Iron,
        Bronze,
        Silver,
        Gold,
        Platinum,
        Diamond,
        Master,
        Lengend,
        God,
    }
}