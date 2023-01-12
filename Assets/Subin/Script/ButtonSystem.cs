using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonSystem : MonoBehaviour
{
    public TMP_Text countText;
    public TMP_Text statsText;
    private int count;
    private bool isLearn;
    public static int stats = 10;

    private void FixedStart()
    {
        count = 0;
        UpdateCountText();
    }
    public void setIsLearn(bool tf){
        this.isLearn = tf;
    }

    public void Increase()
    {
        if(stats <= 0){
            return;
        }
        if(stats <= 10)
        {
            stats = stats - 1;
            count = count + 1;
        UpdateCountText();
        }
        else UpdateCountText();

    }

    public void Decrease()
    {
        if(count <= 0){
            return;
        }
        if(stats <= 10)
        {
            stats = stats + 1;
            count = count - 1;
        UpdateCountText();
        }
        else UpdateCountText();
    }

    public void UpdateCountText()
    {
        countText.text = count.ToString();
        Debug.Log(countText.text + " " + count.ToString());
        statsText.text = stats.ToString();
        Debug.Log(statsText.text + " " + stats.ToString());
    }

    public void MoveFight()
    {
        if(isLearn == false){
            SceneManager.LoadScene("FailFight");
        }

        if(isLearn == true){
            SceneManager.LoadScene("TrainFight");
        }
    }
}

