using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : CharacterController
{
    public GameObject arrow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Game();
    }
    public override void Damage(){
        Instantiate(arrow,attackArea.transform.position,attackArea.transform.rotation);
    }
    public override void AttackEnd(){
        canMove = true;
        canAttack = true;
    }
}
