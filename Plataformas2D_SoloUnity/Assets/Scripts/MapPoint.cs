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

    void Start()
    {
        
        if(isLevel && LevelToLoad != null)
        {
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
