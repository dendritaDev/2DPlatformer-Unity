using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    
    public string levelSelect, mainMenu;

    public GameObject pauseScreen;

    public bool isPaused;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if(Input.GetButtonDown("Menu"))
        {
            PauseUnpause();
        }
    }

    public void PauseUnpause()
    {
        if(isPaused)
        {
            isPaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1f; //esto es que el tiempo corre normal
        }
        else
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0f; //esto es que el tiempo se pausa
        }
    }

    public void LevelSelect()
    {
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);

        SceneManager.LoadScene(levelSelect);
        Time.timeScale = 1f; 
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;//esto lo ponemos aqui pq como en cuanto le damos al escape se pone en 0 si despues vamso a main menu p.e
                            //aun consta que el time scale es 0
    }
}
