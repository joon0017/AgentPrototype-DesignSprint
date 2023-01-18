using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bard : CharacterController
{
    public GameObject spell;
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
    }
    public override void Damage(){
        Instantiate(spell,attackArea.transform.position,attackArea.transform.rotation);
    }
    public override void AttackEnd(){
        canMove=true;
        canAttack=true;
    }
}
