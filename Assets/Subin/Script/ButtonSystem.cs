using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ButtonSystem : MonoBehaviour
{
    public TMP_Text countText;
    private int count;
    private bool isState;

    private void Start()
    {
        count = 0;
        UpdateCountText();
        isState = true;
    }

    public void Increase()
    {
        count++;
        UpdateCountText();
    }

    public void Decrease()
    {
        count--;
        UpdateCountText();
    }

    public void IncreaseState()
    {
        isState = true;
    }

    public void DecreaseState()
    {
        isState = false;
    }

    public void UpdateCountText()
    {
        countText.text = count.ToString();
    }

    public void MoveFight()
    {
        //Move to fight scene
        SceneManager.LoadScene("Fight");
    }
    public void MoveLearning()
    {
        //Move to LearningFinish scene
        SceneManager.LoadScene("LearningFinish");
    }
}

