using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapControl : MonoBehaviour //Attached to Trap Wall..
{
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Shuriken Protection")
        {
            //Debug.Log("Touched");
            Destroy(this.gameObject);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shuriken Protection")
        {
            //Debug.Log("Touched");
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
