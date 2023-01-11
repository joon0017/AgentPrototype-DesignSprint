using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UsingInstantiate : MonoBehaviour
{
    public GameObject light;
    public Transform random;
    
    
    void FixedUpdate ()
    {
        random.position = new Vector3(Random.Range(-960f, 960f), Random.Range(-540f, 540f), 0);
        Instantiate(light, random.position, random.rotation);
    }
}