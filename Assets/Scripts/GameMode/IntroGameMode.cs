using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[ExecuteInEditMode]
public class IntroGameMode : BaseGameMode
{
    // Start is called before the first frame update
    void Start()
    {
        Keyboard a = Keyboard.current;
        a.SetIMEEnabled ( true );
        
        //IMECompositionMode.Auto;
    }
}
