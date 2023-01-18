using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Projectile
{
    // Start is called before the first frame update
    void Start()
    {
        Shoot();
    }

    // Update is called once per frame
    void Update()
    {
        Duration();
    }
}
