using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{

    public int HasMoney;


    //SingleTon
    public static PlayerManager Instance;

    public Text MonetSeeText;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("빌드를 2개이상 가질 수 없습니다.");
        }

        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        MonetSeeText.text = HasMoney.ToString();
    }
}
