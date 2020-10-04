using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ScottGarland;

public class Itembutton : MonoBehaviour
{

    public Text ItemDisplayer;

    public string ItemName;

    public int itemLevel = 0;

    [HideInInspector]
    public BigInteger currentcoast;

    public BigInteger startcurrentcoast;

    [HideInInspector]
    public BigInteger goldpersecond;

    public BigInteger startgoldpersecond;

    public float coastpow = 3.14f;

    public float upgradepow = 1.07f;

    [HideInInspector]
    public bool isPurchased = false;

    [HideInInspector]
    public bool isGoldpercheck;

    private string coastNumber;

    private void Start()
    {

        //currentcoast = startcurrentcoast;
        //  goldpersecond = startgoldpersecond;

        DataController.Instance.LoadItemButton(this);
        StartCoroutine("AddGoldLoop");

        isGoldpercheck = true;

        UpdateUI();
    }


    
    //Item 구매 유효성 검사 
    public void PurchaseItem()
    {
        if(DataController.Instance.gold >= currentcoast)
        {
            isPurchased = true;
            DataController.Instance.gold -= currentcoast;
            itemLevel++;

            UpdateItem();
            UpdateUI();

            DataController.Instance.SaveItemButton(this);
        }
    }


    //0.1초마다 GOLD 추가
    IEnumerator AddGoldLoop()
    {
        while(true)
        {
            if(isPurchased)
            {
                DataController.Instance.gold += goldpersecond;
            }

            yield return new WaitForSeconds(1.0f);
        }
    }


    //item에 능력을 update해주는 함수 
   public void UpdateItem()
    {
       isGoldpercheck = true;
      long math =   (long)Mathf.Pow(upgradepow, itemLevel);
      long test = (long)Mathf.Pow(coastpow, itemLevel);

        goldpersecond = math;

        currentcoast = test;
        
    }

    //item ui를 보여주는 함수 
    public void UpdateUI()
    {

        string CoastMoney = checkcoast();
        string coastgoldpersec = checkgoldpersec();

        ItemDisplayer.text = ItemName + "\n Level: " + itemLevel + "\n coast: " + CoastMoney + "\n Gold Persec:" + coastgoldpersec
        + "\n ispurchased: " + isPurchased;
    }


    private string checkcoast()
    {

        
        int Len = goldpersecond.ToString().Length;
        string currenttext = "";
        string finaltext = "";
        switch (Len)
        {
            case (int)Itembutton.lebate.a:
                currenttext = goldpersecond.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.a.ToString();
                break;
            case (int)Itembutton.lebate.b:
                currenttext = goldpersecond.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.b.ToString();
                break;
            case (int)Itembutton.lebate.c:
                currenttext = goldpersecond.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.c.ToString();
                break;
            case (int)Itembutton.lebate.d:
                currenttext = goldpersecond.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.d.ToString();
                break;
            case (int)Itembutton.lebate.e:
                currenttext = goldpersecond.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.e.ToString();
                break;
            case (int)Itembutton.lebate.f:
                currenttext = goldpersecond.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.f.ToString();
                break;
            case (int)Itembutton.lebate.g:
                currenttext = goldpersecond.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.g.ToString();
                break;
            case (int)Itembutton.lebate.h:
                currenttext = goldpersecond.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.h.ToString();
                break;
            case (int)Itembutton.lebate.i:
                currenttext = goldpersecond.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.i.ToString();
                break;
            case (int)Itembutton.lebate.j:
                currenttext = goldpersecond.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.j.ToString();
                break;
            case (int)Itembutton.lebate.k:
                currenttext = goldpersecond.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.k.ToString();
                break;
            case (int)Itembutton.lebate.l:
                currenttext = goldpersecond.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.l.ToString();
                break;
            case (int)Itembutton.lebate.M:
                currenttext = goldpersecond.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.M.ToString();
                break;
            case (int)Itembutton.lebate.N:
                currenttext = goldpersecond.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.N.ToString();
                break;
            case (int)Itembutton.lebate.O:
                currenttext = goldpersecond.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.O.ToString();
                break;
            case (int)Itembutton.lebate.P:
                currenttext = goldpersecond.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.P.ToString();
                break;
            case (int)Itembutton.lebate.Q:
                currenttext = goldpersecond.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.Q.ToString();
                break;
            case (int)Itembutton.lebate.R:
                currenttext = goldpersecond.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.R.ToString();
                break;
            case (int)Itembutton.lebate.S:
                currenttext = goldpersecond.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.S.ToString();
                break;
            case (int)Itembutton.lebate.T:
                currenttext = goldpersecond.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.T.ToString();
                break;
            case (int)Itembutton.lebate.U:
                currenttext = goldpersecond.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.U.ToString();
                break;
            case (int)Itembutton.lebate.V:
                currenttext = goldpersecond.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.V.ToString();
                break;
            case (int)Itembutton.lebate.W:
                currenttext = goldpersecond.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.W.ToString();
                break;
            case (int)Itembutton.lebate.X:
                currenttext = goldpersecond.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.X.ToString();
                break;
            case (int)Itembutton.lebate.Y:
                currenttext = goldpersecond.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.Y.ToString();
                break;
            case (int)Itembutton.lebate.Z:
                currenttext = goldpersecond.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.Z.ToString();
                break;

            default:
                finaltext = goldpersecond.ToString();
                break;


        }

        return finaltext;
    }



    private string checkgoldpersec()
    {


        int Len = currentcoast.ToString().Length;
        string currenttext = "";
        string finaltext = "";
        switch (Len)
        {
            case (int)Itembutton.lebate.a:
                currenttext = currentcoast.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.a.ToString();
                break;
            case (int)Itembutton.lebate.b:
                currenttext = currentcoast.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.b.ToString();
                break;
            case (int)Itembutton.lebate.c:
                currenttext = currentcoast.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.c.ToString();
                break;
            case (int)Itembutton.lebate.d:
                currenttext = currentcoast.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.d.ToString();
                break;
            case (int)Itembutton.lebate.e:
                currenttext = currentcoast.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.e.ToString();
                break;
            case (int)Itembutton.lebate.f:
                currenttext = currentcoast.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.f.ToString();
                break;
            case (int)Itembutton.lebate.g:
                currenttext = currentcoast.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.g.ToString();
                break;
            case (int)Itembutton.lebate.h:
                currenttext = currentcoast.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.h.ToString();
                break;
            case (int)Itembutton.lebate.i:
                currenttext = currentcoast.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.i.ToString();
                break;
            case (int)Itembutton.lebate.j:
                currenttext = currentcoast.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.j.ToString();
                break;
            case (int)Itembutton.lebate.k:
                currenttext = currentcoast.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.k.ToString();
                break;
            case (int)Itembutton.lebate.l:
                currenttext = currentcoast.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.l.ToString();
                break;
            case (int)Itembutton.lebate.M:
                currenttext = currentcoast.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.M.ToString();
                break;
            case (int)Itembutton.lebate.N:
                currenttext = currentcoast.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.N.ToString();
                break;
            case (int)Itembutton.lebate.O:
                currenttext = currentcoast.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.O.ToString();
                break;
            case (int)Itembutton.lebate.P:
                currenttext = currentcoast.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.P.ToString();
                break;
            case (int)Itembutton.lebate.Q:
                currenttext = currentcoast.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.Q.ToString();
                break;
            case (int)Itembutton.lebate.R:
                currenttext = currentcoast.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.R.ToString();
                break;
            case (int)Itembutton.lebate.S:
                currenttext = currentcoast.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.S.ToString();
                break;
            case (int)Itembutton.lebate.T:
                currenttext = currentcoast.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.T.ToString();
                break;
            case (int)Itembutton.lebate.U:
                currenttext = currentcoast.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.U.ToString();
                break;
            case (int)Itembutton.lebate.V:
                currenttext = currentcoast.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.V.ToString();
                break;
            case (int)Itembutton.lebate.W:
                currenttext = currentcoast.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.W.ToString();
                break;
            case (int)Itembutton.lebate.X:
                currenttext = currentcoast.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.X.ToString();
                break;
            case (int)Itembutton.lebate.Y:
                currenttext = currentcoast.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.Y.ToString();
                break;
            case (int)Itembutton.lebate.Z:
                currenttext = currentcoast.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.Z.ToString();
                break;

            default:
                finaltext = currentcoast.ToString();
                break;


        }

        return finaltext;
    }




    public enum lebate
    {
        a = 5,
        b = 6,
        c = 7,
        d = 8,
        e = 9,
        f = 10,
        g = 11,
        h = 12,
        i = 13,
        j = 14,
        k = 15,
        l = 16,
        M = 17,
        N = 18,
        O = 19,
        P = 20,
        Q = 21,
        R = 22,
        S = 23,
        T = 24,
        U = 25,
        V = 26,
        W = 27,
        X = 28,
        Y = 29,
        Z = 30

    };
}
