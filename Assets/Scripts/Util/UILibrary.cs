using UnityEngine.UI;
using TMPro;

namespace Developers.Util
{
    public class UILibrary
    {
        public static float TextWidthApproximation(TextMeshPro textMeshPro)
        {
            float pointSizeScale = textMeshPro.fontSize / (textMeshPro.font.faceInfo.pointSize * textMeshPro.font.faceInfo.scale);
            float emScale = textMeshPro.fontSize * 0.01f;

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
    }
}