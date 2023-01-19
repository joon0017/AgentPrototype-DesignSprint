using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    [SerializeField]GameObject agent;
    [SerializeField]GameObject spawner;
    void OnTriggerStay(Collider coll)
    {
        if(coll.gameObject.tag=="Weapon"){
            agent.GetComponent<Gilbert_Agent_Prototype1>().KillEnemy(this.gameObject);
        }
    }

    void OnCollisionStay(Collision coll) { 
        if (coll.gameObject.CompareTag("Wall") || coll.gameObject.CompareTag("Trap")){
            this.transform.localPosition = spawner.transform.localPosition +  new Vector3(Random.Range(-12, 12), 0.5f, Random.Range(-12, 12));
            return;
        }}

}
