using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ActorOrder : int
{ 
    Idle = 0,       // 아무런 액션을 취하지 않음
    Stop,
    Attack,
    Move,
    Wander,         // 주변을 방황합니다.
    
}

public interface IAIFactory
{
    void SetOrder ( ActorOrder order );
}