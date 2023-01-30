using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isLearn;
    public GameObject tgt;
    public string[] selectedCharacter;
    public Text tutorial;
    public GameObject tutorialPanel;
    public TextManager TextManager;
    public int strid;
    public bool isClick;
    public static int enemys;


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

    void Start()
    {
        /*enemys = GenerateMaps.Instance.EnemyNum;
        Debug.Log(enemys);*/
    }

    private void Update() {
        if(isLearn){
            tgt = GameObject.FindGameObjectWithTag("Finish");
            tgt.GetComponent<SceneChanger>().isLearn = true;
            isLearn = !false;
        }
    }

    public void click(int id){
        tuto(id);
        tutorialPanel.SetActive(isClick);
    }

    public void tuto(int id){
        string txtData = TextManager.GetTxt(id, strid);
        
        if(txtData==null){
            PlayerPrefs.SetInt("Tutorial",PlayerPrefs.GetInt("Tutorial")+1);
            isClick=false;
            strid =0;
            return;
        }
        isClick=true;
        tutorial.text = txtData;
        strid++;
    }

    public static void DcreaseEnemy(){
        Debug.Log("Decrease");
        enemys--;
        Debug.Log(enemys);
        if(enemys == 0){
            SceneManager.LoadScene("MainPage");
        }
    }
}
