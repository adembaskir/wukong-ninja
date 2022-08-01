using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerShurikenControl : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ninja")
        {
            Destroy(gameObject);
        }
    }
}
