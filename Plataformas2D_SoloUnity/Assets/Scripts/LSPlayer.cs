using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSPlayer : MonoBehaviour
{

    public MapPoint currentPoint;

    public float moveSpeed = 10f;

    public bool levelLoading;

    public LSManager theManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, moveSpeed * Time.deltaTime); //para q se desplace

        if(Vector3.Distance(transform.position, currentPoint.transform.position) < .1f) //esto es para que no se salga de las lineas, miramos la distancia entre el vector donde se encuentra
                                                                                        //el player y el siguiente mappoint, y mientras la distancia sea menor a .1 se puede mover
        {
            if (Input.GetAxisRaw("Horizontal") > .5f) //si es myao (getaxis raw obtiene 1 para dercha y -1 si se pulsa izquierda
            {

                if (currentPoint.right != null)
                {
                    SetNextPoint(currentPoint.right);
                    AudioManager.instance.PlaySFX(5);
                }

            }

            if (Input.GetAxisRaw("Horizontal") < -.5f) //si pulsamos a la izquierda y el currepoting tiene algo a la izquierda pos el nextpoint es ese punto
            {

                if (currentPoint.left != null)
                {
                    SetNextPoint(currentPoint.left);
                    AudioManager.instance.PlaySFX(5);
                }

            }

            if (Input.GetAxisRaw("Vertical") > .5f)
            {

                if (currentPoint.up != null)
                {
                    SetNextPoint(currentPoint.up);
                    AudioManager.instance.PlaySFX(5);
                }

            }

            if (Input.GetAxisRaw("Vertical") < -.5f)
            {

                if (currentPoint.down != null)
                {
                    SetNextPoint(currentPoint.down);
                    AudioManager.instance.PlaySFX(5);
                }

            }

            if(currentPoint.isLevel && currentPoint.LevelToLoad != "" && !currentPoint.isLocked) //lo segundo quiere decir que si leveltoload es diferente a nada si se podra cargar mapa, sino no
            {
                LSUIManager.instance.ShowInfo(currentPoint);

                if(Input.GetButtonDown("Jump")) //si pulsamos el espacio cargamos el nivel
                {
                    levelLoading = true;

                    AudioManager.instance.PlaySFX(4);

                    theManager.LoadLevel();
                }
            }

        }

        
    }

    public void SetNextPoint(MapPoint nextPoint)
    {
        currentPoint = nextPoint;
        LSUIManager.instance.HideInfo();
    }

}

