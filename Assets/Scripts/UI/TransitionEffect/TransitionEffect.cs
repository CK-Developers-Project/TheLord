using System;
using System.Collections.Generic;
using UnityEngine;
using Developers.Structure;

public abstract class TransitionEffect : MonoBehaviour
{
    public TransitionType type;

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