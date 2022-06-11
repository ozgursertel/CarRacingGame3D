using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{

    ObjectPooler objectPooler;
    private float timer;
    [SerializeField] private float startPositionZ;
    public float zAdder;
    [SerializeField] private Quaternion rotLeft;
    [SerializeField] private Quaternion rotRight;
    private Quaternion rot;
    private float countdown;
    private float rngNumber;
    private int floorToRngNumber;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        timer = Random.Range(2f, 3f);
        countdown = timer;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
            SpawnCar(carName(), carPosition(), rot);
            timer = Random.Range(1, 5f);
            countdown = timer;
        }

    }

    private string carName()
    {
        rngNumber = Random.Range(0, 7);
        floorToRngNumber = (int)Mathf.Floor(rngNumber);

        switch (floorToRngNumber)
        {
            case 0:
                return "B5Blue";
            case 1:
                return "B5Silver";
            case 2:
                return "DogeYellow";
            case 3:
                return "DogeRed";
            case 4:
                return "CooperGreen";
            case 5:
                return "CooperPurple";
            case 6:
                return "CooperRed";
            default:
                return " ";
        }
    }

    private Vector3 carPosition()
    {
        Vector3 position;
        rngNumber = Random.Range(0,2);
        floorToRngNumber = (int)Mathf.Floor(rngNumber);
        switch (floorToRngNumber)
        {
            case 0:
                position = new Vector3(2, 5f, startPositionZ);
                setRotRight();
                return position;
            case 1:
                position = new Vector3(-2,5f,startPositionZ);
                setRotLeft();
                return position;
            default:
                Debug.Log("Rng numarasi 1 den b?y?k" + floorToRngNumber);
                position = new Vector3(-2,4.1f,120);
                return position;
               
        }
        
    }

    private void setRotLeft()
    {
        rot = rotLeft;
    }

    private void setRotRight()
    {
        rot = rotRight;
    }

    private void SpawnCar(string carName,Vector3 carPosition,Quaternion rot)
    {
        if(!GameManager.isGameFinished && GameManager.isGameStarted)
        {
            ObjectPooler.Instance.SpawnFromPool(carName, carPosition, rot);
            startPositionZ += zAdder;
        }
    }
}
