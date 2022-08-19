using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankController : MonoBehaviour
{
    public enum bossStates { shooting, hurt, moving, ended}
    public bossStates currentStates;

    public Transform theBoss;

    public Animator anim;

    [Header("Movement")]
    public float moveSpeed;
    
    
    public Transform leftPoint, rightPoint;
    private bool moveRight;
    public GameObject mine;
    public Transform minePoint;
    public float timeBetweenMines;
    private float mineCounter;

    [Header("Shooting")]
    public GameObject bullet;
    public Transform firePoint; //el sitio de donde sale la bala
    public float timeBetweenShots;
    private float shotCounter;

    [Header("Hurt")]
    public float hurtTime;
    private float hurtCounter;

    public GameObject hitBox;

    [Header("Health")]
    public int health = 5;
    public GameObject explosion, winPlatform;
    private bool isDefeated;
    public float shootSpeedUp, mineSpeedUp;

    void Start()
    {
        currentStates = bossStates.shooting;
    }

    
    void Update()
    {
        
        switch(currentStates)
        {
            case bossStates.shooting:

                shotCounter -= Time.deltaTime;

                if(shotCounter <= 0 )
                {
                    shotCounter = timeBetweenShots;

                    var newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                    newBullet.transform.localScale = theBoss.localScale; //aqui hacemos que la bala mire para un lado u otro segun donde mire el boss
                }

                break;

            case bossStates.hurt:
                if(hurtCounter > 0)
                {
                    hurtCounter -= Time.deltaTime;
                    
                    if(hurtCounter <= 0)
                    {
                        currentStates = bossStates.moving;

                        mineCounter = 0;

                        if(isDefeated)
                        {
                            theBoss.gameObject.SetActive(false);
                            Instantiate(explosion, theBoss.position, theBoss.rotation);

                            winPlatform.gameObject.SetActive(true);

                            AudioManager.instance.StopBossMusic();

                            currentStates = bossStates.ended;

                        }
                    }
                }
                
                break;

            case bossStates.moving:

                if(moveRight)
                {
                    theBoss.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                    if(theBoss.position.x > rightPoint.position.x)
                    {
                        theBoss.localScale = new Vector3(1f, 1f, 1f); //en vez de usar flip cambiarfemos la sacala para grirarlo, ya que con el flip el firepoint se veria mal al flipear el sprite del tanque

                        moveRight = false;

                        EndMovement();

                    }
                }
                else
                {
                    theBoss.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                    if (theBoss.position.x < leftPoint.position.x)
                    {
                        theBoss.localScale = new Vector3(-1f, 1f, 1f);

                        moveRight = true;

                        EndMovement();

                    }
                }

                mineCounter -= Time.deltaTime;

                if(mineCounter <=0)
                {
                    mineCounter = timeBetweenMines;

                    Instantiate(mine, minePoint.position, minePoint.rotation);
                }

                break;
        }

    }

    public void TakeHit()
    {
        currentStates = bossStates.hurt;
        hurtCounter = hurtTime;

        anim.SetTrigger("Hit");

        BossTankMine[] mines = FindObjectsOfType<BossTankMine>(); //con estas 5 lineas lo que ahcemos es buscar todas las minas que hay puestas de anteriorx y explotarlas
        if(mines.Length > 0)
        {
            foreach(BossTankMine foundMine in mines)
            {
                foundMine.Explode();
            }
        }

        health--;

        if(health <= 0)
        {
            isDefeated = true;
        }
        else
        {
            timeBetweenShots /= shootSpeedUp; //esto es para que cuando tenga menos vida dispare mas rapido y ponga minas mas rapido
            timeBetweenMines /= mineSpeedUp;
        }
    }

    private void EndMovement()
    {
        currentStates = bossStates.shooting;

        shotCounter = 0f;

        anim.SetTrigger("StopMoving");

        hitBox.SetActive(true);
    }
}
