using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public string SceneName;
    private Button btn;
    public bool isLearn;
    private void Start() {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(ChangeScene);
    }
    
    void ChangeScene()
    {
        SceneManager.LoadScene(SceneName);
        if(isLearn){
            SceneManager.LoadScene("TrainFight");
        }
    }
}
