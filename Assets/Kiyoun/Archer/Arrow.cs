using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{

    // Start is called before the first frame update
    void Start()
    {
        Shoot();
    }

    // Update is called once per frame
    void Update()
    {
        Duration();
    }
    //if arrow hits something, destroy self
    void OnTriggerEnter(Collider c){
        if(c.gameObject.tag!="Player")
            Destroy(gameObject);
    }
}
