﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject objectToSwitch;

    private SpriteRenderer theSR;
    public Sprite downSprite;

    private bool hasSwitched;

    public bool deactivateOnSwitch;
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !hasSwitched)
        {
            if(deactivateOnSwitch)
            {
                objectToSwitch.SetActive(false);
            }
            else
            {
                objectToSwitch.SetActive(true);
            }

            theSR.sprite = downSprite;
            hasSwitched = true;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
