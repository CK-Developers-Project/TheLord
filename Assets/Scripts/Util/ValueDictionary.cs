using System.Collections.Generic;

namespace Developers.Util
{
    public class ValueTable<T>
    {
        Dictionary<int, float> table = new Dictionary<int, float> ( );

        static TValue To<TValue>(object value) { return (TValue)System.Convert.ChangeType ( value, typeof ( TValue ) ); }

        public ValueTable ( int max )
        {
            for ( int i = 0; i < max; ++i )
            {
                table.Add ( i, 0 );
            }
        }

        public ValueTable (params float[] values)
        {
            for ( int i = 0; i < values.Length; ++i )
            {
                table.Add ( i, values[i] );
            }
        }
        
        public float Get ( T type )
        {
            int key = To<int> ( type );
            return table.ContainsKey ( key ) ? table[key] : -1;
        }

        public void Set ( T type, float value )
        {
            int key = To<int> ( type );
            if ( table.ContainsKey ( key ) )
            {
                table[key] = value;
            }
            else
            {
                table.Add ( key, value );
            }
        }

        public float this[T key] 
        {
            get => Get ( key );
            set => Set ( key, value );
        }
    }
}
