using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward*1000);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 5);
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag=="Target"){
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if(other.gameObject.tag!="Player"){
            Destroy(gameObject);
        }
    }
}
