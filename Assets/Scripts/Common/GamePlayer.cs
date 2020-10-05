using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Developers.Util;
using Developers.Structure;

public class GamePlayer : MonoBehaviour
{

    public PlayerInfo playerInfo;


    



    public static GamePlayer Create()
    {

        return null;
    }



    public int GetGold()
    {
        // TODO : 서버로부터 골드 값을 불러와야함
        return playerInfo.Gold;
    }


    public void SetGold<T>(T gold) where T : IGoldTransfer
    {
        // TODO : 서버로부터 골드 값 갱신 요청

        // FIXME : 현재는 서버연동이 안되어있으므로 로컬 골드값 갱신
        playerInfo.Gold += gold.Cost;
    }

    

    

    public static void Initialize()
    {
        //MonoSingleton<GameManager>.Instance.gamePlayers.Add()
    }


}
