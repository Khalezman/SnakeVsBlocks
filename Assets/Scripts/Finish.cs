using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [Header("Links")]
    public GameController GC;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out SnakeController snake)) return;
        GC.Win();
    }
}
