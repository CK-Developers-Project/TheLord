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

    /// <summary>
    /// 동기화 데이터
    /// </summary>
    public class SynchronizeData
    {
        public List<BuildingInfo> buildingInfoList = new List<BuildingInfo> ( );

        public void SetBuildingInfo ( List<BuildingInfo> buildingInfoList )
        {
            this.buildingInfoList.Clear ( );
            this.buildingInfoList.AddRange ( buildingInfoList );
        }

        public BuildingInfo GetBuildingInfo(BuildingType type)
        {
             return buildingInfoList.Find ( x => x.index == type );
        }
    }

    // /************* 캐릭터 정보 *************/

    public enum Race : int
    {
        Elf = 1,
        Human,
        Undead,
        End
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
        EmainBuilding                = 1,       // 엘프 메인건물
        HmainBuilding                = 101,     // 인간 메인건물
        UmainBuilding                = 201,     // 언데드 메인건물

        elfarcherBuilding            = 301,     // 엘프 아처
        druidBuilding                = 401,     // 엘프 드루이드
        bardBuilding                 = 501,     // 엘프 바드
        spiritBuilding               = 601,     // 엘프 영혼술사
        guardianBuilding             = 701,     // 엘프 가디언

        archerBuilding               = 801,     // 인간 아처
        warriorBuilding              = 901,     // 인간 전사
        shieldbearerBuilding         = 1001,    // 인간 방패병
        gunnerBuilding               = 1101,    // 인간 거너
        priestBuilding               = 1201,    // 인간 프리스트

        UwarriorBuilding             = 1301,    // 언데드 전사
        witchBuilding                = 1401,    // 언데드 마녀
        reaperBuilding               = 1501,    // 언데드 리퍼
        necromancerBuilding          = 1601,    // 언데드 네크로맨서
        soulknightBuilding           = 1701,    // 언데드 소울나이트
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
        public BuildingType index;
        public BuildingState state;
        public int LV;
        public string name;
        public DateTime workTime;
    }
}