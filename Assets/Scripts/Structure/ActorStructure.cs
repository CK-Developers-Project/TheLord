using Developers.Structure.Data;
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

    [Serializable]
    public struct ActorAbility
    {
        public float damage;
        public float armor;
        public float life;
        public float speed;
        public float aspeed;

        public ActorAbility(float damage, float armor, float life, float speed, float aspeed)
            => (this.damage, this.armor, this.life, this.speed, this.aspeed) 
            = (damage, armor, life, speed, aspeed);
    }

    [Serializable]
    public class CharacterAbility
    {
        public ActorAbility normal;             // 기본
        public ActorAbility additional;         // 추가량
        public ActorAbility multiplicative;     // 비율 추가량
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