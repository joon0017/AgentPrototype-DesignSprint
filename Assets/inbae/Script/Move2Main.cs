using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move2Main : MonoBehaviour
{
    public string[] selectedChar = new string[3];
    public void MoveMain()
    {
        GameObject Obj = GameObject.Find("Layout1");
        if(Obj.transform.childCount != 0)
        {
            selectedChar[0] = Obj.transform.GetChild(0).name;
        }
            
        
        Obj = GameObject.Find("Layout2");
        if(Obj.transform.childCount != 0)
        {
            selectedChar[1] = Obj.transform.GetChild(0).name;
        }

        Obj = GameObject.Find("Layout3");
        if(Obj.transform.childCount != 0)
        {
            selectedChar[2] = Obj.transform.GetChild(0).name;
        }

        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.selectedCharacter = selectedChar;
            
        SceneManager.LoadScene("Fight");
    }
}
