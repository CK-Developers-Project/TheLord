using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Developers.Util;
using Developers.Structure;

public class GamePlayer : MonoBehaviour
{

    public PlayerInfo playerInfo = new PlayerInfo();

    readonly string[] ordinals = new[] { "", "K", "M", "T", "q", "Q", "s", "S", "O", "N", "D" };


    public void Initialize(string nickname, Race race)
    {
        playerInfo.Nickname = nickname;
        playerInfo.Race = race;
    }

    public string DisplayGold ( ResourceType type )
    {
        BigInteger temp = playerInfo.GetResource ( type );
        BigInteger origin = temp;
        int space = 0;

        while(temp >= 1000)
        {
            temp /= 1000;
            ++space;
        }

        if(space >= ordinals.Length)
        {
            space = ordinals.Length - 1;
        }

        string strOrigin = origin.ToString(), strTemp = temp.ToString();

        int d = 1;
        string c = "0";

        if(space > 0)
        {
            d = strOrigin.Length - strTemp.Length;
            c = strOrigin.Substring(strTemp.Length, (d < 2) ? d : 2);
        }

        return string.Format("{0} {1}", strTemp + ((d > 0 && int.Parse(c) == 0) ? "" : "," + c), ordinals[space]);
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
