using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LSManager : MonoBehaviour
{

    public LSPlayer thePlayer;

    private MapPoint[] allPoints;

    void Start()
    {
        allPoints = FindObjectsOfType<MapPoint>(); //esto nos mete todos los mappoints en el array, sin tener que ir arrastrandolos nosotros 1 a 1

        if(PlayerPrefs.HasKey("CurrentLevel"))
        {
            foreach(MapPoint point in allPoints) //con esto cuando terminamos el juego, guardamos la posicion en la que estabamos en el mapa al acabar ese nivel
            {
                if(point.LevelToLoad == PlayerPrefs.GetString("CurrentLevel"))
                {
                    thePlayer.transform.position = point.transform.position;
                    thePlayer.currentPoint = point;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadLevelCo());

    }

    public IEnumerator LoadLevelCo()
    {
        LSUIManager.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / LSUIManager.instance.fadeSpeed) + .25f);

        SceneManager.LoadScene(thePlayer.currentPoint.LevelToLoad);
    }
}
