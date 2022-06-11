using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public int coinValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.isGameFinished == false)
        {
            transform.Rotate(new Vector3(0,1f,0));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.addCoin(coinValue);
            this.gameObject.SetActive(false);
        }
    }
}
