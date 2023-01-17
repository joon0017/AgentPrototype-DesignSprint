using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : CharacterController
{
    public GameObject spell;
    public Vector3 spellSpawn;
    Ray ray1;
    Ray ray2;
    Ray ray3;
    Vector3 rayDirection;
    float rayDistance = 5f;
    float raySpread = 10f;
    Quaternion spreadRotation;
    Vector3 spreadDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spellSpawn=new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Game();
        RaycastHit hit;
        //Ray ray = new Ray(new Vector3(transform.position.x,transform.position.y+1f,transform.position.z), Vector3.forward);
        rayDirection = transform.forward;
        
        Vector3 origin = transform.position + new Vector3(0, 1, 0);
        // ray 1
        ray1 = new Ray(origin, rayDirection);
        Debug.DrawRay(ray1.origin, ray1.direction * rayDistance, Color.red);

        // ray 2
        spreadRotation = Quaternion.AngleAxis(-raySpread, transform.up);
        spreadDirection = spreadRotation * rayDirection;
        ray2 = new Ray(origin, spreadDirection);
        Debug.DrawRay(ray2.origin, ray2.direction * rayDistance, Color.green);

        // ray 3
        spreadRotation = Quaternion.AngleAxis(raySpread, transform.up);
        spreadDirection = spreadRotation * rayDirection;
        ray3 = new Ray(origin, spreadDirection);
        Debug.DrawRay(ray3.origin, ray3.direction * rayDistance, Color.blue);

        if(Physics.Raycast(ray1, out hit, 5)||Physics.Raycast(ray2, out hit, 5)||Physics.Raycast(ray3, out hit, 5)){
            if(hit.collider.tag == "Target"){
                canAttack=true;
                spellSpawn = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y+1f, hit.collider.transform.position.z);
            }
        }
        else canAttack=false;
    }
    public override void AttackStart(){
        canMove=false;
        canAttack=false;
        rayDistance=0;
    }
    public override void Damage(){
        if(spellSpawn != transform.position){
            Instantiate(spell, spellSpawn, transform.rotation);
        }
    }
    public override void AttackEnd(){
        canMove = true;
        canAttack = true;
        rayDistance=5f;
    }
}
