using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrainSceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeScene());
        GameObject.FindGameObjectWithTag("NoDestroy").GetComponent<GameManager>().isLearn = true;
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene("LearningFinish");
    }
}
