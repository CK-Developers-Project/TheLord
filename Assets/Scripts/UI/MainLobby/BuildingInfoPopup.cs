using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class BuildingInfoPopup : MonoBehaviour
{
    public Sprite[] sprites;
    int index = 0;

    [SerializeField] AspectRatioFitter aspectRatioFitter;
    [SerializeField] Image image;


    public bool isSwitch = false;


    private void Update()
    {
        if(isSwitch)
        {
            aspectRatioFitter.enabled = false;
            image.sprite = sprites[index++];
            image.SetNativeSize();
            

            isSwitch = false;
        }
    }


}
