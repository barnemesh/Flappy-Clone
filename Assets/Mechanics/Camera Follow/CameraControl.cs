using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float smoothTime = 0.2f;

    private float _yVel;
    
    void FixedUpdate()
    {
        var position = transform.position;
        float smoothPos = Mathf.SmoothDamp(position.x, player.transform.position.x, ref _yVel, smoothTime);
        position = new Vector3(smoothPos, position.y, position.z);
        transform.position = position;
    }
}