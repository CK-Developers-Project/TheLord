using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace Developers.Structure
{
    using Data;

    public enum AbilityType : int
    {
        Natural = 0,
        Active = 1,
        Passive = 2,
    }

    ///<summary>명령</summary>
    public enum AbilityOrder : int
    {
        Idle = 0,       // 아무런 액션을 취하지 않음
        Stop,
        Attack,
        Move,
        BossSkill,
    }

    public enum AbilityStatus
    {
        Cast,
        Duration,
        Range,
        Distance,
        Cooltime,
        End
    }

    [System.Serializable]
    public class AbilityInfo
    {
        public Status<AbilityStatus> status = new Status<AbilityStatus> ( (int)AbilityStatus.End );
        public List<int> amount = new List<int> ( );

        public bool isUse = false;
        public float cooltime = 0;

        public AbilityInfo ( AbilityData data )
        {
            Data = data;
            Name = Data.Name;
            AbilityType = Data.AbilityType;
            status.Normal.Set ( AbilityStatus.Cast, Data.Cast );
            status.Normal.Set ( AbilityStatus.Duration, Data.Duration );
            status.Normal.Set ( AbilityStatus.Range, Data.Range );
            status.Normal.Set ( AbilityStatus.Distance, Data.Distance );
            status.Normal.Set ( AbilityStatus.Cooltime, Data.Cooltime );
            amount = Data.Amount;
        }

        public bool OnStart ( BaseCharacter owner )
        {
            return Data.OnStart ( owner, this );
        }
        public bool OnStart ( BaseCharacter owner, Vector3 position )
        {
            return Data.OnStart ( owner, this, position );
        }
        public bool OnStart ( BaseCharacter owner, IActor target )
        {
            return Data.OnStart ( owner, this, target );
        }

        public IEnumerator UpdateCoolTime()
        {
            while(cooltime > 0)
            {
                cooltime -= Time.deltaTime;
                yield return null;
            }
            isUse = false;
            cooltime = 0F;
        }

        public AbilityData Data { get; protected set; }
        public AbilityOrder Order { get => Data.order; }
        public string Name { get; private set; }
        public AbilityType AbilityType { get; private set; }
        public float Cast { get => status.Get ( AbilityStatus.Cast, true, true, true ); }         // 시전 시간
        public float Duration { get => status.Get ( AbilityStatus.Duration, true, true, true ); }  // 스킬 시전 후 지속시간
        public float Range { get => status.Get ( AbilityStatus.Range, true, true, true ); }     // 범위
        public float Distance { get => status.Get ( AbilityStatus.Distance, true, true, true ); }  // 사정거리
        public float Cooltime { get => status.Get ( AbilityStatus.Cooltime, true, true, true ); }  // 재사용 시간
    }
}