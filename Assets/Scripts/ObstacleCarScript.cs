using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCarScript : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        if(myApproximation(transform.position.x,2,0.5f) && !GameManager.isGameFinished)
        {
            transform.Translate(Vector3.forward/50,Space.World);
        }
        else if(myApproximation(transform.position.x, -2, 0.5f) && !GameManager.isGameFinished)
        {
            transform.Translate(Vector3.back/10,Space.World);
        }
        
    }
    
    private bool myApproximation(float a, float b, float tolerance)
    {
        return (Mathf.Abs(a - b) < tolerance);
    }

}
