using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float waitToRespawn;

    public int gemsCollected;
    public float timeInLevel;

    public string levelToLoad;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        timeInLevel = 0;
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected); //este y el que esta en update de time los puse pq sino no me funcioanba, aunque antes no me hacia falta, no se pq toqué
        
    }

    // Update is called once per frame
    void Update()
    {
        timeInLevel += Time.deltaTime;
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCoroutine());

    }

    IEnumerator RespawnCoroutine()
    {
        PlayerController.instance.gameObject.SetActive(false);

        yield return new WaitForSeconds(waitToRespawn - (1f / UIController.instance.fadeSpeed)); 

        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + .2f);

        UIController.instance.FadeFromBlack();

        PlayerController.instance.gameObject.SetActive(true);

        PlayerController.instance.transform.position = CheckpointController.instance.spawnPoint;

        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;

        UIController.instance.UpdateHealthDisplay();
    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelCo());
    }
    
    public IEnumerator EndLevelCo()
    {
        AudioManager.instance.PlayLevelVictory();

        PlayerController.instance.stopInput = true;

        CameraController.instance.stopFollow = true;

        UIController.instance.leveCompleteText.SetActive(true);
        
        yield return new WaitForSeconds(1.5f);

        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + .25f);

        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1); //esto lo que hace es guardar una info, independientemente de si cerramos el juego o lo quesea, guarda la info y
                                                                                 //lo que hace es identificar la variable que se llama unlocked y dandole un valor de entero de 1

        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name); //guardamos en una variable de playerprefs el nombre de la escena

        if(PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_gems")) //chequeamos si ya tenemos guardadas un nivel de gemas recoelctadas de otra partida
        {
            
            if (gemsCollected > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected)) //lo que hacemos es crear una variable llamada nombre de la escena_gems y darle de valor
                                                                                                                //la variable gemscollected. Todo esto ademas se guarda en una memoria que despues de cerrar
                                                                                                                //el juego si lo abrimos, sigue recordando esta info y estando guardada
                                                                                                                //chequeamos si hemos recogido en alguna nueva partida mas gemas que anteriormente
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected); //de ser asi pues actualizamos el numero
            }
        }


        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_time")) //hacemos lo mismo con el tiempo
        {
            if(timeInLevel < PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel))
            {
                PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
            }
        }

        SceneManager.LoadScene(levelToLoad); //esto es para que cuando choquemos con la bandera y se haga el fadetoblack que se cargue el siguiente nivel
    }
}
