using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIMainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject levelSelect;
    public string[] levelNames;

    void Start()
    {
        mainMenu.SetActive(true);
        levelSelect.SetActive(false);
    }

    public void OpenLevelSelect()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(true);
    }

    public void CloseLevelSelect()
    {
        mainMenu.SetActive(true);
        levelSelect.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SelectLevel1()
    {
        SceneManager.LoadScene(levelNames[0]);
    }

        public void SelectLevel2()
    {
        SceneManager.LoadScene(levelNames[1]);
    }

        public void SelectLevel3()
    {
        SceneManager.LoadScene(levelNames[2]);
    }

        public void SelectLevel4()
    {
        SceneManager.LoadScene(levelNames[3]);
    }

        public void SelectLevel5()
    {
        SceneManager.LoadScene(levelNames[4]);
    }
}
