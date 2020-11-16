using UnityEngine;
using System;
using System.IO;
using ProtoBuf;

namespace Developers.Util
{
    public class BinSerializer
    {
        public static byte[] Serialize<T> ( T t )
        {
            try
            {
                using ( MemoryStream ms = new MemoryStream ( ) )
                {
                    Serializer.Serialize<T> ( ms, t );
                    byte[] result = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read ( result, 0, result.Length );
                    return result;
                }
            }
            catch ( Exception e )
            {
                Debug.Log ( "Serialize Fail : " + e.ToString ( ) );
                return null;
            }
        }

        public static T Deserialize<T> ( byte[] msg )
        {
            try
            {
                using ( MemoryStream ms = new MemoryStream ( ) )
                {
                    ms.Write ( msg, 0, msg.Length );
                    ms.Position = 0;
                    return Serializer.Deserialize<T> ( ms );
                }
            }
            catch ( Exception e )
            {
                Debug.Log ( "Deserialize Fail : " + e.ToString ( ) );
                return default ( T );
            }
        }
    }
}
