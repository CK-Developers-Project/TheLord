using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
   
    public void GameStart()
    {
        SceneManager.LoadScene("TribleSelect");
    }

    public void SelectTribe(string tribeName)
    {

            SceneManager.LoadScene(tribeName);
        
    }


    
}
