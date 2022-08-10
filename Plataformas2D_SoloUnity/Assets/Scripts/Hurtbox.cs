using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public GameObject deathEffect;

    [Range(0,100)]
    public float chanceToDrop; //para que el enemigo suelte recompensa
    public GameObject collectible;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            other.transform.parent.gameObject.SetActive(false); //esto mata a la rana cuando le demos con la hitbox del player de los pies
            
            Instantiate(deathEffect, other.transform.position, other.transform.rotation);

            PlayerController.instance.Bounce();

            float dropSelect = Random.Range(0, 100f);

            AudioManager.instance.PlaySFX(3);

            if (dropSelect <= chanceToDrop)
            {
                Instantiate(collectible, other.transform.position, other.transform.rotation);

            }
        }
    }
}
