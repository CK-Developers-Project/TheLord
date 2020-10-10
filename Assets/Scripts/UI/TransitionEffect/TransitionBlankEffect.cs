using System;
using UnityEngine;
using UnityEngine.UI;
using Coffee.UIEffects;

public class TransitionBlankEffect : TransitionEffect
{
    [SerializeField] Image image = null;
    [SerializeField] UITransitionEffect EffectComponent = null;

    public override float EffectFactor
    { 
        get => effectFactor; 
        set
        {
            effectFactor = value;
            Color color = image.color;
            color.a = effectFactor;
            image.color = color;
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
        EffectFactor = image.color.a + curve.Evaluate ( value );
        
        if ( image.color.a >= 1f )
        {
            EffectComponent.effectPlayer.OnDisable ( );
            @event?.Invoke ( );
            @event = null;
        }
    }

    void FadeOut ( float value )
    {
        Color color = image.color;
        color.a = color.a - curve.Evaluate ( value );
        image.color = color;

        if ( image.color.a <= 0f )
        {
            EffectComponent.effectPlayer.OnDisable ( );
            @event?.Invoke ( );
            @event = null;
        }
    }

#if UNITY_EDITOR
    private void OnValidate ( )
    {
        EffectFactor = effectFactor;
    }
#endif

    private void Awake ( )
    {
        effectFactor = image.color.a;
    }
}
