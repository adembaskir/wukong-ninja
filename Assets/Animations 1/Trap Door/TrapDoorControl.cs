using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorControl : MonoBehaviour // Attached to Trap Door..
{
    public GameObject needle1, needle2;
    public float needle1Counter, needle2Counter, needle1Destroy, needle2Destroy,mainDestroy;
    public bool countForActive,countForDestroy,trapDestroyed = false;
    void Start()
    {
        //needle2.GetComponent<Animator>().enabled = false;
        //needle1.GetComponent<Animator>().enabled = false;
    }

    void Update()
    {
        if (countForActive)
        {
            needle2Counter -= Time.deltaTime;
            needle1Counter -= Time.deltaTime;
            ActivateTrap();

        }
        if (countForDestroy && !trapDestroyed)
        {
            if (!countForActive)
            {
                needle2Counter -= Time.deltaTime;
                needle1Counter -= Time.deltaTime;
                ActivateTrap();
            }

            Destroy(needle1, needle1Destroy);
            Destroy(needle2, needle2Destroy);

            trapDestroyed = true;
        }


    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Ninja")
        {
            countForActive = true;
        }
        if(collision.gameObject.tag == "Shuriken Protection")
        {
            countForDestroy = true;
            /*
            needle2Counter -= Time.deltaTime;
            needle1Destroy -= Time.deltaTime;
            needle1Counter -= Time.deltaTime;
            needle2Destroy -= Time.deltaTime;
            mainDestroy -= Time.deltaTime;

            if(needle2Counter <= 0)
            needle2.GetComponent<Animator>().enabled = true;
            if(needle1Counter <= 0)
            needle1.GetComponent<Animator>().enabled = true;

            if(needle1Destroy <= 0)
            Destroy(needle1);
            if(needle2Destroy <= 0)
            Destroy(needle2);

            if (mainDestroy <= 0)
                Destroy(gameObject);

            */
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Shuriken Protection")
        {
            Destroy(gameObject);
        }
    }

    public void ActivateTrap()
    {
        if (needle2Counter <= 0)
            needle2.GetComponent<Animator>().enabled = true;
        if (needle1Counter <= 0)
            needle1.GetComponent<Animator>().enabled = true;
    }

}
