using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed;
    public float hAxis;
    public float vAxis;
    public bool canMove = true;
    public bool canAttack = true;

    public BoxCollider attackArea;
    public Vector3 moveVec;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Game(){
        if(canMove){
            hAxis = Input.GetAxisRaw("Horizontal");    
            vAxis = Input.GetAxisRaw("Vertical");
            moveVec = new Vector3(hAxis,0,vAxis).normalized;
            transform.position += moveVec * speed * Time.deltaTime;
            transform.LookAt(transform.position + moveVec);
            anim.SetBool("Moving",moveVec != Vector3.zero);
        }
        if(Input.GetKeyDown(KeyCode.Space)&&canAttack)
        {
            anim.SetTrigger("Attack");
        }
    }
    public void AttackStart(){
        canMove=false;
        canAttack=false;
    }
    public void Damage(){
        attackArea.enabled=true;
    }
    public void AttackEnd(){
        canMove = true;
        canAttack = true;
        attackArea.enabled=false;
    }
    public void onCollisionExit(Collision c){
        moveVec = Vector3.zero;
    }
}
