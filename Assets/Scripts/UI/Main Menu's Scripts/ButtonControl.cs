using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour
{
    public GameObject levelPanel;

    public void OpenLevelPanel()
    {
        levelPanel.SetActive(true);
    }
    public void CloseLevelPanel()
    {
        levelPanel.SetActive(false);
    }
    public void Level1Selected()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void Level2Selected()
    {

    }
    public void Level3Selected()
    {

    }
}
