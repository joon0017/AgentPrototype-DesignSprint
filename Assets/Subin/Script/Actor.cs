using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public int health;
    public float MoveSpeed;
    public float AttackDamge;
    Animator anim;
    
    void Start()
    {
        health = 100;
        MoveSpeed = 10f;
        AttackDamge = 10f;
        anim = GetComponent<Animator>(); 
    }

    void Update()
    {
        Move();
        //Attack();
    }

    //Player Move with WASD
    public void Move(){
        if(Input.GetKey(KeyCode.W)){
            transform.Translate(0, 0, MoveSpeed * Time.deltaTime);
            anim.SetBool("IsRun", true);
        }
        if(Input.GetKey(KeyCode.S)){
            transform.Translate(0, 0, -MoveSpeed * Time.deltaTime);
            anim.SetBool("IsRun", true);
        }
        if(Input.GetKey(KeyCode.A)){
            transform.Translate(-MoveSpeed * Time.deltaTime, 0, 0);
            anim.SetBool("IsRun", true);
        }
        if(Input.GetKey(KeyCode.D)){
            transform.Translate(MoveSpeed * Time.deltaTime, 0, 0);
            anim.SetBool("IsRun", true);
        }

        anim.SetBool("IsRun", false);
    }

    public void Attack(){
        if(Input.GetMouseButtonDown(0)){
            Debug.Log("Attack");
        }
    }
}
