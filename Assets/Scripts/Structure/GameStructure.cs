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
        ActorPath,

        Actor = 12,
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


    public enum MusicType
    {
        Login,
        MainLobby_Elf,
        MainLobby_Human,
        MainLobby_Undead,
        RaidBattle,
        RaidLobby,
        Story,
    }

    public enum SFXType
    {
        BossSkill,
        Bow,
        Build,
        BuildClear,
        Coin,
        ElfSkill,
        Flute,
        GunShot,
        Hit,
        HumanSkill,
        Magic,
        Reward,
        Strong,
        Sword,
        Tabsound,
        UndeadSkill,
    }

    public enum VFXType
    {
        Attack,
        BossSkill,
        BlackDust,
        ElfSkill,
        HumanSkill,
        ScreenTouch,
        UndeadSkill,
    }
}
