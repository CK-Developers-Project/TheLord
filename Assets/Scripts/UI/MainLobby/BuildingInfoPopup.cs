using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Developers.Structure;

public class BuildingInfoPopup : MonoBehaviour, IGameUI
{
    [System.Serializable]
    class IllustImage
    {
        public RectTransform transform;

        AspectRatioFitter aspectRatioFitter;
        Image image;

        AspectRatioFitter AspectRatioFitter {
            get
            {
                if ( aspectRatioFitter == null )
                {
                    aspectRatioFitter = transform.GetComponent<AspectRatioFitter> ( );
                }
                return aspectRatioFitter;
            }
        }

        Image Image {
            get
            {
                if ( AspectRatioFitter == null )
                {
                    image = transform.GetComponent<Image> ( );
                }
                return image;
            }
        }


        public void Change ( Sprite sprite )
        {
            AspectRatioFitter.enabled = false;

            Image.sprite = sprite;
            RectTransformExtensions.SetAnchor ( transform, AnchorPresets.MiddleCenter );
            transform.sizeDelta = new Vector2 ( sprite.rect.width, sprite.rect.height );
            bool bWidth = transform.rect.width > transform.rect.height;

            AspectRatioFitter.AspectMode aspectMode = bWidth ?
                AspectRatioFitter.AspectMode.WidthControlsHeight : AspectRatioFitter.AspectMode.HeightControlsWidth;

            float aspectRatio = bWidth ?
                transform.rect.height / transform.rect.width : transform.rect.width / transform.rect.height;

            AspectRatioFitter.aspectMode = aspectMode;
            AspectRatioFitter.aspectRatio = aspectRatio;
            AspectRatioFitter.enabled = true;

            RectTransformExtensions.SetAnchor ( transform, AnchorPresets.StretchAll );
            transform.sizeDelta = Vector2.zero;
            transform.anchoredPosition = Vector2.zero;
        }
    }

    [System.Serializable]
    class SkillInfo
    {
        public Image icon;
        public TextMeshProUGUI name;
        public TextMeshProUGUI description;
    }

    [System.Serializable]
    class RaceColor
    {
        public Image title;
        public Image illust;
        public Image notice;
    }

    [System.Serializable]
    class CharacterInfo
    {
        public TextMeshProUGUI level;
        public TextMeshProUGUI name;
        public TextMeshProUGUI hp;
        public TextMeshProUGUI atk;
        public TextMeshProUGUI def;
    }

    [System.Serializable]
    class RaceSprite
    {
        public Race race;
        public Sprite title;
        public Sprite illust;
        public Sprite notice;
    }


    [SerializeField] IllustImage illustImage = null;
    [SerializeField] SkillInfo skillInfo = null;
    [SerializeField] RaceColor raceColor = null;
    [SerializeField] CharacterInfo charactertInfo = null;

    [SerializeField] List<RaceSprite> raceSprites = null;




    public void OnUpdate ( )
    {
        
    }
}
