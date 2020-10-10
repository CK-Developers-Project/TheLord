using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Developers.Util;
using Developers.Structure;

public class IntroGameMode : BaseGameMode
{

    [SerializeField] ImageHelper schoolLogo = null;
    [SerializeField] ImageHelper teamLogo = null;

    bool isWork = false;


    IEnumerator ShowLogo ( )
    {
        if(isWork)
        {
            yield break;
        }
        isWork = true;
        yield return StartCoroutine ( schoolLogo.Appear ( 1f ) );
        yield return new WaitForSecondsRealtime ( 2f );
        yield return StartCoroutine ( schoolLogo.Disappear ( 1f ) );
        yield return StartCoroutine ( teamLogo.Appear ( 1f ) );
        yield return new WaitForSecondsRealtime ( 2f );
        yield return StartCoroutine ( teamLogo.Disappear ( 1f ) );
        MonoSingleton<TransitionManager>.Instance.OnSceneTransition ( SceneName.Login, TransitionType.Blank, TransitionType.Slide );
        isWork = false;
    }


    public override void OnEnter ( )
    {
        MonoSingleton<TransitionManager>.Instance.OnFadeOut ( TransitionType.Blank, ( ) => { StartCoroutine ( ShowLogo ( ) ); } );
    }
}