using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Developers.Util;
using Developers.Structure;

public class GamePlayer : MonoBehaviour
{

    public PlayerInfo playerInfo = new PlayerInfo();

    public List<BaseCharacter> characters = new List<BaseCharacter> ( );

    List<BaseCharacter> selectCharacter = new List<BaseCharacter> ( );
    public List<BaseCharacter> SelectCharacter {
        get
        {
            selectCharacter.RemoveAll ( x => x == null );
            return selectCharacter;
        }
    }

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

    #region 유닛 설정

    public BaseCharacter GetCharacter(int i)
    {
        if(characters.Count <= i)
        {
            return null;
        }

        var result = characters[i];

        if ( characters[i] == null )
            characters.RemoveAll ( x => x == null );

        return result;
    }

    public List<BaseCharacter> GetCharacter<T> ( ) where T : BaseCharacter
    {
        characters.RemoveAll ( x => x == null );
        return characters.FindAll ( x => x is T );
    }

    public List<BaseCharacter> GetCharacterAll()
    {
        characters.RemoveAll ( x => x == null );
        return characters;
    }

    public void CharacterAdd(BaseCharacter characater)
    {
        characters.Add ( characater );
        characater.Owner = this;
    }

    #endregion


    public void Initialize(string nickname, Race race)
    {
        Teams.Add ( this );
        playerInfo.Nickname = nickname;
        playerInfo.Race = race;

        foreach ( var character in characters )
        {
            character.Owner = this;
        }
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
