using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int speed;
    public int fly;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward*speed);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, fly);
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag!="Player"){
            Destroy(gameObject);
        }
    }
}
