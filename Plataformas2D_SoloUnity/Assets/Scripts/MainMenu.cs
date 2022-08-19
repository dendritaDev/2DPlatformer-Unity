using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string startScene, continueScene;

    public GameObject continueButton;

    private void Start()
    {
        if(PlayerPrefs.HasKey(startScene + "_unlocked")) //esto quiere decir que ya hemos jugado al juego y no emepzamos de 0
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(startScene);

        PlayerPrefs.DeleteAll(); //esto empezará el juego desde 0
    }


    public void ContinueGame()
    {
        SceneManager.LoadScene(continueScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
