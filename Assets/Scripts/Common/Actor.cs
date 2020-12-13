using UnityEngine;

public interface IActor
{
    AbilityCaster Caster {get; set;}

    int Index { get; }
    bool Synchronized { get; set; }

    void Initialize ( );
    void Load ( );

    void OnSelect ( );
}