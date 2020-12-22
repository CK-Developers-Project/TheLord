using UnityEngine;

public delegate void FOnDeath ( );
public delegate void FOnAttack ( BaseCharacter target );
public delegate void FOnHit ( BaseCharacter target );
public delegate void FOnDamaged ( BaseCharacter source, BaseCharacter target, DamageCalculator.DamageInfo info );

public interface IActor
{
    GamePlayer Owner { get; set; }

    int Index { get; }
    bool Synchronized { get; set; }
    bool Initialized { get; set; }

    bool Anim_Event { get; set; }

    void Initialize ( );
    void Load ( );
    void OnUpdate ( );

    void OnSelect ( );
}