
namespace Developers.Structure
{
    ///<summary>명령</summary>
    public enum AbilityOrder : int
    {
        Idle = 0,       // 아무런 액션을 취하지 않음
        Stop,
        Attack,
        Move,
        Wander,         // 주변을 방황합니다.
    }

    public enum AbilityStatus
    {
        Cast,
        Duration,
        Range,
        Distance,
        Amount,
        Cooltime,
        End
    }

    public class AbilityInfo
    {
        public int index;
        public string name;
        public int abilityType; // 타입(기본, 패시브, 액티브)
        public int cast;        // 시전 시간
        public int duration;    // 스킬 시전 후 지속시간
        public int range;       // 범위
        public int distance;    // 사정거리
        public int cooltime;    // 재사용 시간
        public int amount;
    }
}