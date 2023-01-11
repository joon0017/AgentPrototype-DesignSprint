using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Path : MonoBehaviour
{
   // [SerializeField] private 
    Transform target;
    [SerializeField] private GameObject attackArea;
    private NavMeshAgent n;
    bool canMove;
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
            Debug.Log(target.gameObject.tag);
            canMove=true;
        }
        else if(target==null) {
            canMove=false;
        }
        if(canMove){
            n.destination = new Vector3(target.position.x,target.position.y,target.position.z);
        }
    }
}
