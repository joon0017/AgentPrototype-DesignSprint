using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Path : MonoBehaviour
{
   // [SerializeField] private 
    Transform target;
    Animator animator;
    private NavMeshAgent n;
    int i;
    bool canMove=true;
    // Start is called before the first frame update
    void Start()
    {  
        n=GetComponent<NavMeshAgent>();
        animator=GetComponent<Animator>();
        target=GameObject.FindWithTag("Target").transform;
    }

    // Update is called once per frame
    void Update()
    {
        i=GameObject.FindGameObjectsWithTag("Target").Length;
        if(i==0) canMove=false;
        if(canMove){
            target=GameObject.FindWithTag("Target").transform;
            n.destination = new Vector3(target.position.x,target.position.y,target.position.z);
            animator.SetBool("isRun",canMove);
            if(Vector3.Distance(target.position,transform.position)<2){
            animator.SetBool("isAttack",true);
            }
            else animator.SetBool("isAttack",false);
        }
        else animator.SetBool("isRun",canMove);
        //if the distance between target and the character is less than 2
        
    }
}
