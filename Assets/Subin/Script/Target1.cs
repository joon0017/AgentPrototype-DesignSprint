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


    void OnTriggerStay(Collider coll)
    {
        if(coll.gameObject.tag=="Weapon"){
            OnDamage();
            //find coll.gameObject's parent and get reward
            //coll.gameObject.transform.parent.gameObject.GetComponent<KnightAgent>().SetRWD(0.1f);
        }
    }

    void OnCollisionStay(Collision coll) { 
        if (coll.gameObject.CompareTag("Wall") || coll.gameObject.CompareTag("Trap")){
            this.transform.position = new Vector3(Random.Range(-10, 10), transform.position.y, Random.Range(-10, 10));
            return;
        }
        // this.transform.position = new Vector3(Random.Range(-10, 10), transform.position.y, Random.Range(-10, 10));
    }

    public void OnDamage()
    {
        Debug.Log("OnDamage");
        GameManager.DcreaseEnemy();
        GenerateMap.ReturnObject(this);
    }
}
