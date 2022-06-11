using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasCanSpawner : MonoBehaviour
{
    private int rngNumber;
    private int floorToRngNumber;
    [SerializeField]private float startPositionZ;
    private float countdown;
    [SerializeField]private float timer;
    public float zAdder;

    void FixedUpdate()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
            gasPosition();
            countdown = timer;
        }
    }

    private Vector3 gasPosition()
    {
        Vector3 position;
        rngNumber = Random.Range(0, 2);
        floorToRngNumber = (int)Mathf.Floor(rngNumber);
        switch (floorToRngNumber)
        {
            case 0:
                position = new Vector3(2, 4.5f, startPositionZ);
                spawnGasCan(position);
                return position;
            case 1:
                position = new Vector3(-2, 4.5f, startPositionZ);
                spawnGasCan(position);
                return position;
            default:
                Debug.Log("Rng numarasi 1 den b?y?k" + floorToRngNumber);
                position = new Vector3(-2, 4.1f, 120);
                return position;

        }

    }

    private void spawnGasCan(Vector3 position)
    {
        if (GameManager.isGameStarted)
        {
            ObjectPooler.Instance.SpawnFromPool("GasCan", position, Quaternion.identity);

            startPositionZ += zAdder;

        }
    }
}
