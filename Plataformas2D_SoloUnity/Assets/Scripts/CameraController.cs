using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;

    public Transform farBackground, middleBackground;

    private Vector2 lastPos;

    public float minHeight, maxHeight; //lo maximo que podras subir o bajar la camara en el eje Y
    void Start()
    {
        lastPos = transform.position;
    }

    
    void Update()
    {
        transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z); //con la camara parece que se usa vector 3

        Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);
      
        farBackground.position = farBackground.position + new Vector3(amountToMove.x, amountToMove.y, 0f); //estas dos lineas son para el parallax effect
        middleBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * 0.5f;

        lastPos = transform.position;
    }
}
