using Developers.Structure.Data;
using Developers.Util;
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

    public enum Race : int
    {
        Elf = 1,
        Human,
        Undead
    }

    [Serializable]
    public class CharacterInfo
    {
        public int index;
        public int price;
        public string name;

        public List<int> abilitys;

        public int damage;
        public int armor;
        public int life;
        public int speed;
        public int attackspeed;
        public int distance;

        // TODO : 캐릭터 특성 (특성이 여러개일 가능성이 있다면 List인데...)
    
        //    
    }

    public enum ActorStatus : int
    {
        Damage,             // 피해량
        Armor,              // 방어력
        Life,               // 체력
        Speed,              // 이동속도
        AattackSpeed,       // 공격 쿨타임
        Distance,           // 공격 사정거리
        CastSpeed,          // 마법 시전 속도 *
        AbilityTime,        // 마법 재사용 시간 *
        End
    }

    public class Status<T> where T : struct
    {
        public ValueTable<T> Normal { get; private set; }
        public ValueTable<T> Additional { get; private set; }
        public ValueTable<T> Multiplicative { get; private set; }

        public Status ( int max ) => (Normal, Additional, Multiplicative)
            = (new ValueTable<T> ( max ), new ValueTable<T> ( max ), new ValueTable<T> ( max ));

        public int Get ( T type, bool normal = true, bool additional = false, bool multiplicative = false )
        {
            int amount = 0;
            if ( normal )
            {
                amount += this.Normal[type];
            }
            if ( additional )
            {
                amount += this.Additional[type];
            }
            if ( multiplicative )
            {
                amount += this.Multiplicative[type];
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