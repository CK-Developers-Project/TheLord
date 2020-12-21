using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Developers.Table;
using Developers.Structure;
using Developers.Structure.Data;
using Developers.Net.Protocol;
using Spine.Unity;

public class BuildingInfoPopup : BasePopup
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
                if ( image == null )
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
    class SpineCharacter
    {
        public SkeletonGraphic spineGraphic;

        public void Change ( SkeletonDataAsset asset )
        {
            spineGraphic.skeletonDataAsset = asset;
            spineGraphic.startingAnimation = "idle";
        }
    }

    [System.Serializable]
    class SkillInfo
    {
        public Image icon;
        public TextMeshProUGUI name;
        public TextMeshProUGUI descript;
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
        public TextMeshProUGUI people;
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
    [SerializeField] SpineCharacter spineCharacter = null;

    [SerializeField] SkillInfo skillInfo = null;
    [SerializeField] RaceColor raceColor = null;
    [SerializeField] CharacterInfo charactertInfo = null;

    [SerializeField] List<RaceSprite> raceSprites = null;


    public BuildingInfo info { get; set; }

    public void SetTarget ( CharacterData target, AbilityData skill, BuildingInfo buildingInfo )
    {
        illustImage.Change ( target.illust );
        spineCharacter.Change ( target.asset );

        skillInfo.icon.sprite = skill.icon;
        skillInfo.name.text = skill.Name;
        skillInfo.descript.text = skill.Descript;

        foreach ( var rs in raceSprites )
        {
            if ( rs.race == target.Race )
            {
                raceColor.title.sprite = rs.title;
                raceColor.illust.sprite = rs.illust;
                raceColor.notice.sprite = rs.notice;
                break;
            }
        }

        info = buildingInfo;

        charactertInfo.name.text = target.Name;
        charactertInfo.hp.text = ( (int)target.HP ).ToString ( );
        charactertInfo.atk.text = ( (int)target.Atk ).ToString ( );
        charactertInfo.def.text = ( (int)target.Def ).ToString ( );
    }

    public void LevelUp()
    {
        if(info == null)
        {
            return;
        }
        var packet = new BuildingClickRequest ( );
        packet.index = (int)info.index;
        packet.clickAction = ClickAction.BuildingLevelUp;
        packet.SendPacket ( true, true );
    }

    public void Cancel()
    {
        gameObject.SetActive ( false );
    }

    public override void OnUpdate ( )
    {
        if ( info != null )
        {
            charactertInfo.level.text = info.LV.ToString ( );
            charactertInfo.people.text = info.amount.ToString();
        }
    }
}
