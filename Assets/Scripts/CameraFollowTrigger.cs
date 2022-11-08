using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTrigger : MonoBehaviour
{
    [SerializeField] CameraMovement _snakeCamera;
    [SerializeField] bool isFinish = false;
    [SerializeField] GameController GC;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out SnakeController snake)) return;
        if (!isFinish)
        {
            GC.StartGame();
            _snakeCamera.canFollow = true;
        } 
        else _snakeCamera.canFollow = false;
    }

}
