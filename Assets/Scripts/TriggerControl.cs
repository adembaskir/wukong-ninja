using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerControl : MonoBehaviour //Not Attached..
{
    //public UnityEvent onEnter;
    public Animator detector;
    public EnemyThrower enemyThrowerScript;

    public void Start()
    {
        enemyThrowerScript = GameObject.Find("Enemy Ninja Thrower").GetComponent<EnemyThrower>();
        //enemyThrowerScript = GameObject.Find("Control Scripts").GetComponent<EnemyThrower>();
        //detector.enabled = false;
    }
    public void OnCollisionEnter(Collision collision)
    {
        /*
        if (collision.gameObject.tag == "Trap")
        onEnter?.Invoke();
        */
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ninja")
        {
            enemyThrowerScript.detect = true;
            //enemyThrowerScript.thisGuysAnimator.enabled = true;
        }
    }
}
