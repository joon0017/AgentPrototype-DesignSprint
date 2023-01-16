using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed;
    float hAxis;
    float vAxis;

    Vector3 moveVec;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        hAxis = Input.GetAxisRaw("Horizontal");    
        vAxis = Input.GetAxisRaw("Vertical");
        moveVec = new Vector3(hAxis,0,vAxis).normalized;
        transform.position += moveVec * speed * Time.deltaTime;
        transform.LookAt(transform.position + moveVec);
        //anim.SetBool("Moving",moveVec != Vector3.zero);
    }
}
