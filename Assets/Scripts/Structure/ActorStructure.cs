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

    [Serializable]
    public class CharacterInfo
    { 
    }
    

    // /************* 건물 정보 *************/
    
    public enum BuildingType
    {
        Castle,                            // 기지
        SpearmanTrainingCenter,            // 창병 훈련소
        WarriorTrainingCenter,             // 전사 훈련소
    }

    public enum BuildingState
    {
        Empty,                              // 없음
        Work,                               // 건설중
        Completion,                         // 완공
    }


    [Serializable]
    public class BuildingInfo
    {
        public BuildingType type;
        public BuildingState state;
    }
}