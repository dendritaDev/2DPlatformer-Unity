using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour
{

    public MapPoint up, right, down, left;

    public bool isLevel, isLocked;

    public string LevelToLoad, levelToCheck, levelName;

    public int gemsCollected, totalGems;
    public float bestTime, targetTime;

    public GameObject gemBadge, timeBadge; //si conseguimos llegar a la meta que s poen de tiempo y gema apreceran los iconocitos en ese nivel.

    void Start()
    {
        
        if(isLevel && LevelToLoad != null)
        {

            if(PlayerPrefs.HasKey(LevelToLoad + "_gems")) //aqui lo que hacemos es chequear si tenemos info guardada en esta variable que se llama nombre de escena + _gems
            {
                gemsCollected = PlayerPrefs.GetInt(LevelToLoad + "_gems"); //y de ser asi le damos ese valor a gemscollected
            }

            if (PlayerPrefs.HasKey(LevelToLoad + "_time"))
            {
                bestTime = PlayerPrefs.GetFloat(LevelToLoad + "_time");
            }

            if(gemsCollected >= totalGems && totalGems != 0)
            {
                gemBadge.SetActive(true);
            }

            if (bestTime <= targetTime && bestTime != 0)
            {
                timeBadge.SetActive(true);
            }

            isLocked = true;

            if(levelToCheck != null) //aqui lo que miramos es si el nivel anterior(el que tenemos que cheuqear que ya nos hayamos pasado) esta unlocked (que si nos lo hemos pasado le añadimos eso),
                                     //de ser así, en el current mappoint hacemos que islocked sea false y por tanto ya podemos jugar el siguiente nivel
            {
                if (PlayerPrefs.HasKey(levelToCheck + "_unlocked"))
                {
                    if(PlayerPrefs.GetInt(levelToCheck + "_unlocked") == 1)
                    {
                        isLocked = false;
                    }
                }
            }

            if(LevelToLoad == levelToCheck) //si el nombre to load y a chequear es lo mismo, queire decir que ese nivel esta dessbloqueado
            {
                isLocked = false;
            }
        }

    }

    void Update()
    {
        
    }

}
