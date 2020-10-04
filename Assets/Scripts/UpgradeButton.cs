using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{

    public Text upgradeDisplay;
   
    public string upgradeName;

    [HideInInspector]
    public int goldByUpgrade;

    public int startGoldByUpgrade;

    [HideInInspector]
    public int currentCost = 1;

    public int StartCurrentCost = 1;

    public int level = 1;

    public float upgradePow = 1.06f;

    public float costPow = 3.14f;

    private void Start()
    {
        DataController.Instance.LoadUpgradeButton(this);
        UpdateUI();
    }


    //현재 gold와 가격이 부합되는지 검사한다. 
    public void PurchaseUpgrade()
    {
        if(DataController.Instance.gold >= currentCost)
        {
            DataController.Instance.gold -= currentCost;
            level++;
            DataController.Instance.goldPerClick += goldByUpgrade;

            UpdateUpgrade();
            UpdateUI();
            //부합된다면 저장 
            DataController.Instance.SaveUpgradeButton(this);
        }
    }


    public void UpdateUpgrade()
    {
        //level 승 
        goldByUpgrade = startGoldByUpgrade * (int)Mathf.Pow(upgradePow, level);
        currentCost = StartCurrentCost * (int)Mathf.Pow(costPow, level);
    }


    
    public void UpdateUI()
    {
        upgradeDisplay.text = upgradeName + "\nCost:" + currentCost + "\nLevel" + level + "\nText GoldPerClick: " + goldByUpgrade;
    }
}
