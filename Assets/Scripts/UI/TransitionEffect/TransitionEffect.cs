using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class TransitionEffect : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)]
    protected float effectFactor;
    [SerializeField]
    protected AnimationCurve curve = null;

    protected Action @event;

    public bool IsFade { get => effectFactor > 0f; }
    public abstract float EffectFactor { get; set; }

    public abstract void OnFadeIn ( Action callback = null );
    public abstract void OnFadeOut ( Action callback = null );
}