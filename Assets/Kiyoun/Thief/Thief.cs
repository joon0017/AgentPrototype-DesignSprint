using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : CharacterController
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Game();
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Target"){
            canMove=false;
            transform.LookAt(other.transform.position);
            anim.SetTrigger("Attack");
        }
    }
}
