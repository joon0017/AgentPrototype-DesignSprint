using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanManager : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject[] agentPrefabs;

    public void Start()
    {
        gameManager = GameObject.Find("GameManager");

        if (gameManager.GetComponent<GameManager>().selectedCharacter[0] == "Balance_Agent")
        {
            GameObject agent = Instantiate(agentPrefabs[0], new Vector3(-3.19f, 0.5f, -1.2f), Quaternion.identity);
        }
        else if (gameManager.GetComponent<GameManager>().selectedCharacter[0] == "Striker_Agent")
        {
            GameObject agent = Instantiate(agentPrefabs[1], new Vector3(-3.19f, 0.5f, -1.2f), Quaternion.identity);
        }
        else if (gameManager.GetComponent<GameManager>().selectedCharacter[0] == "Goalkeeper_Agent")
        {
            GameObject agent = Instantiate(agentPrefabs[2], new Vector3(-3.19f, 0.5f, -1.2f), Quaternion.identity);
        }

        if (gameManager.GetComponent<GameManager>().selectedCharacter[1] == "Balance_Agent")
        {
            GameObject agent = Instantiate(agentPrefabs[0], new Vector3(-3.19f, 0.5f, 1.2f), Quaternion.identity);
        }
        else if (gameManager.GetComponent<GameManager>().selectedCharacter[1] == "Striker_Agent")
        {
            GameObject agent = Instantiate(agentPrefabs[1], new Vector3(-3.19f, 0.5f, 1.2f), Quaternion.identity);
        }
        else if (gameManager.GetComponent<GameManager>().selectedCharacter[1] == "Goalkeeper_Agent")
        {
            GameObject agent = Instantiate(agentPrefabs[2], new Vector3(-3.19f, 0.5f, 1.2f), Quaternion.identity);
        }
    }
}
