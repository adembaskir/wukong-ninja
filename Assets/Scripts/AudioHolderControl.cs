using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioHolderControl : MonoBehaviour
{
    //public static bool musicKeeper;
    //public GameObject backGroundMusic;
    public AudioSource audioHolder;
    public AudioClip ninjaSlap, fallingSound, backGroundMusic, painfullSound;
    // Start is called before the first frame update
    void Start()
    {
        //audioHolder.PlayOneShot(backGroundMusic, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
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
