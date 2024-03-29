﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LSUIManager : MonoBehaviour
{

    public static LSUIManager instance;

    public Image fadeScreen;
    public float fadeSpeed;
    private bool shouldFadeToBlack, shouldFadeFromBlack;

    public GameObject levelInfoPanel;

    public Text levelName, gemsFound, gemsTarget, bestTime, timeTarget;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFadeToBlack) //si pasamos de 0 a 1
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 1)
            {
                shouldFadeToBlack = false;
        
            }
        }

        if (shouldFadeFromBlack) //si pasamos de 0 a 1
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 0)
            {
                shouldFadeFromBlack = false;
            }
        }
    }

    public void FadeToBlack()
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }

    public void FadeFromBlack()
    {
        shouldFadeToBlack = false;
        shouldFadeFromBlack = true;
    }

    public void ShowInfo(MapPoint levelInfo)
    {
        levelName.text = levelInfo.levelName;

        gemsFound.text = "FOUND: " + levelInfo.gemsCollected;
        gemsTarget.text = "IN LEVEL: " + levelInfo.totalGems;

        timeTarget.text = "TARGET: " + levelInfo.targetTime + "s";
        
        if(levelInfo.bestTime == 0)
        {
            bestTime.text = "BEST ---";
        } 
        else
        {
            bestTime.text = "BEST " + levelInfo.bestTime.ToString("F2") + "s"; //el F2 indica que se muestren dos digitos
        }

        levelInfoPanel.SetActive(true);
    }

    public void HideInfo()
    {
        levelInfoPanel.SetActive(false);
    }
}
