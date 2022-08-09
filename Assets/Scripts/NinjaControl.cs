using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaControl : MonoBehaviour //Attached to Ninja..
{
    public int energy;
    public float slowMotionPer, coolDownAfterJump,timer;
    public BoxCollider mainCollider; //Collider
    public GameObject thisGuysRig,ninja;   //RigidBody, Ninja game object
    public Animator thisGuysAnimator; //Animator
    public Rigidbody thisGuysRigidbody; //Rigidbody for force
    public bool gameOn,useShoot = true;
    public bool countDownThing = false;
    public ControlPoint controlPointScript;
    public AudioHolderControl audioHolderControlScript;

    //public float power = 10f;
    //public float maxDrag = 5f;
    //float xRot, yRot = 0f;
    //float rotationSpeed = 5f;
    //public float shootPower = 30f; 

    Vector3 dragStartPos;
    Touch touch;

    public IEnumerator WaitFor()
    {
        yield return new WaitForSecondsRealtime(slowMotionPer);
        Time.timeScale = 1;
    }

    void Start()
    {
        controlPointScript = GameObject.Find("Control Point").GetComponent<ControlPoint>();
        audioHolderControlScript = GameObject.Find("Audio Holder").GetComponent<AudioHolderControl>();
        gameOn = true;
        useShoot = false;
        GetRagDollBits();
        RagDollModeOff();
        countDownThing = true;
    }


    void Update()
    {
        if (countDownThing)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                countDownThing = false;
                if(energy > 0)
                {
                    useShoot = true;
                }
                else if(energy <= 0 && !controlPointScript.gameWin)
                {
                    audioHolderControlScript.audioHolder.PlayOneShot(audioHolderControlScript.fallingSound, 0.3f);
                    useShoot = false;
                    thisGuysAnimator.SetBool("isFalled", true);
                    controlPointScript.gameLost = true;
                    //RagDollModeOn();
                }
                timer = coolDownAfterJump;
            }
        }


        
        //xRot = Input.GetAxis("Mouse X");
        //yRot = Input.GetAxis("Mouse Y");
        /*
        if (Input.touchCount > 0)
        {
            //xRot = Input.touches[0].deltaPosition.x;
            //yRot = Input.touches[0].deltaPosition.y;

            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                DragStart();
            }
            if (touch.phase == TouchPhase.Moved)
            {
                Dragging();
            }
            if (touch.phase == TouchPhase.Ended)
            {
                DragRelease();
            }
        }
        */
        

    }

    void DragStart()
    {
        /*
        dragStartPos = Camera.main.ScreenToWorldPoint(touch.position);
         dragStartPos.z = 0f;
        lr.positionCount = 1;
        lr.SetPosition(0, dragStartPos);
        */
        
    }
    void Dragging()
    {
        /*
        Vector3 draggingPos = Camera.main.ScreenToWorldPoint(touch.position);
        dragStartPos.z = 0f;
        lr.positionCount = 2;
        lr.SetPosition(1, draggingPos);
        */


    }
    void DragRelease()
    {
        /*
        lr.positionCount = 0;
        Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(touch.position);
        dragReleasePos.z = 0f;
        Vector3 force = dragStartPos - dragReleasePos;
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;

        //rb.AddForce(clampedForce, ForceMode.Impulse);
        rb.velocity = transform.forward * power;
        //rb.AddForce(new Vector3(clampedForce.x,clampedForce.y,clampedForce.z));
        */

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

    public Collider[] ragDollColliders;
    public Rigidbody[] limbsRigidBodies;
    public void GetRagDollBits()
    {
        ragDollColliders = thisGuysRig.GetComponentsInChildren<Collider>();
        limbsRigidBodies = thisGuysAnimator.GetComponentsInChildren<Rigidbody>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Trap" && !controlPointScript.shurikenPowerUp)
        {
            audioHolderControlScript.audioHolder.PlayOneShot(audioHolderControlScript.painfullSound, 1f);
            controlPointScript.gameLost = true;
            gameOn = false;
            RagDollModeOn();
            Time.timeScale = slowMotionPer;
            StartCoroutine(WaitFor());
        }
        else if(collision.gameObject.tag == "Trap" && controlPointScript.shurikenPowerUp)
        {
            controlPointScript.shurikenPowerUpTurns--;
            if (energy < 1)
            {
                energy = 1;
            }
        }
        if(collision.gameObject.tag == "Left Wall" && !useShoot)
        {
            thisGuysRigidbody.AddForce(new Vector3((controlPointScript.xWallPower), (controlPointScript.yWallPower), (controlPointScript.zWallPower)), ForceMode.Impulse);
        }
        if(collision.gameObject.tag == "Right Wall" && !useShoot)
        {
            thisGuysRigidbody.AddForce(new Vector3(-(controlPointScript.xWallPower), (controlPointScript.yWallPower), (controlPointScript.zWallPower)), ForceMode.Impulse);
        }
        if (collision.gameObject.tag == "Ground")
        {
            thisGuysRigidbody.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            countDownThing = true;
            thisGuysAnimator.SetBool("isLanded", true);
            thisGuysAnimator.SetBool("isJumped", false);

            if (timer < 0)
            {
                countDownThing = false;
                if (energy > 0)
                {
                    useShoot = true;

                }
                else if (energy <= 0)
                {
                    controlPointScript.gameLost = true;
                    useShoot = false;
                    thisGuysAnimator.SetBool("isFalled", true);
                }
            }
        }

        if(collision.gameObject.tag == "EnemyNinja")
        {
            //audioHolderControlScript.audioHolder.PlayOneShot(audioHolderControlScript.ninjaSlap, 0.3f);
            Handheld.Vibrate();
            if(energy < 2)
            energy = 1;
        }

        if (collision.gameObject.tag == "Finish")
        {
            thisGuysAnimator.SetBool("isLanded", true);
            countDownThing = false;
            controlPointScript.gameWin = true;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Trap" && !controlPointScript.shurikenPowerUp)
        {
            audioHolderControlScript.audioHolder.PlayOneShot(audioHolderControlScript.painfullSound, 0.8f);
            controlPointScript.gameLost = true;
            gameOn = false;
            RagDollModeOn();
            Time.timeScale = slowMotionPer;
            StartCoroutine(WaitFor());
        }
        if(other.gameObject.tag == "Fall Border")
        {
            audioHolderControlScript.audioHolder.PlayOneShot(audioHolderControlScript.painfullSound, 0.8f);
            controlPointScript.gameLost = true;
            gameOn = false;
            RagDollModeOn();
            Time.timeScale = slowMotionPer;
            StartCoroutine(WaitFor());
        }
        if (other.gameObject.tag == "Finish")
        {
            thisGuysAnimator.SetBool("isLanded", true);
            countDownThing = false;    
            controlPointScript.gameWin = true;
        }
        if(other.gameObject.tag == "Shuriken Power Up")
        {
            Destroy(other.gameObject);
            controlPointScript.shurikenPowerUp = true;
            controlPointScript.shurikenPowerUpTurns += 3;
        }
    }
}





/*
   if (collision.gameObject.tag.Equals("Trap"))
        {
            PhysicsControl(false);
            ColliderControl(true);
        }




    void PhysicsControl(bool situation)
    {
        Rigidbody[] rg = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody childrenPhysics in rg)
        {
            childrenPhysics.isKinematic = situation;
        } 
    }

    void ColliderControl(bool situation)
    {
        Collider[] CL = GetComponentsInChildren<Collider>();
        foreach (Collider childrenPhysics in CL)
        {
            childrenPhysics.enabled = situation;
        }
    }
 */