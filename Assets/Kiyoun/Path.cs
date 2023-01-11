using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Path : MonoBehaviour
{
   // [SerializeField] private 
    Transform target;
    private NavMeshAgent n;
    // Start is called before the first frame update
    void Start()
    {
        n=GetComponent<NavMeshAgent>();
        target=GameObject.FindWithTag("Target").transform;
    }

    // Update is called once per frame
    void Update()
    {
        target=GameObject.FindWithTag("Target").transform;
        if(target!=null){
            n.destination = target.position;
        }
    }
}
