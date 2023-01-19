using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{

    Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = GameObject.Find("Archer").GetComponent<Archer>().targetPosition;
        Shoot();
    }

    // Update is called once per frame
    void Update()
    {
        Duration();
    }
    public override void Shoot(){
        Vector3 direction = (targetPosition - transform.position).normalized;
        GetComponent<Rigidbody>().AddForce(direction * speed);
    }
    //if arrow hits something, destroy self
    void OnTriggerEnter(Collider c){
        if(c.gameObject.tag!="Player")
            Destroy(gameObject);
    }
}
