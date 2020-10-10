using System;
using Coffee.UIEffects;
using UnityEngine;

public class TransitionSlideEffect : TransitionEffect
{
    [SerializeField] UITransitionEffect EffectComponent = null;

    public override float EffectFactor { 
        get => effectFactor; 
        set
        {
            effectFactor = value;
            EffectComponent.effectFactor = effectFactor;
        }
    }

    public override void OnFadeIn ( Action callback )
    {
        @event = callback;
        EffectComponent.effectPlayer.OnDisable ( );
        EffectComponent.effectPlayer.OnEnable ( FadeIn );
        EffectComponent.effectPlayer.Play ( true );
    }

    public override void OnFadeOut ( Action callback )
    {
        @event = callback;
        EffectComponent.effectPlayer.OnDisable ( );
        EffectComponent.effectPlayer.OnEnable ( FadeOut );
        EffectComponent.effectPlayer.Play ( true );
    }

    void FadeIn ( float value )
    {
        EffectFactor = EffectFactor + curve.Evaluate ( value );

        if ( EffectComponent.effectFactor >= 1f)
        {
            EffectComponent.effectPlayer.OnDisable ( );
            @event?.Invoke ( );
            @event = null;
        }
    }

    void FadeOut ( float value )
    {
        EffectFactor = EffectFactor - curve.Evaluate ( value );

        if ( EffectComponent.effectFactor <= 0f )
        {
            EffectComponent.effectPlayer.OnDisable ( );
            @event?.Invoke ( );
            @event = null;
        }
    }

#if UNITY_EDITOR
    private void OnValidate ( )
    {
        EffectComponent.effectFactor = effectFactor;
    }
#endif

    private void Awake ( )
    {
        effectFactor = EffectComponent.effectFactor;
    }
}