using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectedManager : MonoBehaviour
{
    public GameObject GameManager;
    public GameObject Char1Script;
    public GameObject Char2Script;

    public string agentName1;
    public string agentName2;

    void Start()
    {
        GameManager = GameObject.Find("GameManager");
    }

    public void Selected2GameManager()
    {
            if (Char1Script.transform.childCount != 0 && Char2Script.transform.childCount != 0)
            {
                agentName1 = Char1Script.transform.GetChild(0).gameObject.name;
                agentName2 = Char2Script.transform.GetChild(0).gameObject.name;

                GameManager.GetComponent<GameManager>().selectedCharacter[0] = agentName1;
                GameManager.GetComponent<GameManager>().selectedCharacter[1] = agentName2;

                SceneManager.LoadScene("MapSelect");
            }
    } 
}
