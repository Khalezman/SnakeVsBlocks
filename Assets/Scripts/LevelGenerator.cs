using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] GameObject blockPrefab;
    [SerializeField] GameObject foodPrefab;
    [SerializeField] List<Transform> _blocksSpawnPoints;
    [SerializeField] List<Transform> _foodSpawnPoints;
    public List<Transform> _spawnedBlocksList;
    public List<Transform> _spawnedFoodList;
    private GameController _gameController;

    private int MinBlocks;
    private int MaxBlocks;

    public int blocksCount { get; private set; }
    private int foodCount;

    private int blocksSpawnPointIndex;
    private int foodSpawnPointIndex;
    private void Start()
    {

    }
    private void Awake()
    {
        _gameController = FindObjectOfType<GameController>();
        MinBlocks = _gameController.LevelIndex + 1;
        MaxBlocks = _gameController.LevelIndex + 5;
        if (MaxBlocks > _blocksSpawnPoints.Count) MaxBlocks = _blocksSpawnPoints.Count;
        SpawnBlocksAtRandomPos();
        SpawnFoodAtRandomPos();

        Debug.Log($"Spawned blocks: {_spawnedBlocksList.Count}");
        Debug.Log($"Spawned food: {_spawnedFoodList.Count}");
    }

    private void SpawnBlocksAtRandomPos()
    {
        blocksCount = Random.Range(MinBlocks, MaxBlocks); // get random blocks count
        if (blocksCount >= _blocksSpawnPoints.Count) blocksCount = _blocksSpawnPoints.Count; //if blocks count larger than spawn points count - set equal
        else if (blocksCount <= 0) blocksCount = 1; // spawn at least 1 block

        for (int i = 0; i < blocksCount - 1; i++)
        {
            blocksSpawnPointIndex = Random.Range(0, _blocksSpawnPoints.Count); // get random spawn point index

            while (_spawnedBlocksList.Contains(_blocksSpawnPoints[blocksSpawnPointIndex])) // if we get same position - change it
            {
                blocksSpawnPointIndex = Random.Range(0, _blocksSpawnPoints.Count);
                if (!_spawnedBlocksList.Contains(_blocksSpawnPoints[blocksSpawnPointIndex])) break;
            }

            _spawnedBlocksList.Add(_blocksSpawnPoints[blocksSpawnPointIndex]); // add spawn point to NON-USE-NEXTTIME list
            Debug.Log("Position added");

            SpawnBlock();
        }
    }

    private void SpawnFoodAtRandomPos()
    {
        foodCount = blocksCount - 10; //food is always in lower count than blocks
        if (foodCount <= 3) foodCount = blocksCount; // spawn food equal to blocks at least

        for (int i = 0; i < foodCount - 1; i++)
        {
            foodSpawnPointIndex = Random.Range(0, _foodSpawnPoints.Count); // get random spawn point index

            while (_spawnedFoodList.Contains(_foodSpawnPoints[foodSpawnPointIndex])) // if we get same position - change it
            {
                foodSpawnPointIndex = Random.Range(0, _foodSpawnPoints.Count);
                if (!_spawnedFoodList.Contains(_foodSpawnPoints[foodSpawnPointIndex])) break;
            }

            _spawnedFoodList.Add(_foodSpawnPoints[foodSpawnPointIndex]); // add spawn point to NON-USE-NEXTTIME list
            Debug.Log("Position added");

            SpawnFood();
        }
    }

    void SpawnBlock()
    {
        Vector3 randomSpawnPos = new Vector3(_blocksSpawnPoints[blocksSpawnPointIndex].position.x, _blocksSpawnPoints[blocksSpawnPointIndex].position.y + 0.4f, _blocksSpawnPoints[blocksSpawnPointIndex].position.z);
        Instantiate(blockPrefab, randomSpawnPos, Quaternion.identity);
    }

    void SpawnFood()
    {
        Vector3 randomSpawnPos = new Vector3(_foodSpawnPoints[foodSpawnPointIndex].position.x, _foodSpawnPoints[foodSpawnPointIndex].position.y, _foodSpawnPoints[foodSpawnPointIndex].position.z);
        Instantiate(foodPrefab, randomSpawnPos, Quaternion.identity);
    }

}
