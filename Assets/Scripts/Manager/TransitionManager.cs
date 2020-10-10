using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Developers.Util;
using Developers.Structure;


public class TransitionManager : MonoSingleton<TransitionManager>
{

    static EnumDictionary<TransitionType, TransitionEffect> transitionEffects = new EnumDictionary<TransitionType, TransitionEffect> ( );
    
    public TransitionEffect current { get; private set; }
    bool isWork = false;


    public void OnSceneTransition(string sceneName, TransitionType transitionType, Action callback = null)
    {
        isWork = false;
        Action action = ( ) =>
        {
            callback?.Invoke ( );
            StartCoroutine ( SceneTransition ( sceneName ) );
        };

        current = transitionEffects[transitionType];
        current.gameObject.SetActive ( true );
        current.EffectFactor = 0f;
        current.OnFadeIn ( action );
    }

    public void OnSceneTransition ( string sceneName, TransitionType startTransitionType, TransitionType endTransitionType, Action callback = null )
    {
        isWork = false;
        Action action = ( ) =>
        {
            callback?.Invoke ( );
            current.gameObject.SetActive ( false );
            current = transitionEffects[endTransitionType];
            current.gameObject.SetActive ( true );
            current.EffectFactor = 1f;
            StartCoroutine ( SceneTransition ( sceneName ) );
        };

        current = transitionEffects[startTransitionType];
        current.gameObject.SetActive ( true );
        current.EffectFactor = 0f;
        current.OnFadeIn ( action );
    }

    public void OnTransition(TransitionType transitionType, Action callback_FadeIn = null, Action callback_FadeOut = null)
    {
        Action action = ( ) =>
        {
            callback_FadeIn?.Invoke ( );
            Action endAction = ( ) =>
            {
                callback_FadeOut?.Invoke ( );
                current.gameObject.SetActive ( false );
                current = null;
            };
            current.OnFadeOut ( endAction );
        };

        current = transitionEffects[transitionType];
        current.gameObject.SetActive ( true );
        current.EffectFactor = 0f;
        current.OnFadeIn ( action );
    }

    public void OnTransition ( TransitionType startTransitionType, TransitionType endTransitionType, Action callback_FadeIn = null, Action callback_FadeOut = null )
    {
        Action action = ( ) =>
        {
            callback_FadeIn?.Invoke ( );
            current.gameObject.SetActive ( false );
            current = transitionEffects[endTransitionType];
            current.gameObject.SetActive ( true );
            current.EffectFactor = 1f;

            Action endAction = ( ) =>
            {
                callback_FadeOut?.Invoke ( );
                current.gameObject.SetActive ( false );
                current = null;
            };
            current.OnFadeOut ( endAction );
        };

        current = transitionEffects[startTransitionType];
        current.gameObject.SetActive ( true );
        current.EffectFactor = 0f;
        current.OnFadeIn ( action );
    }

    /// <param name="effectFactor">만약 음수라면 원래의 값을 사용합니다.</param>
    public void OnFadeIn ( TransitionType transitionType, Action callback = null, float effectFactor = 0f )
    {
        Action action = ( ) =>
        {
            callback?.Invoke ( );
        };

        current = transitionEffects[transitionType];
        current.gameObject.SetActive ( true );
        if ( effectFactor >= 0f )
        {
            current.EffectFactor = effectFactor;
        }
        current.OnFadeIn ( action );
    }


    /// <param name="effectFactor">만약 음수라면 원래의 값을 사용합니다.</param>
    public void OnFadeOut ( TransitionType transitionType, Action callback = null, float effectFactor = 1f )
    {
        Action action = ( ) =>
        {
            callback?.Invoke ( );
            current.EffectFactor = 0f;
            current.gameObject.SetActive ( false );
            current = null;
        };

        current = transitionEffects[transitionType];
        current.gameObject.SetActive ( true );
        if ( effectFactor >= 0f )
        {
            current.EffectFactor = effectFactor;
        }
        current.OnFadeOut ( action );
    }

    public TransitionEffect GetTransitionEffect(TransitionType type)
    {
        return transitionEffects[type];
    }


    IEnumerator SceneTransition(string sceneName )
    {
        if(isWork)
        {
            yield break;
        }
        isWork = true;


        var handle = SceneManager.LoadSceneAsync ( sceneName );
        handle.allowSceneActivation = false;

        while(!handle.isDone)
        {
            if(handle.progress >= 0.9f)
            {
                handle.allowSceneActivation = true;
            }
            yield return null;
        }

        
        // TODO : 게임매니저로부터 게임모드 초기화

        Action endAction = ( ) =>
        {
            current.gameObject.SetActive ( false );
            current = null;
        };

        current.OnFadeOut ( endAction );
        isWork = false;
    }

    protected override void Awake ( )
    {
        base.Awake ( );

        if(Instance == this)
        {
            var components = GetComponentsInChildren<TransitionEffect> ( true );
            foreach(var com in components)
            {
                switch(com)
                {
                    case TransitionBlankEffect blank:
                        transitionEffects.Add ( TransitionType.Blank, blank );
                        break;
                    case TransitionSlideEffect slide:
                        transitionEffects.Add ( TransitionType.Slide, slide );
                        break;
                }
            }
        }
    }
}
