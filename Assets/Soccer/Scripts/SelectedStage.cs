using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectedStage : MonoBehaviour
{
    public GameObject GameManager;

    void Start()
    {
        GameManager = GameObject.Find("GameManager");
    }
    
    public void StageSelected()
    {
        GameManager.GetComponent<GameManager>().selectedStage = gameObject.name;
        SceneManager.LoadScene("CharacterSelect");
    }
}
