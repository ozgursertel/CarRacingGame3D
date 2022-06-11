using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_Hold : MonoBehaviour
{
    Rigidbody rb;
    private bool isHolding;
    public int forceMultipler;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            rb.AddForce(Vector3.left*forceMultipler);
            transform.Rotate(new Vector3(0,-0.1f, 0));
            isHolding = true;
        }
        else
        {
            isHolding = false;
        }
        if(transform.position.x < 2 && !isHolding)
        {
            rb.AddForce(Vector3.right*forceMultipler);
            transform.Rotate(new Vector3(0, 0.1f, 0));
        }

        
            transform.Rotate(new Vector3(0, 0, 0));
       
    }
}
