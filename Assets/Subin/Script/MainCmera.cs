using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCmera : MonoBehaviour
{
    //follow target
    public Transform target;
    //camera offset
    public Vector3 offset;
    //camera rotation
    public Vector3 rotation;
    //camera speed
    public float speed;
    //camera distance
    public float distance;
    //camera height
    public float height;
    //camera angle
    public float angle;

    // Start is called before the first frame update
    void Start()
    {
        //set camera position
        transform.position = target.position + offset;
        //set camera rotation
        transform.rotation = Quaternion.Euler(rotation);
    }

    // Update is called once per frame
    void Update()
    {
        //set camera position
        transform.position = target.position + offset;
        //set camera rotation
        transform.rotation = Quaternion.Euler(rotation);
        //set camera distance
        transform.position = transform.position - (transform.forward * distance);
        //set camera height
        transform.position = new Vector3(transform.position.x, height, transform.position.z);
        //set camera angle
        transform.rotation = Quaternion.Euler(angle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    //set camera follow target
    public void SetTarget(Transform _target)
    {
        target = _target;
    }
}
