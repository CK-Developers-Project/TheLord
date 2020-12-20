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
}
