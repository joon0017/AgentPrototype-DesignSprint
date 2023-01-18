using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int speed;
    public int fly;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Shoot(){
        GetComponent<Rigidbody>().AddForce(transform.forward*speed);
    }
    public void Duration(){
        Destroy(gameObject, fly);
    }
}
