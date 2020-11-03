using Developers.Structure.Data;
using Developers.Util;
using System;

namespace Developers.Structure
{
    ///<summary>액터의 종류</summary>
    public enum ActorType
    {
        None = 0,           // 
        Character,          // 캐릭터
        Building,           // 건물
        End
    }

    ///<summary>가져올 액터의 자료</summary>
    [Serializable]
    public struct ActorRecord
    {
        public ActorType type;
        public int index;
        public ActorRecord ( ActorType actorType, int recordKey ) => (type, index) = (actorType, recordKey);
    }

    ///<summary>명령</summary>
    public enum ActorOrder : int
    {
        Idle = 0,       // 아무런 액션을 취하지 않음
        Stop,
        Attack,
        Move,
        Wander,         // 주변을 방황합니다.

    }

    // /************* 캐릭터 정보 *************/

    public enum Race
    {
        Elf,
        Human,
        Undead
    }

    [Serializable]
    public class CharacterInfo
    {
        public int index;
        public int price;
        public string name;

        public float damage;
        public float armor;
        public float life;
        public float speed;
        public float aspeed;

        // TODO : 캐릭터 특성 (특성이 여러개일 가능성이 있다면 List인데...)
    
        //    
    }

    public enum ActorAbilityType : int
    {
        Damage,             // 피해량
        Armor,              // 방어력
        Life,               // 체력
        Speed,              // 이동속도
        Aspeed,             // 공격 애니메이션 속도
    }

    [Serializable]
    public class ActorAbility
    {
        EnumDictionary<ActorAbilityType, int> table = new EnumDictionary<ActorAbilityType, int>();

        public ActorAbility()
        {
            for(int i = 0; i < (int)ActorAbilityType.Aspeed; ++i)
            {
                table.Add ( (ActorAbilityType)i, 0 );
            }
        }

        public ActorAbility(params int[] data)
        {
            int cnt = 0;
            foreach(var item in data)
            {
                table.Add ( (ActorAbilityType)cnt++, item );
            }
        }

        public int Get(ActorAbilityType type)
        {
            return table.ContainsKey (type) ? table[type] : default;
        }

        public void Set(ActorAbilityType type, int value)
        {
            if(table.ContainsKey(type))
            {
                table[type] = value;
            }
            else
            {
                table.Add(type, value);
            }
        }
    }

    public class CharacterAbility
    {
        public ActorAbility normal;             // 기본
        public ActorAbility additional;         // 추가량
        public ActorAbility multiplicative;     // 비율 추가량

        public CharacterAbility()
        {
            normal = new ActorAbility ( );
            additional = new ActorAbility ( );
            multiplicative = new ActorAbility ( );
        }

        public int Get ( ActorAbilityType type, bool normal = true, bool additional = false, bool multiplicative = false )
        {
            int amount = 0;
            if ( normal )
            {
                amount += this.normal.Get ( type );
            }
            if ( additional )
            {
                amount += this.additional.Get ( type );
            }
            if ( multiplicative )
            {
                amount += this.multiplicative.Get ( type );
            }
            return amount;
        }


    }



    // /************* 건물 정보 *************/

    public enum BuildingType : int
    {
        Castle,                            // 기지
        SpearmanTrainingCenter,            // 창병 훈련소
        WarriorTrainingCenter,             // 전사 훈련소
    }

    public enum BuildingState
    {
        Empty,                              // 없음
        Work,                               // 건설중
        Complete,                           // 완공
    }


    [Serializable]
    public class BuildingInfo
    {
        public BuildingType type;
        public BuildingState state;
        public int level;
        public int price;
        public string name;

        public CharacterData characterData;

        // 좀더 생각해봐야할 데이터 자료형들
        public int current;
        public int max;
    }
}