using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Developers.Util;
using Developers.Structure;

public class GamePlayer : MonoBehaviour
{

    public PlayerInfo playerInfo = new PlayerInfo();

    List<GamePlayer> Teams = new List<GamePlayer> ( );

    #region 팀 설정

    public void AddAlliance(GamePlayer player)
    {
        if(!Teams.Contains(player))
        {
            Teams.Add ( player );
        }
    }

    public void RemoveAlliance(GamePlayer player)
    {
        if(Teams.Contains(player))
        {
            Teams.Remove ( player );
        }
    }

    public bool IsEnemy(GamePlayer player)
    {
        return !Teams.Exists ( x => x.Equals ( player ) );
    }

    public bool IsAlliance(GamePlayer player)
    {
        return Teams.Exists ( x => x.Equals ( player ) );
    }

    #endregion



    public void Initialize(string nickname, Race race)
    {
        Teams.Add ( this );
        playerInfo.Nickname = nickname;
        playerInfo.Race = race;
    }

    public string DisplayGold ( ResourceType type )
    {
        return GameUtility.Ordinal ( playerInfo.GetResource ( type ) );
    }

    public BigInteger GetGold(ResourceType type)
    {
        return playerInfo.GetResource ( type );
    }

    public void SetGold( ResourceType type, BigInteger value )
    {
        playerInfo.SetResource ( type, value );
    }

    public void SetGold(ResourceType type, string value)
    {
        playerInfo.SetResource ( type, new BigInteger ( value ) );
    }

    public void AddGold(ResourceType type, BigInteger value)
    {
        playerInfo.SetResource ( type, playerInfo.GetResource ( type ) + value );
    }

    public void AddGold(ResourceType type, string value)
    {
        playerInfo.SetResource ( type, playerInfo.GetResource ( type ) + new BigInteger ( value ) );
    }
}
