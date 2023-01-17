using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    public static GenerateMap Instance;
    public int EnemyNum;
    public GameObject Enemy;
    public GameObject Agent;
    private GameObject[] spawnedEnemies;
    Queue<Target1> poolingObjectQueue = new Queue<Target1>();

    void Awake()
    {
        Instance = this;
        Initialize(EnemyNum);
        spawnedEnemies = new GameObject[EnemyNum];
    }

    public void Spawn()
    {
        //destroy existintg enemies
        foreach(GameObject obj in spawnedEnemies)
        {
            if(obj) Destroy(obj);
        }

        //spawn new enemies
        for(int i=0; i<EnemyNum; i++)
        {
            var obj = GetObject();
            Vector3 rndVec3 = new Vector3(Random.Range(-12, 12), transform.position.y, Random.Range(-12, 12));
            obj.transform.localPosition = rndVec3 + transform.position;
            spawnedEnemies[i] = obj.gameObject;
        }

        //set new enemies
        for(int i=0; i<EnemyNum; i++)
        {
            Agent.GetComponent<KnightAgent>().targets[i] = spawnedEnemies[i];
        }
    }

    private void Initialize(int EnemyNum)
    {
        for(int i = 0; i < EnemyNum; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
            
        }
    }

    private Target1 CreateNewObject()
    {
        var newObj = Instantiate(Enemy).GetComponent<Target1>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public static Target1 GetObject()
    {
        if(Instance.poolingObjectQueue.Count > 0)
        {
            var obj = Instance.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }

    public static void ReturnObject(Target1 obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(obj);
    }
}
