using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public static CheckpointController instance;

    public CheckPoint[] CheckPoints; //array de checkpoints 

    public Vector3 spawnPoint;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        spawnPoint = PlayerController.instance.transform.position;
    }

    public void DeactivateCheckpoints()
    {
        for(int i = 0; i<CheckPoints.Length; i++)
        {
            CheckPoints[i].ResetCheckPoint();
        }
    }
   public void SetSpawnPoint(Vector3 newSpawnPoint) //para guardar la posicion del ultimo checkpoint por el que haya pasado el jugador
    {
        spawnPoint = newSpawnPoint;
    }
}
