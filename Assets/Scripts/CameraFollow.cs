using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraFollow : MonoBehaviour
{
    Transform target;
    public float smoothSpeed;

    public Vector3 offset;
    public Vector3 startCameraPos;
    private bool follow;
    private void Start()
    {
        follow = false;
    }

    private void Update()
    {
        if (follow)
        {
            FollowTargetWithOffset(offset,target);
        }
    }

    private void FollowTargetWithOffset(Vector3 offset,Transform target)
    {
        transform.position = new Vector3(0, offset.y, offset.z + target.position.z);
    }

    public IEnumerator StartCamera()
    {
        transform.DOMove(startCameraPos, 0.5f);
        transform.DORotate(new Vector3(20, 0, 0),0.5f);
        yield return new WaitForSeconds(0.5f);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        follow = true;
        
    }
}
