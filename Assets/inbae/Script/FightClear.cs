using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightClear : MonoBehaviour
{
    void Start()
    {
        Invoke("CheckEnemy", 2.5f);
    }

    void CheckEnemy()
    {
        GameObject monster = GameObject.Find("Goblin(Clone)");
        if(monster == null)
        {
            SceneManager.LoadScene("FightFinish");
        }

        Invoke("CheckEnemy", 2.5f);
    }
}
