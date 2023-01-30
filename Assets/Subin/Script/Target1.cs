using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target1 : MonoBehaviour
{
    public GameObject Generator;
    void Start()
    {

    }

    void Update()
    {
        
    }


    void OnTriggerStay(Collider coll)
    {
        if(coll.gameObject.tag=="Weapon"){
            //find coll.gameObject's parent and get reward
            KnightAgent ka = coll.gameObject.transform.parent.gameObject.GetComponent<KnightAgent>();
            // ka.killed = this.gameObject;
            ka.killEnemy();
            //this object Active false
            this.gameObject.SetActive(false);
        }
    }

    void OnCollisionStay(Collision coll) { 
        // Debug.Log(coll.gameObject.name);

        if (coll.gameObject.CompareTag("Wall") || coll.gameObject.CompareTag("Trap")){
            this.transform.localPosition = Generator.transform.localPosition + new Vector3(Random.Range(-12, 12), 0.5f, Random.Range(-12, 12));
        }
            // Vector3 rndVec3 = new Vector3(Random.Range(-12, 12), transform.position.y, Random.Range(-12, 12));
            // obj.transform.localPosition = rndVec3 + transform.position;
        // this.transform.position = new Vector3(Random.Range(-10, 10), transform.position.y, Random.Range(-10, 10));
    }
}
