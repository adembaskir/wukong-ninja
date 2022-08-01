using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlPoint : MonoBehaviour // Attached to "Control Point" named Game Object.
{
    public TextMeshProUGUI timeCounterText,failureText,shurikenIconTurnText,startUpMessage;
    public GameObject pausePanel,energyIcon,shurikenIcon;


    [Header("Unity ayarlama kýsmý.")]
    private float xRot, yRot, timeCounter = 0f;
    public float xWallPower,yWallPower,zWallPower;
    public float rotationSpeed = 5f;
    public float shootPower = 30f;
    public float jumpPower = 6f;
    public Rigidbody ninja;
    public LineRenderer line;
    public NinjaControl ninjaControlScript;
    public AudioHolderControl audioHolderControlScript;
    public GameObject topHead, shurikenProtection;
    public int shurikenPowerUpTurns = 0;
    public bool shurikenPowerUp,gameWin,gameLost = false;

    // Start is called before the first frame update
    void Start()
    {  
        ninjaControlScript = GameObject.Find("Ninja").GetComponent<NinjaControl>();
        audioHolderControlScript = GameObject.Find("Audio Holder").GetComponent<AudioHolderControl>();
        ninjaControlScript.gameOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        PowerUpShurikenCheck();
        if (gameWin)
        {
            
            //audioHolderControlScript.audioHolder.PlayOneShot(audioHolderControlScript.fallingSound, 0.3f);
            ChapterComplited();
        }
        if (!gameWin && ninjaControlScript.gameOn && !gameLost)
        {
            timeCounter += Time.deltaTime;
            timeCounterText.text = (int)timeCounter + "";
        }
        if (gameLost)
        {
            failureText.text = "Tekrar Dene";
            Time.timeScale = 1;
            pausePanel.gameObject.SetActive(true);
        }

        if(ninjaControlScript.energy < 1)
        {
            energyIcon.gameObject.SetActive(false);
        }
        if(ninjaControlScript.energy > 0)
        {
            energyIcon.gameObject.SetActive(true);
        }
        
        //ninjaControlScript.thisGuysRig.GetComponent<Rigidbody>().useGravity = false;
        transform.position = ninja.position;
        if (!gameLost && !gameWin)
        {
            if (ninjaControlScript.useShoot)
            {
                if (Input.GetMouseButton(0))
                {
                    xRot += Input.GetAxis("Mouse X") * rotationSpeed;
                    yRot += Input.GetAxis("Mouse Y") * rotationSpeed;
                    if (yRot < -35f)
                    {
                        yRot = -35f;
                    }
                    //transform.rotation = Quaternion.Euler(0f, xRot, 0f);
                    ninjaControlScript.ninja.transform.rotation = Quaternion.Euler(0f, xRot, 0f);
                    //head.transform.rotation = Quaternion.Euler(0f, xRot, 0f);
                    line.gameObject.SetActive(true);
                    line.SetPosition(0, topHead.transform.position);
                    line.SetPosition(1, topHead.transform.position + topHead.transform.forward * 4f);
                    //line.transform.rotation = Quaternion.Euler(0f,xRot,0f);

                    startUpMessage.text = "";
                }
                if (Input.GetMouseButtonUp(0))
                {
                    /*
                    ninja.velocity = rig.transform.forward * shootPower;
                    ninja.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
                    */
                    ninjaControlScript.useShoot = false;
                    ninjaControlScript.energy--;
                    ninjaControlScript.thisGuysAnimator.SetBool("isLanded", false);
                    ninjaControlScript.thisGuysAnimator.SetBool("isJumped", true);
                    ninjaControlScript.thisGuysRigidbody.velocity = topHead.transform.forward * shootPower;
                    ninjaControlScript.thisGuysRigidbody.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);


                    line.gameObject.SetActive(false);
                    //enemyTouchCounter = 0;

                    if(shurikenPowerUpTurns > 0)
                    shurikenPowerUpTurns--;
                    ninjaControlScript.thisGuysRigidbody.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

                }
            }
        }
    }


    public void PowerUpShurikenCheck()
    {
        if (shurikenPowerUp)
        {
            shurikenIcon.SetActive(true);
            shurikenProtection.gameObject.SetActive(true);
            shurikenIconTurnText.text = (int)shurikenPowerUpTurns + "";
            if (shurikenPowerUpTurns <= 0)
            {
                shurikenPowerUp = false;
            }
        }
        else if (!shurikenPowerUp)
        {
            shurikenIcon.SetActive(false);
            shurikenProtection.gameObject.SetActive(false);
            //shurikenPowerUpTouchCounter = 0;
        }
    }





    public void ChapterComplited()
    {
        
        ninjaControlScript.thisGuysAnimator.SetBool("isGameWin", true);
        ninjaControlScript.thisGuysRigidbody.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        failureText.text = "Baþarýlý";
        failureText.color = Color.green;
        Time.timeScale = 1;
        pausePanel.gameObject.SetActive(true);
    }

    public void PauseButtonClicked()
    {
        failureText.text = "";
        Time.timeScale = 0;
        pausePanel.gameObject.SetActive(true);
    }

    public void ResumeButtonClicked()
    {
        Time.timeScale = 1;
        
        if (gameWin)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            pausePanel.gameObject.SetActive(false);
        }
    }
    public void RestartButton()
    {
        //Application.LoadLevel(Application.loadedLevel);
        Time.timeScale = 1;
        //DontDestroyOnLoad(audioHolderControlScript);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  
    }
    public void MainMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
