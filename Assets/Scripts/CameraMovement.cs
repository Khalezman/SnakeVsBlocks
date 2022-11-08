using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform snakeHead;
    public Vector3 cameraOffset;
    public bool canFollow = false;

    private Vector3 lastCamPos;

    void Update()
    {
        if(canFollow) transform.position = snakeHead.position + cameraOffset;
        else lastCamPos = transform.position;
    }

}
