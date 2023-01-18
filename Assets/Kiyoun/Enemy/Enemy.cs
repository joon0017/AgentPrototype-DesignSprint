using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public ParticleSystem p;
    public Animator a;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerStay(Collider c){
        if(c.gameObject.tag=="Weapon"){
            p.Play();
            a.SetTrigger("isDead");
        }   
    }
    void Dead(){
        GetComponent<Rigidbody>().useGravity=true;
        GetComponent<BoxCollider>().enabled=false;
        Destroy(gameObject,2f);
    }
}
