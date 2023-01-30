using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : CharacterController
{
    public GameObject spell;
    public Vector3 spellSpawn;
    public float rayDistance = 5f;
    public float raySpread = 10f;
    Ray ray1;
    Ray ray2;
    Ray ray3;
    Vector3 rayDirection;
    Quaternion spreadRotation;
    Vector3 spreadDirection;
    Vector3 origin;
    float temp = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Game();        
    }
    public override void AttackStart(){
        canMove=false;
        canAttack=false;
        RaycastHit hit;
        //ray는 항상 캐릭터의 앞을 바라본다
        rayDirection = transform.forward;
        //바닥에서 1 떨어져야 적을 확인 가능
        origin = transform.position + new Vector3(0, 1, 0);
        //직선 앞으로 쏘아지는 ray
        ray1 = new Ray(origin, rayDirection);
        Debug.DrawRay(ray1.origin, ray1.direction * rayDistance, Color.red);

        //ray의 각도를 계산하는 식
        spreadRotation = Quaternion.AngleAxis(-raySpread, transform.up);
        spreadDirection = spreadRotation * rayDirection;
        //2번째 ray (왼쪽 10도로 쏘아지는 ray)
        ray2 = new Ray(origin, spreadDirection);
        Debug.DrawRay(ray2.origin, ray2.direction * rayDistance, Color.green);

        //ray의 각도를 계산하는 식
        spreadRotation = Quaternion.AngleAxis(raySpread, transform.up);
        spreadDirection = spreadRotation * rayDirection;
        //3번째 ray (오른쪽 10도로 쏘아지는 ray)
        ray3 = new Ray(origin, spreadDirection);
        Debug.DrawRay(ray3.origin, ray3.direction * rayDistance, Color.blue);
        //ray1||2||3에 적이 있을 경우, 공격 가능하고, 마법 소환 위치를 적이 있는 위치로 변경
        if(Physics.Raycast(ray1, out hit, rayDistance)||Physics.Raycast(ray2, out hit, rayDistance)||Physics.Raycast(ray3, out hit, rayDistance)){
            if(hit.collider.tag == "Target"){
                canAttack=true;
                spellSpawn = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y, hit.collider.transform.position.z);
                canMove=false;
                anim.SetTrigger("Attack");
            }   
        }
        //적을 못찾으면 공격 불가능
        //swap rayDistance and temp ->  공격을 하는 중에는 다시 공격을 가능하게 하면 안되기 때문에 ray의 길이를 0으로 함으로써 적을 찾지 못하게 함
        (rayDistance, temp) = (temp, rayDistance);
    }
    public override void Damage(){
        //공격 소환
        Instantiate(spell, spellSpawn, transform.rotation);
        
    }
    public override void AttackEnd(){
        canMove = true;
        canAttack = true;
        //swap rayDistance and temp
        (rayDistance, temp) = (temp, rayDistance);
    }
}
