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

    private void Start()
    {
        count = 0;
        UpdateCountText();
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
        //Move to fight scene
        SceneManager.LoadScene("Fight");
    }
    public void MoveLearning()
    {
        //Move to LearningFinish scene
        SceneManager.LoadScene("LearningFinish");
    }
}

