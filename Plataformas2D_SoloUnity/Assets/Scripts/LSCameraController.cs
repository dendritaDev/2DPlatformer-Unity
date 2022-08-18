using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSCameraController : MonoBehaviour
{

    public Vector2 minPos, maxPos;

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate() //utilizamos esto para que la camara no se mueva exactamente en el mismo momento que el jugador y se vea como mas guay por la sensacion q puede dar
    {
        float xPos = Mathf.Clamp(target.position.x, minPos.x, maxPos.x);
        float yPos = Mathf.Clamp(target.position.y, minPos.x, maxPos.y);

        transform.position = new Vector3(xPos, yPos, transform.position.z); //con camara siempre usamos vector3
    }
}
