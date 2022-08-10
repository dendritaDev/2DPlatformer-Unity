using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth, maxHealth;

    public float invincibleLength;
    private float invincibleCounter; //esto lo que hace es que solo se pueda dañar por una trampa una vez cada los segundos (q sera un numero q marquemos) que marquemos en unity.

    private SpriteRenderer theSR;

    public GameObject deathEffect;

    public void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentHealth = maxHealth;

        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;

            if(invincibleCounter <= 0)
            {
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f); //esto es para que el alfa de sprite sea de 1
            }
        }
    }

    public void DealDamage()
    {
        if(invincibleCounter <= 0 )
        {
            currentHealth--;

            PlayerController.instance.anim.SetTrigger("Hurt");

            AudioManager.instance.PlaySFX(9);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                //gameObject.SetActive(false);

                Instantiate(deathEffect, PlayerController.instance.transform.position, PlayerController.instance.transform.rotation);

                AudioManager.instance.PlaySFX(8);

                LevelManager.instance.RespawnPlayer();
            }
            else
            {
                invincibleCounter = invincibleLength;
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, .5f); //hacemos que el alfa sea a la mitad como para notar cuanto tiempo es invencible a vover a hacerse daño

                PlayerController.instance.KnockBack();
            }

            UIController.instance.UpdateHealthDisplay();
        }

    }

    public void HealPlayer()
    {
        currentHealth++;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.UpdateHealthDisplay();
    }
}
