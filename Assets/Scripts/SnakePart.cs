using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakePart : MonoBehaviour
{
    public Transform SnakeHead;
    public SnakeTail snakeTail;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == SnakeHead)
        {
            snakeTail.AddPart();
            GameObject.Destroy(this);
            Debug.Log("ADDED???");
        }
        else Debug.Log("Nothing happened");
    }
}
