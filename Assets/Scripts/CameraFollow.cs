using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour //Attached to Main Camera..
{
    public Transform target;
    public float speed = 15f;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,target.position,speed*Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation,target.rotation,speed*Time.deltaTime);
    }
}
