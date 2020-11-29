using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Developers.Util;
using Developers.Structure;

public class GamePlayer : MonoBehaviour
{

    public PlayerInfo playerInfo = new PlayerInfo();


    public void Initialize(string nickname, Race race)
    {
        playerInfo.Nickname = nickname;
        playerInfo.Race = race;
    }

    public string DisplayGold ( ResourceType type )
    {
        BigInteger a = playerInfo.GetResource ( type );
        a = 10000;
        BigInteger temp = a;

        while(a >= 1000)
        {
            a /= 1000;
        }



        return "";
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
