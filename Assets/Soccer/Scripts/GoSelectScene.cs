using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoSelectScene : MonoBehaviour
{
    public void GoStageSelect()
    {
        SceneManager.LoadScene("StageSelect");
    }
}
