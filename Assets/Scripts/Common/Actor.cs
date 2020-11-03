using UnityEngine;

public interface IActor
{
    AbilityCaster Caster {get; set;}

    int Index { get; }

    void Load ( );

    void OnSelect ( );
}