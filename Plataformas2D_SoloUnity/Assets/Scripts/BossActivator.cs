using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    public GameObject theBossBattle;
   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            theBossBattle.SetActive(true);

            gameObject.SetActive(false); //aqui desactivamos el bossactivator una vez ya ha hecho su funcion para q no hayan problemas o interferencias o algo

            AudioManager.instance.PlayBossMusic();
        }
    }
}
