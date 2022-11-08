using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Food : MonoBehaviour
{
    [Header("Randomness settings")]
    private int minParts;
    private int maxParts;

    [Header("Links")]
    public GameObject food;
    public TextMeshPro CountText;
    public int partsCount { get; private set; }

    private GameController gameController;
    private LevelGenerator levelGenerator;
    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        levelGenerator = FindObjectOfType<LevelGenerator>();
        minParts = Random.Range(gameController.LevelIndex, levelGenerator.blocksCount - 5);
        if (minParts <= 0) minParts = 1;
        maxParts = levelGenerator.blocksCount;
        // Setting count of food
        partsCount = Random.Range(minParts, maxParts + 1);
        CountText.text = $"{partsCount}";

        // Setting food color by its count
        Color foodColor = new Color(0f, 0.2f * partsCount/2, 0f, 1f);
        var foodRenderer = food.GetComponent<MeshRenderer>();
        foodRenderer.material.color = foodColor;
    }

}
