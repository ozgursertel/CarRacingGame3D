using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    private char lane = 'R';

    public float speedMultipler;
    public float laneSwitchTimer;
    private Swipe swipeController;

    Animator animator;

    private Rigidbody rb;

    [SerializeField] private GameObject carParticle;

    private bool MoveLocked;

    void Start()
    {
        swipeController = GameObject.FindGameObjectWithTag("Swipe").GetComponent<Swipe>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        MoveLocked = false;
    }


    private void FixedUpdate()
    {
        if(!GameManager.isGameStarted || GameManager.isGameFinished)
        {
            return;
        }
        if (swipeController.Hold)
        {
            rb.AddForce(Vector3.forward * speedMultipler/3);
        }
        else
        {
            rb.AddForce(Vector3.forward * speedMultipler);

        }
        rb.velocity = new Vector3(0, 0, rb.velocity.z);
    }

    private void Update()
    {
        if (swipeController.SwipeLeft && lane != 'L' && !MoveLocked)
        {
            StartCoroutine(MoveLeft());
            lane = 'L';
        }
        if (swipeController.SwipeRight && lane != 'R' && !MoveLocked)
        {
            StartCoroutine(MoveRight());
            lane = 'R';
        }
        if (swipeController.Hold)
        {
            Debug.Log("Hold");
        }
    }
    private IEnumerator MoveLeft()
    {
        MoveLocked = true;
        rb.DOMoveX(-2f, laneSwitchTimer);
        transform.DORotate(new Vector3(30, -120, 0), laneSwitchTimer);
        yield return new WaitForSeconds(laneSwitchTimer);
        MoveLocked = false;
        transform.DORotate(new Vector3(0, -90, 0),0.3f);
    }

    private IEnumerator MoveRight()
    {
        MoveLocked = true;
        rb.DOMoveX(2f, laneSwitchTimer);
        transform.DORotate(new Vector3(-30, -60, 0), laneSwitchTimer);
        yield return new WaitForSeconds(laneSwitchTimer);
        MoveLocked = false;
        transform.DORotate(new Vector3(0, -90, 0), 0.3f);

    }

    public void Brake(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            Debug.Log("Brake Started");
        }
        if (context.canceled)
        {
            Debug.Log("Brake Finished");
        }
    }



    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Obstacle")
        {
            if (!GameManager.Instance.isContinue)
            {

                Debug.Log("Collison With Obstacle");
                carParticle.SetActive(false);

                GameManager.Instance.StartCountinuePanel();
            }
            else if (GameManager.Instance.isContinue)
            {

                AudioManager.instance.Play("CarCrash");
                Debug.Log("Collison With Obstacle");
                carParticle.SetActive(true);
                GameManager.Instance.GameFinished();
            }
        }

    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "Gas")
        {
            collision.gameObject.SetActive(false);
            GameManager.Instance.AddGas();
        }
    }


}
