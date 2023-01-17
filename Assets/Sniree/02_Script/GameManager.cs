using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isLearn;
    public GameObject tgt;
    public string[] selectedCharacter;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    private void Update() {
        if(isLearn){
            tgt = GameObject.FindGameObjectWithTag("Finish");
            tgt.GetComponent<SceneChanger>().isLearn = true;
            isLearn = !false;
        }
    }


}
