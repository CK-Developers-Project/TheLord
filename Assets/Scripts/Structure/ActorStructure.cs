using Developers.Structure.Data;
using System;
using System.Collections.Generic;

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
        Dictionary<int, object> table;

        public ActorAbility()
        {
            for(int i = 0; i < (int)ActorAbilityType.Aspeed; ++i )
            {
                table.Add ( i, 0 );
            }
        }

        public ActorAbility(params object[] data)
        {
            int cnt = 0;
            foreach(var item in data)
            {
                table.Add ( cnt++, item );
            }
        }

        public T Get<T>(ActorAbilityType type)
        {
            int key = (int)type;
            return table.ContainsKey ( key ) ? (T)table[key] : default;
        }
    }

    [Serializable]
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


        #region Get Ability Methods
        public int Get2Int ( ActorAbilityType type, bool normal = true, bool additional = false, bool multiplicative = false )
        {
            int amount = 0;
            if ( normal )
            {
                amount += this.normal.Get<int> ( type );
            }
            if ( additional )
            {
                amount += this.additional.Get<int> ( type );
            }
            if ( multiplicative )
            {
                amount += this.multiplicative.Get<int> ( type );
            }
            return amount;
        }

        public float Get2Float ( ActorAbilityType type, bool normal = true, bool additional = false, bool multiplicative = false )
        {
            float amount = 0F;
            if ( normal )
            {
                amount += this.normal.Get<float> ( type );
            }
            if ( additional )
            {
                amount += this.additional.Get<float> ( type );
            }
            if ( multiplicative )
            {
                amount += this.multiplicative.Get<float> ( type );
            }
            return amount;
        }
        #endregion
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