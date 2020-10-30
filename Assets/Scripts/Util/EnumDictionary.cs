using System.Collections.Generic;

namespace Developers.Util
{
    public class EnumDictionary<TKey, TValue> : IEnumerable<KeyValuePair<int, TValue>> where TKey : unmanaged
    {
        private Dictionary<int, TValue> internalDictionary = new Dictionary<int, TValue> ( );

        public IEnumerator<KeyValuePair<int, TValue>> GetEnumerator ( ) => internalDictionary.GetEnumerator ( );

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ( ) => internalDictionary.GetEnumerator ( );

        public TValue this[TKey key] {
            get => internalDictionary[ConvertToIndex ( key )];
            set => Add ( key, value );
        }
        public TValue this[int key]
        {
            get => internalDictionary[key];
            set => Add(key, value);
        }


        public void Add ( TKey key, TValue values )
        {
            if ( !internalDictionary.TryGetValue ( ConvertToIndex ( key ), out TValue storedValues ) )
            {
                internalDictionary.Add ( ConvertToIndex ( key ), values );
            }
            storedValues = values;
        }

        public void Add(int key, TValue values)
        {
            if (!internalDictionary.TryGetValue(key, out TValue storedValues))
            {
                internalDictionary.Add(key, values);
            }
            storedValues = values;
        }


        public void Remove ( TKey key )
        {
            if ( internalDictionary.ContainsKey ( ConvertToIndex ( key ) ) )
            {
                internalDictionary.Remove ( ConvertToIndex ( key ) );
            }
        }

        public void Remove(int key)
        {
            if (internalDictionary.ContainsKey(key))
            {
                internalDictionary.Remove(key);
            }
        }


        public bool ContainsKey ( TKey key )
        {
            return internalDictionary.ContainsKey ( ConvertToIndex ( key ) );
        }

        public bool ContainsKey(int key)
        {
            return internalDictionary.ContainsKey(key);
        }



        public static unsafe int ConvertToIndex ( TKey key )
        {
            return *(int*)&key;
        }
    }
}
