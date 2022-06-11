using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    private int rngNumber;
    private int floorToRngNumber;
    [SerializeField] private float startPositionZ;
    public float zAdder;
    private float countdown;
    [SerializeField] private float timer;
    [SerializeField] private Quaternion rot;

    void FixedUpdate()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
            coinPosition();
            countdown = timer;
        }
    }

    private Vector3 coinPosition()
    {
        Vector3 position;
        rngNumber = Random.Range(0, 2);
        floorToRngNumber = (int)Mathf.Floor(rngNumber);
        switch (floorToRngNumber)
        {
            case 0:
                position = new Vector3(2, 4.5f, startPositionZ);
                spawnCoin(position);
                return position;
            case 1:
                position = new Vector3(-2, 4.5f, startPositionZ);
                spawnCoin(position);
                return position;
            default:
                Debug.Log("Rng numarasi 1 den b?y?k" + floorToRngNumber);
                position = new Vector3(-2, 4.1f, 120);
                return position;

        }

    }

    private void spawnCoin(Vector3 position)
    {
        float rngNumber = Random.Range(0,100);
        float floorToRngNumber = (int)Mathf.Floor(rngNumber);
        if (GameManager.isGameStarted)
        {
            if (rngNumber <= 50)
            {
                ObjectPooler.Instance.SpawnFromPool("Bronze Coin", position, rot);
                startPositionZ += zAdder;

            }
            else if (rngNumber <= 90)
            {
                ObjectPooler.Instance.SpawnFromPool("Silver Coin", position, rot);
                startPositionZ += zAdder;

            }
            else
            {
                ObjectPooler.Instance.SpawnFromPool("Gold Coin", position, rot);
                startPositionZ += zAdder;

            }
        }
        
    }
}
