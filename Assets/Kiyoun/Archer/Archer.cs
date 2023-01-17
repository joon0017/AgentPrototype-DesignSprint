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
    void Damage(){
        //spawn arrow at attackArea
        Instantiate(arrow,attackArea.transform.position,attackArea.transform.rotation);
        Debug.Log("Fire");
    }
    void AttackEnd(){
        canMove = true;
        canAttack = true;
    }
}
