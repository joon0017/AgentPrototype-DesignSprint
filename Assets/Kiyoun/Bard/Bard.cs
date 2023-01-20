using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bard : CharacterController
{
    public GameObject spell;
    public float rayDistance;
    public float raySpread;
    Ray ray1;
    Ray ray2;
    Ray ray3;
    Vector3 rayDirection;
    Quaternion spreadRotation;
    Vector3 spreadDirection;
    Vector3 origin;
    float temp = 0;
    public RaycastHit hit;
    //public Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Game();
        rayDirection = transform.forward;
        origin = transform.position + new Vector3(0, 1, 0);
        ray1 = new Ray(origin, rayDirection);
        Debug.DrawRay(ray1.origin, ray1.direction * rayDistance, Color.red);
        spreadRotation = Quaternion.AngleAxis(-raySpread, transform.up);
        spreadDirection = spreadRotation * rayDirection;
        ray2 = new Ray(origin, spreadDirection);
        Debug.DrawRay(ray2.origin, ray2.direction * rayDistance, Color.green);
        spreadRotation = Quaternion.AngleAxis(raySpread, transform.up);
        spreadDirection = spreadRotation * rayDirection;
        ray3 = new Ray(origin, spreadDirection);
        Debug.DrawRay(ray3.origin, ray3.direction * rayDistance, Color.blue);
        if(Physics.Raycast(ray1, out hit, rayDistance)||Physics.Raycast(ray2, out hit, rayDistance)||Physics.Raycast(ray3, out hit, rayDistance)){
            if(hit.collider.tag == "Target"){
                //targetPosition= new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y, hit.collider.transform.position.z);
                canMove=false;
                transform.LookAt(hit.collider.transform);
                anim.SetTrigger("Attack");
            }   
        }
    }
    public override void AttackStart(){
        canAttack=false;
        (rayDistance, temp) = (temp, rayDistance);
    }
    public override void Damage(){
        Instantiate(spell,attackArea.transform.position,attackArea.transform.rotation);
    }
    public override void AttackEnd(){
        canMove=true;
        canAttack=true;
        (rayDistance, temp) = (temp, rayDistance);
    }
}
