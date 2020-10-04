using UnityEngine;

public interface IActor
{
    int Index { get; }

    void Load ( );

    void OnSelect ( );
}