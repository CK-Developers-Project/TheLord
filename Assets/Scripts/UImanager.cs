using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UImanager : MonoBehaviour
{

    public Text GoldDisPlayer;

    public Text GoldPerClickDisplayer;

    public Text goldpersecDisplayer;
    
    
    

    List<string> MoneyName = new List<string>();
    private void Update()
    {

        string data = GoldPerdisplay();

        string moneyunit = GoldDisplay();

        GoldPerClickDisplayer.text = "GOLD DISPLAY: " + DataController.Instance.goldPerClick;
        
        goldpersecDisplayer.text = "GOLD PER SEC:" + data;
        
        GoldDisPlayer.text = "GOLD:" + moneyunit;
    }

    private void Start()
    {
        //StartCoroutine(GoldDisplay());
    }

    private string GoldDisplay()
    {
        int Len = DataController.Instance.gold.ToString().Length;
        string currenttext = "";
        string finaltext ="";
        switch(Len)
        {
            case (int)Itembutton.lebate.a:
                 currenttext = DataController.Instance.gold.ToString().Substring(0, 5);
                 finaltext = currenttext.Insert(2, ",");
                 finaltext = finaltext + Itembutton.lebate.a.ToString();
                break;         
            case (int)Itembutton.lebate.b:
                currenttext = DataController.Instance.gold.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.b.ToString();
                break;
            case (int)Itembutton.lebate.c:
                currenttext = DataController.Instance.gold.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.c.ToString();
                break;
            case (int)Itembutton.lebate.d:
                currenttext = DataController.Instance.gold.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.d.ToString();
                break;
            case (int)Itembutton.lebate.e:
                currenttext = DataController.Instance.gold.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.e.ToString();
                break;
            case (int)Itembutton.lebate.f:
                currenttext = DataController.Instance.gold.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.f.ToString();
                break;
            case (int)Itembutton.lebate.g:
                currenttext = DataController.Instance.gold.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.g.ToString();
                break;
            case (int)Itembutton.lebate.h:
                currenttext = DataController.Instance.gold.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.h.ToString();
                break;
            case (int)Itembutton.lebate.i:
                currenttext = DataController.Instance.gold.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.i.ToString();
                break;
            case (int)Itembutton.lebate.j:
                currenttext = DataController.Instance.gold.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.j.ToString();
                break;
            case (int)Itembutton.lebate.k:
                currenttext = DataController.Instance.gold.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.k.ToString();
                break;
            case (int)Itembutton.lebate.l:
                currenttext = DataController.Instance.gold.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.l.ToString();
                break;
            case (int)Itembutton.lebate.M:
                currenttext = DataController.Instance.gold.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.M.ToString();
                break;
            case (int)Itembutton.lebate.N:
                currenttext = DataController.Instance.gold.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.N.ToString();
                break;
            case (int)Itembutton.lebate.O:
                currenttext = DataController.Instance.gold.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.O.ToString();
                break;
            case (int)Itembutton.lebate.P:
                currenttext = DataController.Instance.gold.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.P.ToString();
                break;
            case (int)Itembutton.lebate.Q:
                currenttext = DataController.Instance.gold.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.Q.ToString();
                break;
            case (int)Itembutton.lebate.R:
                currenttext = DataController.Instance.gold.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.R.ToString();
                break;
            case (int)Itembutton.lebate.S:
                currenttext = DataController.Instance.gold.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.S.ToString();
                break;
            case (int)Itembutton.lebate.T:
                currenttext = DataController.Instance.gold.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.T.ToString();
                break;
            case (int)Itembutton.lebate.U:
                currenttext = DataController.Instance.gold.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.U.ToString();
                break;
            case (int)Itembutton.lebate.V:  
                currenttext = DataController.Instance.gold.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.V.ToString();
                break;
            case (int)Itembutton.lebate.W:
                currenttext = DataController.Instance.gold.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.W.ToString();
                break;
            case (int)Itembutton.lebate.X:
                currenttext = DataController.Instance.gold.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.X.ToString();
                break;
            case (int)Itembutton.lebate.Y:
                currenttext = DataController.Instance.gold.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.Y.ToString();
                break;
            case (int)Itembutton.lebate.Z:
                currenttext = DataController.Instance.gold.ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.Z.ToString();
                break;

            default:
                 finaltext =  DataController.Instance.gold.ToString();
                break;


        }

        return finaltext;
    }



    private string GoldPerdisplay()
    {
        int Len = DataController.Instance.GetGoldperSec().ToString().Length;
        string currenttext = "";
        string finaltext = "";
        switch (Len)
        {
            case (int)Itembutton.lebate.a:
                currenttext = DataController.Instance.GetGoldperSec().ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.a.ToString();
                break;
            case (int)Itembutton.lebate.b:
                currenttext = DataController.Instance.GetGoldperSec().ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.b.ToString();
                break;
            case (int)Itembutton.lebate.c:
                currenttext = DataController.Instance.GetGoldperSec().ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.c.ToString();
                break;
            case (int)Itembutton.lebate.d:
                currenttext = DataController.Instance.GetGoldperSec().ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.d.ToString();
                break;
            case (int)Itembutton.lebate.e:
                currenttext = DataController.Instance.GetGoldperSec().ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.e.ToString();
                break;
            case (int)Itembutton.lebate.f:
                currenttext = DataController.Instance.GetGoldperSec().ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.f.ToString();
                break;
            case (int)Itembutton.lebate.g:
                currenttext = DataController.Instance.GetGoldperSec().ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.g.ToString();
                break;
            case (int)Itembutton.lebate.h:
                currenttext = DataController.Instance.GetGoldperSec().ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.h.ToString();
                break;
            case (int)Itembutton.lebate.i:
                currenttext = DataController.Instance.GetGoldperSec().ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.i.ToString();
                break;
            case (int)Itembutton.lebate.j:
                currenttext = DataController.Instance.GetGoldperSec().ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.j.ToString();
                break;
            case (int)Itembutton.lebate.k:
                currenttext = DataController.Instance.GetGoldperSec().ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.k.ToString();
                break;
            case (int)Itembutton.lebate.l:
                currenttext = DataController.Instance.GetGoldperSec().ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.l.ToString();
                break;
            case (int)Itembutton.lebate.M:
                currenttext = DataController.Instance.GetGoldperSec().ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.M.ToString();
                break;
            case (int)Itembutton.lebate.N:
                currenttext = DataController.Instance.GetGoldperSec().ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.N.ToString();
                break;
            case (int)Itembutton.lebate.O:
                currenttext = DataController.Instance.GetGoldperSec().ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.O.ToString();
                break;
            case (int)Itembutton.lebate.P:
                currenttext = DataController.Instance.GetGoldperSec().ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.P.ToString();
                break;
            case (int)Itembutton.lebate.Q:
                currenttext = DataController.Instance.GetGoldperSec().ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.Q.ToString();
                break;
            case (int)Itembutton.lebate.R:
                currenttext = DataController.Instance.GetGoldperSec().ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.R.ToString();
                break;
            case (int)Itembutton.lebate.S:
                currenttext = DataController.Instance.GetGoldperSec().ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.S.ToString();
                break;
            case (int)Itembutton.lebate.T:
                currenttext = DataController.Instance.GetGoldperSec().ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.T.ToString();
                break;
            case (int)Itembutton.lebate.U:
                currenttext = DataController.Instance.GetGoldperSec().ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.U.ToString();
                break;
            case (int)Itembutton.lebate.V:
                currenttext = DataController.Instance.GetGoldperSec().ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.V.ToString();
                break;
            case (int)Itembutton.lebate.W:
                currenttext = DataController.Instance.GetGoldperSec().ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.W.ToString();
                break;
            case (int)Itembutton.lebate.X:
                currenttext = DataController.Instance.GetGoldperSec().ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.X.ToString();
                break;
            case (int)Itembutton.lebate.Y:
                currenttext = DataController.Instance.GetGoldperSec().ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.Y.ToString();
                break;
            case (int)Itembutton.lebate.Z:
                currenttext = DataController.Instance.GetGoldperSec().ToString().Substring(0, 5);
                finaltext = currenttext.Insert(2, ",");
                finaltext = finaltext + Itembutton.lebate.Z.ToString();
                break;

            default:
                finaltext = DataController.Instance.GetGoldperSec().ToString();
                break;


        }

        return finaltext;
    }




}








    
    
