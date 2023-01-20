using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneData : MonoBehaviour
{
    public GameManager manager;
    public int id;

    private void Awake(){
        if(id == PlayerPrefs.GetInt("Tutorial")){
            manager.click(id);
        }
        else{
            manager.tutorialPanel.SetActive(false);
        }
    }

    private void Update(){
        if(Input.anyKeyDown && manager.isClick){
            manager.click(id);
        }
    }
}
