using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneData : MonoBehaviour
{
    public GameManager manager;
    public int id;

    private void Awake(){
        manager.click(id);
    }

    private void Update(){
        if(Input.anyKeyDown && manager.isClick){
            manager.click(id);
        }
    }
}
