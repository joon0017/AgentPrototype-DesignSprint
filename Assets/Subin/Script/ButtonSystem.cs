using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonSystem : MonoBehaviour
{
    public TMP_Text countText;
    private int count;
    private bool isLearn;

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
        count = count + 1;
        UpdateCountText();
        Debug.Log("hi" + count);
    }

    public void Decrease()
    {
        count = count - 1;
        UpdateCountText();
    }

    public void UpdateCountText()
    {
        countText.text = count.ToString();
        Debug.Log(countText.text + " " + count.ToString());
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

