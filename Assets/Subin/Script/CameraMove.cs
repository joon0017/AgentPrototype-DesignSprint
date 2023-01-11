using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private float xPosition, yPosition, xMove, yMove; 
    public float moveSpeed = 10.0f;

    void Update()
    {
        //camera move code with WASD
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * Time.deltaTime * 10);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * Time.deltaTime * 10);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * 10);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * 10);
        }

        //camera rotae code with mouse
        if (Input.GetMouseButton(1))
        {
            xPosition = Input.GetAxis("Mouse X");
            yPosition = Input.GetAxis("Mouse Y");
            xMove += xPosition;
            yMove += yPosition;
            transform.eulerAngles = new Vector3(-yMove, xMove, 0);
        }

        //camera zoom code with mouse wheel
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 100);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            transform.Translate(Vector3.back * Time.deltaTime * 100);
        }
    }
}
