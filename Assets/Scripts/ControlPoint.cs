using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlPoint : MonoBehaviour // Attached to "Control Point" named Game Object.
{
    [Header("UI Ayarlama kýsmý")]
    public TextMeshProUGUI timeCounterText,failureText,shurikenIconTurnText,startUpMessage;
    public GameObject pausePanel,energyIcon,emptyEnergyIcon,shurikenIcon;


    [Header("Unity ayarlama kýsmý.")]
    //float pointer_x = Input.GetAxis("Mouse X");
    //float pointer_y = Input.GetAxis("Mouse Y");
    private float xRot, yRot, timeCounter = 0f;
    public float xWallPower,yWallPower,zWallPower;
    public float rotationSpeed = 5f;
    public float mobileRotationSpeed = 0.5f;
    public float shootPower = 30f;
    public float jumpPower = 6f;
    public Rigidbody ninja;
    public LineRenderer line;
    public NinjaControl ninjaControlScript;
    public AudioHolderControl audioHolderControlScript;
    public GameObject topHead, shurikenProtection;
    public int shurikenPowerUpTurns = 0;
    public bool shurikenPowerUp,gameWin,gameLost = false;

    Touch touch;

    // Start is called before the first frame update
    void Start()
    {  
        ninjaControlScript = GameObject.Find("Main Ninja(Blue)").GetComponent<NinjaControl>();
        audioHolderControlScript = GameObject.Find("Audio Holder").GetComponent<AudioHolderControl>();
        ninjaControlScript.gameOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        PowerUpShurikenCheck();
        if (gameWin)
        {
            energyIcon.SetActive(false);
            emptyEnergyIcon.SetActive(false);
            shurikenIcon.SetActive(false);
            ChapterComplited();
        }
        if (!gameWin && ninjaControlScript.gameOn && !gameLost)
        {
            timeCounter += Time.deltaTime;
            timeCounterText.text = (int)timeCounter + "";
        }
        if (gameLost)
        {
            energyIcon.SetActive(false);
            emptyEnergyIcon.SetActive(false);
            shurikenIcon.SetActive(false);
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
            MobileControl();
            //MouseControl();
            
        }
    }
    public void MobileControl()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {

            // Get movement of the finger since last frame
            Vector3 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            // Rotate object across XY plane
            ninjaControlScript.ninja.transform.Rotate(0, touchDeltaPosition.x * mobileRotationSpeed, 0);
            line.gameObject.SetActive(true);
            line.SetPosition(0, topHead.transform.position);
            line.SetPosition(1, topHead.transform.position + topHead.transform.forward * 4f);
            startUpMessage.text = "";
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            ninjaControlScript.useShoot = false;
            ninjaControlScript.energy--;
            ninjaControlScript.thisGuysAnimator.SetBool("isLanded", false);
            ninjaControlScript.thisGuysAnimator.SetBool("isJumped", true);
            ninjaControlScript.thisGuysRigidbody.velocity = topHead.transform.forward * shootPower;
            ninjaControlScript.thisGuysRigidbody.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);


            line.gameObject.SetActive(false);

            if (shurikenPowerUpTurns > 0)
                shurikenPowerUpTurns--;
            ninjaControlScript.thisGuysRigidbody.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
    public void MouseControl()
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
                line.gameObject.SetActive(true);
                line.SetPosition(0, topHead.transform.position);
                line.SetPosition(1, topHead.transform.position + topHead.transform.forward * 4f);

                startUpMessage.text = "";
            }
            if (Input.GetMouseButtonUp(0))
            {
                ninjaControlScript.useShoot = false;
                ninjaControlScript.energy--;
                ninjaControlScript.thisGuysAnimator.SetBool("isLanded", false);
                ninjaControlScript.thisGuysAnimator.SetBool("isJumped", true);
                ninjaControlScript.thisGuysRigidbody.velocity = topHead.transform.forward * shootPower;
                ninjaControlScript.thisGuysRigidbody.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);


                line.gameObject.SetActive(false);

                if (shurikenPowerUpTurns > 0)
                    shurikenPowerUpTurns--;
                ninjaControlScript.thisGuysRigidbody.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
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
        pausePanel.gameObject.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  
    }
    public void MainMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
