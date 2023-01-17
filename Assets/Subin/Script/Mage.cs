using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : CharacterController1
{
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
        attackArea.enabled=true;
        var colliders = Physics.OverlapBox(attackArea.transform.position, attackArea.size/2, attackArea.transform.rotation, LayerMask.GetMask("Enemy"));
        foreach(var col in colliders)
        {
            var enemy = col.GetComponent<Target1>();
            if(enemy != null)
            {
                enemy.OnDamage();
            }
        }
    }
}
