using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadParalax : MonoBehaviour
{
    public GameObject[] roads;
    public int roadChange;
    private Transform player;
    int roadIndex = 0;
    private float countdown;
    [SerializeField] private float timer;
    private void Start()
    {
        StartCoroutine(initRoad());   
    }

    private IEnumerator initRoad()
    {
        yield return new WaitForSeconds(0.1f);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        countdown -= Time.deltaTime;
       
        if (!GameManager.isGameStarted || GameManager.isGameFinished)
        {
            return;
        }
        if (myApproximation(player.position.z % roadChange, 0,4f) && countdown<=0)
        {
            Debug.Log("Road Changed");
            roads[roadIndex % 7].transform.position = new Vector3(0, 0, roads[roadIndex % 7].transform.position.z + 1050);
            roadIndex++;
            countdown = timer;
        }
    }

    private bool myApproximation(float a, float b, float tolerance)
    {
        return (Mathf.Abs(a - b) < tolerance);
    }

    void FixedUpdate()
    {
        
    }
}
