using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrower : MonoBehaviour //Attached to Enemy Ninja Thrower..
{
    //private bool enemySlapped = false;
    public Transform target;
    public BoxCollider mainCollider;
    public GameObject thisGuysRig,KillerEnemyShuriken;
    public GameObject head;
    public Transform throwPoint;
    public Animator thisGuysAnimator;
    public float time,throwShurikenCoolDown;
    public bool dead,detect = false;
    public AudioHolderControl audioHolderControlScript;


    Vector3 triggerPosition = new Vector3(5, 5, 5);
    void Start()
    {
        audioHolderControlScript = GameObject.Find("Audio Holder").GetComponent<AudioHolderControl>();
        thisGuysAnimator = GetComponent<Animator>();
        GetRagDollBits();
        RagDollModeOff();

        throwShurikenCoolDown = 1.4f;
    }

    void Update()
    {
        if (!dead && detect)
        {
            thisGuysAnimator.SetBool("isThrowingShuriken",true);
            //transform.LookAt(target);
            Vector3 direction = (target.position) - transform.position;
            direction.y = 0f;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;

            ThrowShuriken();
        }
    }

    public IEnumerator waitFor()
    {
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1;
    }

    public void RagDollModeOn()
    {
        dead = true;
        thisGuysAnimator.enabled = false;
        foreach (Collider col in ragDollColliders)
        {
            col.enabled = true;
        }

        foreach (Rigidbody rigid in limbsRigidBodies)
        {
            rigid.isKinematic = false;
        }


        mainCollider.enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void RagDollModeOff()
    {
        foreach (Collider col in ragDollColliders)
        {
            col.enabled = false;
        }

        foreach (Rigidbody rigid in limbsRigidBodies)
        {
            rigid.isKinematic = true;
        }


        mainCollider.enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;


    }

    Collider[] ragDollColliders;
    Rigidbody[] limbsRigidBodies;
    void GetRagDollBits()
    {
        ragDollColliders = thisGuysRig.GetComponentsInChildren<Collider>();
        limbsRigidBodies = thisGuysAnimator.GetComponentsInChildren<Rigidbody>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ninja" || collision.gameObject.tag == "Shuriken Protection")
        {
            //detect = true;
            RagDollModeOn();
            Time.timeScale = time;
            StartCoroutine(waitFor());
            Destroy(this.gameObject, 0.5f);
        }
        if(collision.gameObject.tag == "Ninja")
        {
            //enemySlapped = true;
            audioHolderControlScript.audioHolder.PlayOneShot(audioHolderControlScript.ninjaSlap, 0.3f);
        }
        if (collision.gameObject.tag == "EnemyNinja")
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>(),true);
        }
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ninja")
        {
            detect = true;
        }
    }

    public void ThrowShuriken()
    {
        throwShurikenCoolDown -= Time.deltaTime;
        if (throwShurikenCoolDown <= 0)
        {
            GameObject go = Instantiate(KillerEnemyShuriken, throwPoint.position, throwPoint.rotation) as GameObject;
            go.GetComponent<Rigidbody>().velocity = throwPoint.transform.up * 20f;
            Destroy(go.gameObject, 1f);
            throwShurikenCoolDown = 2.97f;

        }
    }

}
