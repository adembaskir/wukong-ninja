using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ButtonControl : MonoBehaviour
{
    static public int levelIndex = 1;

    public GameObject levelPanel;

    public void OpenLevelPanel()
    {
        levelPanel.SetActive(true);
    }
    public void CloseLevelPanel()
    {
        levelPanel.SetActive(false);
    }
    public void PlayButton()
    {
        SceneManager.LoadScene(levelIndex);
    }
    public void Level1Selected()
    {
        levelIndex = 1;
        SceneManager.LoadScene("Level1");
    }
    public void Level2Selected()
    {
        levelIndex = 2;
        SceneManager.LoadScene("Level2");
    }
    public void Level3Selected()
    {
        levelIndex = 3;
        SceneManager.LoadScene("Level3");
    }
}
