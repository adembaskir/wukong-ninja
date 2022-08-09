using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNinjaControl : MonoBehaviour //Attached to Enemy Ninjas and their Ragdoll pieces..
{
    //private bool enemySlapped = false;
    public BoxCollider mainCollider;
    public GameObject thisGuysRig;
    public Animator thisGuysAnimator;
    public float slowMotionTime;
    public float vanishCoolDown = 1.5f;
    public AudioHolderControl audioHolderControlScript;


    void Start()
    {
        audioHolderControlScript = GameObject.Find("Audio Holder").GetComponent<AudioHolderControl>();
        thisGuysAnimator = GetComponent<Animator>();
        GetRagDollBits();
        RagDollModeOff();
        //RagDollModeOn();
        //rig.GetComponent<Rigidbody>().freezeRotation = true;
        //rig.GetComponent<Rigidbody>().isKinematic = true;
        //head.GetComponent<Rigidbody>().freezeRotation = true;
    }


    void Update()
    {

    }

    public IEnumerator waitFor()
    {
        yield return new WaitForSecondsRealtime(slowMotionTime);
        Time.timeScale = 1;
    }

    public void RagDollModeOn()
    {

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


        thisGuysAnimator.enabled = true;
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
        if (collision.gameObject.tag == "Ninja" || collision.gameObject.tag == "Shuriken Protection" || collision.gameObject.tag =="Trap")
        {
            //dead = true;
            EnemyNinjaDown();
        }
        if(collision.gameObject.tag == "Ninja")
        {
            //enemySlapped = true;
            audioHolderControlScript.audioHolder.PlayOneShot(audioHolderControlScript.ninjaSlap, 0.8f);
        }
        if(collision.gameObject.tag == "EnemyNinja")
        {
            Physics.IgnoreCollision(GetComponent<Collider>(),collision.gameObject.GetComponent<Collider>(),true);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Shuriken Protection")
        {
            EnemyNinjaDown();
        }
    }
    public void EnemyNinjaDown()
    {
        RagDollModeOn();
        Time.timeScale = slowMotionTime;
        StartCoroutine(waitFor());
        Destroy(this.gameObject,vanishCoolDown);
        //Destroy(this.transform.parent.gameObject,banishCoolDown);
    }
}
