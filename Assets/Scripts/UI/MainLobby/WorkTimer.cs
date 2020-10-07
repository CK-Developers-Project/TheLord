using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


public class WorkTimer : MonoBehaviour
{
    [SerializeField] TextMeshPro WorkTimeText;
    SpriteRenderer spriteRenderer;

    [HideInInspector] public bool IsComplete { get; set; }




    DateTime targetTime;

    IEnumerator Runnable()
    {
        while (!IsComplete)
        {
            TimeSpan current = targetTime - DateTime.Now;
            WorkTimeText.SetText(string.Format("{0}:{1:D2}", (int)current.TotalMinutes, current.Seconds));
            spriteRenderer.size = new Vector2 ( TextWidthApproximation ( WorkTimeText ), spriteRenderer.size.y );
            yield return null;
        }
    }

    
    private float TextWidthApproximation(TextMeshPro textMeshPro)
    {
        float pointSizeScale = textMeshPro.fontSize / (textMeshPro.font.faceInfo.pointSize * textMeshPro.font.faceInfo.scale);
        float emScale = WorkTimeText.fontSize * 0.01f;

        float styleSpacingAdjustment = (textMeshPro.fontStyle & FontStyles.Bold) == FontStyles.Bold ? textMeshPro.font.boldSpacing : 0;
        float normalSpacingAdjustment = textMeshPro.font.normalSpacingOffset;

        float width = 0;

        for(int i = 0; i < textMeshPro.text.Length; ++i)
        {
            char unicode = textMeshPro.text[i];
            TMP_Character character;
            if(textMeshPro.font.characterLookupTable.TryGetValue(unicode, out character))
                width += character.glyph.metrics.horizontalAdvance * pointSizeScale + (styleSpacingAdjustment + normalSpacingAdjustment) * emScale;
        }
        return width;
    }



    private void Awake ( )
    {
        spriteRenderer = GetComponent<SpriteRenderer> ( );
    }

    private void Start()
    {
        targetTime = DateTime.Now + new TimeSpan(2,0, 10);
        StartCoroutine(Runnable());
    }
}
