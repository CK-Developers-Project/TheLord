using Developers.Structure.Data;
using Developers.Util;
using System;

namespace Developers.Structure
{
    ///<summary>명령</summary>
    public enum AbilityOrder : int
    {
        Idle = 0,       // 아무런 액션을 취하지 않음
        Stop,
        Attack,
        Move,
        Wander,         // 주변을 방황합니다.

    }

    public enum AbilityValue
    {
        Cast,
        Duration,
        Range,
        Distance,
        Amount,
        Cooltime,
    }

    public class AbilityInfo
    {
        EnumDictionary<AbilityValue, int> table = new EnumDictionary<AbilityValue, int> ( );

        public AbilityInfo ( )
        {
            for ( int i = 0; i < (int)AbilityValue.Cooltime; ++i )
            {
                table.Add ( (AbilityValue)i, 0 );
            }
        }

        public AbilityInfo ( params int[] data )
        {
            int cnt = 0;
            foreach ( var item in data )
            {
                table.Add ( (AbilityValue)cnt++, item );
            }
        }

        public int Get ( AbilityValue type )
        {
            return table.ContainsKey ( type ) ? table[type] : default;
        }

        public void Set ( AbilityValue type, int value )
        {
            if ( table.ContainsKey ( type ) )
            {
                table[type] = value;
            }
            else
            {
                table.Add ( type, value );
            }
        }
    }
}