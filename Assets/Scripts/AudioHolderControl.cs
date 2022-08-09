using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioHolderControl : MonoBehaviour
{
    static public bool backGroundLoopThing = true;
    private float loopTimeCounter;
    //public GameObject backGroundMusic;
    public AudioSource audioHolder;
    public AudioClip ninjaSlap, fallingSound, backGroundMusic, painfullSound;
    
    // Start is called before the first frame update
    void Start()
    {
        loopTimeCounter = backGroundMusic.length-1;
        audioHolder.PlayOneShot(backGroundMusic, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (loopTimeCounter > 0)
        {
            loopTimeCounter -= Time.deltaTime;
            if (loopTimeCounter <= 0)
            {
                audioHolder.PlayOneShot(backGroundMusic, 1f);
                loopTimeCounter = backGroundMusic.length-1;
            }

        }
    }
    void Awake()
    {

    }

    public void PlayMusic()
    {
        if (audioHolder.isPlaying)
            return;
        audioHolder.Play();
    }
    public void StopMusic()
    {
        audioHolder.Stop();
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == 0)
        {
            Destroy(backGroundMusic);
        }
    }
}
