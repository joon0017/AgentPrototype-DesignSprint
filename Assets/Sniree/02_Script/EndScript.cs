using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{
        // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeScene());
        GameObject.FindGameObjectWithTag("NoDestroy").GetComponent<GameManager>().isLearn = false;
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(18f);
        SceneManager.LoadScene("GameStart");
    }
}
