using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTail : MonoBehaviour
{
    public Transform SnakeHead;

    private List<Transform> snakeParts = new List<Transform> ();
    private List<Vector3> positions = new List<Vector3> ();

    // Start is called before the first frame update
    void Start()
    {
        positions.Add(SnakeHead.position);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddPart()
    {
        Transform snakePart = Instantiate(SnakeHead, positions[positions.Count - 1], Quaternion.identity, transform);
        snakeParts.Add(snakePart);
        positions.Add(snakePart.position);
    }
}
