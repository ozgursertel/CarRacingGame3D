using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasCan : MonoBehaviour
{    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.isGameFinished == false)
        {
            transform.Rotate(new Vector3(0, 0.1f, 0));
        }
    }
}
