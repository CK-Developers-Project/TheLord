using UnityEngine;

namespace Developers.Structure
{
    using Util;

    public enum GameLayer : int
    {
        Default = 0,
        TransparentFX,
        IgnoreRaycast,
        
        Water = 4,
        UI = 5,
        
        Path = 8,
        Actor
    }

    public static class GameLayerHelper
    {
        public static LayerMask Layer(params GameLayer[] gameLayers)
        {
            LayerMask layerMask = 0;
            foreach(var layer in gameLayers)
            {
                layerMask.value |= 1 << (int)layer;
            }
            return layerMask;
        }
    }

    public static class Utility
    {
        static string[] ordinals = new[] { "", "K", "M", "T", "q", "Q", "s", "S", "O", "N", "D" };

        public static string Ordinal(BigInteger num)
        {
            BigInteger temp = num;
            BigInteger origin = temp;
            int space = 0;

            while ( temp >= 1000 )
            {
                temp /= 1000;
                ++space;
            }

            if ( space >= ordinals.Length )
            {
                space = ordinals.Length - 1;
            }

            string strOrigin = origin.ToString ( ), strTemp = temp.ToString ( );

            int d = 1;
            string c = "0";

            if ( space > 0 )
            {
                d = strOrigin.Length - strTemp.Length;
                c = strOrigin.Substring ( strTemp.Length, ( d < 2 ) ? d : 2 );
            }

            return string.Format ( "{0} {1}", strTemp + ( ( d > 0 && int.Parse ( c ) == 0 ) ? "" : "," + c ), ordinals[space] );
        }

    }
}
