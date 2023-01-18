using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightClear : MonoBehaviour
{
    void FixedUpdate()
    {
        GameObject monster = GameObject.Find("Goblin(Clone)");
        if(monster == null)
        {
            SceneManager.LoadScene("MainPage");
        }
        
    }
}
