using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenProtectionControl : MonoBehaviour //Attached to Shuriken The Buff
{
    public Transform target;
    public float rotationSpeed;
    public float orbitDistance;
    public GameObject rightWall, leftWall, thisShuriken,mainNinja;
    public NinjaControl ninjaControlScript;
    public ControlPoint controlPointScript;
    // Start is called before the first frame update
    void Start()
    {
        ninjaControlScript = GameObject.Find("Main Ninja(Blue)").GetComponent<NinjaControl>();
        controlPointScript = GameObject.Find("Control Point").GetComponent<ControlPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        Orbit();
        //Vector3.Distance(target.position, transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Right Wall")
        {
            Physics.IgnoreCollision(thisShuriken.GetComponent<Collider>(), rightWall.GetComponent<Collider>(),true);
        }
        if (collision.gameObject.tag == "Left Wall")
        {
            Physics.IgnoreCollision(thisShuriken.GetComponent<Collider>(), leftWall.GetComponent<Collider>(),true);
        }
        if(collision.gameObject.tag == "Ninja")
        {
            Physics.IgnoreCollision(thisShuriken.GetComponent<Collider>(), mainNinja.GetComponent<Collider>(), true);
        }
        if(collision.gameObject.tag == "EnemyNinja" && ninjaControlScript.energy < 1)
        {
            ninjaControlScript.energy = 1;
        }
        if(collision.gameObject.tag == "Trap" && controlPointScript.shurikenPowerUp)
        {
            //controlPointScript.shurikenPowerUpTurns--;
        }
    }
    void Orbit()
    {
        if (target != null)
        {
            transform.position = target.position + (transform.position - target.position).normalized * orbitDistance;
            transform.RotateAround(target.position, new Vector3(0, 1, 0), rotationSpeed * Time.deltaTime); //Vector3.Up
        }
    }
}
