using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move2Select : MonoBehaviour
{
    public void Move2Main()
    {
        SceneManager.LoadScene("SelectChar");
    }
}
