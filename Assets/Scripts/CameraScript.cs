using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Transform playerTransform;
    
    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        var position = playerTransform.position;
        transform.position = new Vector3(position.x, position.y, -10);
    }
}
