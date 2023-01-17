using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target1 : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        
    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag=="AttackArea")
            GenerateMap.ReturnObject(this);
    }

    void OnCollisionStay(Collision coll) { 
        // Debug.Log(coll.gameObject.name);

        if (coll.gameObject.CompareTag("Wall") || coll.gameObject.CompareTag("Trap")){
            this.transform.position = new Vector3(Random.Range(-10, 10), transform.position.y, Random.Range(-10, 10));
            return;
        }
        // this.transform.position = new Vector3(Random.Range(-10, 10), transform.position.y, Random.Range(-10, 10));
    }

    public void OnDamage()
    {
        GenerateMap.ReturnObject(this);
    }
}
