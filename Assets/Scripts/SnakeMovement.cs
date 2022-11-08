using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public float sidesMovementSpeed = 1f;
    public float MouseSensetivity = 10f;
    public float forwardMovementSpeed = 3f;

    public Transform SnakeHead;
    public CharacterController SnakeController;
    //public Rigidbody SnakeHead;
    private Vector3 _previousMousePos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveSides = SnakeHead.right;
        Vector3 moveForward = SnakeHead.forward;

        SnakeController.Move(moveForward * forwardMovementSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
        {
            SnakeController.Move(-moveSides * sidesMovementSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            SnakeController.Move(moveSides * sidesMovementSpeed * Time.deltaTime);
        }

    }
}
