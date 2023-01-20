using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStratSystem : MonoBehaviour
{

    private void Update(){
        if(Input.anyKeyDown){
            PlayerPrefs.SetInt("Tutorial",3);
            SceneManager.LoadScene("SelectChar");
        }
    }
}
