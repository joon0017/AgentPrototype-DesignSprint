using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAgent : MonoBehaviour
{
    public float eulerYAngle;
    public bool turnRightOn = true;

    void Start()
    {
        Invoke("TurnLeft", 4);
        if (gameObject.name == "AgentCube_Blue")
            eulerYAngle = 0;
        if (gameObject.name == "AgentCube_Purple")
            eulerYAngle = 55;
    }

    void FixedUpdate()
    {
        if (turnRightOn)
        {
            eulerYAngle += 1;
            transform.eulerAngles = new Vector3(0, eulerYAngle, 0);
        }

        else if (!turnRightOn)
        {
            eulerYAngle -= 1;
            transform.eulerAngles = new Vector3(0, eulerYAngle, 0);
        }
        
    }

    void TurnLeft()
    {
        turnRightOn = false;
        Invoke("TurnRight", 8);
    }

    void TurnRight()
    {
        turnRightOn = true;
        Invoke("TurnLeft", 8);
    }
}
