using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginPage : BasePage
{
    #region Platform Class
    [Serializable]
    class Tester
    {
        public TMP_InputField IDinputField = null;
        public Button loginButton = null;
        
    }

    [Serializable]
    class AOS
    { 
    }

    [Serializable]
    class IOS
    {

    }
    #endregion

    [SerializeField] Tester platformTester = null;
    [SerializeField] AOS platformAOS = null;
    [SerializeField] IOS platformIOS = null;



    public override void Initialize ( )
    {
        base.Initialize ( );
    }


    protected override void Construct ( )
    {
        
    }

}