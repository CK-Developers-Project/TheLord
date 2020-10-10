using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageHelper : MonoBehaviour
{
    public Image Image { get; set; }
    public Color Color { get => Image.color; set => Image.color = value; }
    public float a 
    { 
        get => Image.color.a; 
        set
        {
            Color color = Image.color;
            color.a = value;
            Image.color = color;
        }
    }

    bool appear = false;
    bool disappear = false;

    public IEnumerator Appear( float dur, float target = 1f )
    {
        if(appear)
        {
            yield break;
        }
        appear = true;
        dur = Mathf.Max ( 0.01f, dur );

        float timer = 0f;
        while(appear)
        {
            if(disappear)
            {
                yield break;
            }
            timer += Time.deltaTime;
            a = timer / dur;
            if(a >= target)
            {
                a = target;
                break;
            }
            yield return null;
        }
        appear = false;
    }

    public IEnumerator Disappear( float dur, float target = 0f )
    {
        if(disappear)
        {
            yield break;
        }
        disappear = true;
        dur = Mathf.Max ( 0.01f, dur );

        float timer = 0f;
        while ( disappear )
        {
            if ( appear )
            {
                yield break;
            }
            timer += Time.deltaTime;
            a = 1f - ( timer / dur );
            if ( a <= target )
            {
                a = target;
                break;
            }
            yield return null;
        }
        disappear = false;
    }


    private void Awake ( )
    {
        Image = GetComponent<Image> ( );
    }
}
