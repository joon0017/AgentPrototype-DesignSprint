using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject[] charPrefabs;
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;

    void Start()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(gameManager.selectedCharacter[0] != null)
        {
            SpawnChar(gameManager.selectedCharacter[0], 1);
        }
        if(gameManager.selectedCharacter[1] != null)
        {
            SpawnChar(gameManager.selectedCharacter[1], 2);
        }
        if(gameManager.selectedCharacter[2] != null)
        {
            SpawnChar(gameManager.selectedCharacter[2], 3);
        }
    }

    void SpawnChar(string name, int position)
    {
        Vector3 new_position;
        if(position == 1)
        {
            new_position = new Vector3(-12.2f, 0.5f, -13.36f);
            if(name == "Gilbert")
            {
                SpawnInst(spawn1, new_position, charPrefabs[0]);
            }
            else if(name == "Gonzales")
            {
                SpawnInst(spawn1, new_position, charPrefabs[1]);
            }
            else if(name == "Cecilia")
            {
                SpawnInst(spawn1, new_position, charPrefabs[2]);
            }
            else if(name == "Patrick")
            {
                SpawnInst(spawn1, new_position, charPrefabs[3]);
            }
            else if(name == "Lily")
            {
                SpawnInst(spawn1, new_position, charPrefabs[4]);
            }
        }
        else if(position == 2)
        {
            new_position = new Vector3(-9.2f, 0.5f, -13.36f);
            if(name == "Gilbert")
            {
                SpawnInst(spawn2, new_position, charPrefabs[0]);
            }
            else if(name == "Gonzales")
            {
                SpawnInst(spawn2, new_position, charPrefabs[1]);
            }
            else if(name == "Cecilia")
            {
                SpawnInst(spawn2, new_position, charPrefabs[2]);
            }
            else if(name == "Patrick")
            {
                SpawnInst(spawn2, new_position, charPrefabs[3]);
            }
            else if(name == "Lily")
            {
                SpawnInst(spawn2, new_position, charPrefabs[4]);
            }
        }
        else if(position == 3)
        {
            new_position = new Vector3(-7.2f, 0.5f, -13.36f);
            if(name == "Gilbert")
            {
                SpawnInst(spawn3, new_position, charPrefabs[0]);
            }
            else if(name == "Gonzales")
            {
                SpawnInst(spawn3, new_position, charPrefabs[1]);
            }
            else if(name == "Cecilia")
            {
                SpawnInst(spawn3, new_position, charPrefabs[2]);
            }
            else if(name == "Patrick")
            {
                SpawnInst(spawn3, new_position, charPrefabs[3]);
            }
            else if(name == "Lily")
            {
                SpawnInst(spawn3, new_position, charPrefabs[4]);
            }
        }
    }

    void SpawnInst(GameObject spawnObject, Vector3 position, GameObject prefab)
    {
        spawnObject = Instantiate (prefab) as GameObject;
        spawnObject.name = "Player";
        spawnObject.transform.position = position;
    }
}
