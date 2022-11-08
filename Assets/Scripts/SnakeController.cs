using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SnakeController : MonoBehaviour
{
    [Header("Snake Tail")]
    public List<Transform> snakeParts;
    public float partsDistance;
    public GameObject SnakePartPrefab;

    [Header("Snake Movement Speed")]
    private float MoveSpeed = 3f;
    // private float rotationSpeed = 200f;

    private Transform _transform;

    [Header("UI")]
    public TextMeshPro SnakeFoodCount;
    public Transform mainCam;

    [Header("Sounds")]
    [SerializeField] AudioSource EatSound;
    [SerializeField] AudioSource ScoreSound;

    [Header("Game Logic")]
    public GameController GC;

    public int _score
    {
        get => PlayerPrefs.GetInt("Score", 0);
        private set
        {
            PlayerPrefs.SetInt("Score", value);
            PlayerPrefs.Save();
        }
    }

    public int BestScore
    {
        get => PlayerPrefs.GetInt("Best score", 0);
        private set
        {
            PlayerPrefs.SetInt("Best score", value);
            PlayerPrefs.Save();
        }
    }

    public int SavedSnakeParts
    {
        get => PlayerPrefs.GetInt("Snake Parts", 0);
        private set
        {
            PlayerPrefs.SetInt("Snake Parts", value);
            PlayerPrefs.Save();
        }
    }

    //private float xRotation;


    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        RestoreTail();
    }

    // Update is called once per frame
    void Update()
    {
        //If we're not playing - stop moving
        if (GC.CurrentState == GameController.State.Playing)
        {
            SnakeFoodCount.enabled = true;
            MoveSnake(_transform.position + _transform.forward * MoveSpeed * Time.deltaTime);
            if (Input.GetKey(KeyCode.A))
            {
                if (_transform.position.x <= -2.3f) MoveSnake(_transform.position + _transform.right * MoveSpeed * Time.deltaTime);
                else MoveSnake(_transform.position + -_transform.right * MoveSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                if (_transform.position.x >= 2.9f) MoveSnake(_transform.position + -_transform.right * MoveSpeed * Time.deltaTime);
                else MoveSnake(_transform.position + _transform.right * MoveSpeed * Time.deltaTime);
            }
            /* // float moveAngle = Input.GetAxis("Mouse X") * rotationSpeed;
             float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * MoveSpeed * Time.deltaTime;
             xRotation = mouseX;
             xRotation = Mathf.Clamp(xRotation, -60f, 60f);
             _transform.Rotate(0, xRotation, 0);
             //_transform.Rotate(0, moveAngle * Time.deltaTime, 0);*/
        }
        else if (GC.CurrentState == GameController.State.Starting || GC.CurrentState == GameController.State.Win)
        {
            SnakeFoodCount.enabled = false;
            MoveSnake(_transform.position + _transform.forward * MoveSpeed * Time.deltaTime);
        }
        else return;

        UpdateBestScore();

        SavedSnakeParts = snakeParts.Count;

        //Setting tail count for our snake
        SnakeFoodCount.text = $"{snakeParts.Count + 1}";
        SnakeFoodCount.transform.rotation = Quaternion.LookRotation(transform.position - mainCam.transform.position);

    }

    private void MoveSnake(Vector3 newPosition)
    {
        var sqrDistance = partsDistance * partsDistance;
        Vector3 lastPartPos = _transform.position;

        foreach(var part in snakeParts)
        {
            if ((part.position - lastPartPos).sqrMagnitude > sqrDistance)
            {
                var temp = part.position;
                part.position = lastPartPos;
                lastPartPos = temp;
            }
            else break;
        }
        _transform.position = newPosition;
    }

    public void Die()
    {
        GC.OnSnakeDeath();
        Debug.Log("You are dead");
    }

    public void GetScore(int Score)
    {
        _score = _score + Score;
        ScoreSound.Play();
        Debug.Log($"You score is {_score}");
    }

    public void ResetScore()
    {
        PlayerPrefs.DeleteKey("Score");
    }
    private void UpdateBestScore()
    {
        if (_score > BestScore)
        {
            BestScore = _score;
        }
        else BestScore = BestScore;
    }
    private void OnCollisionEnter(Collision collision)
    {
      if(collision.gameObject.tag == "Food")
        {
            if (collision.collider.TryGetComponent(out Food snakeFood))
            {
                AddTail(collision, snakeFood);
            }
            else Debug.Log("No Food");

        }
    }

    private void AddTail(Collision collision, Food snakeFood)
    {
        EatSound.Play();
        for (int i = 0; i < snakeFood.partsCount; i++)
        {
            ResetTail();
            var part = Instantiate(SnakePartPrefab);
            snakeParts.Add(part.transform);
           // SavedSnakeParts = snakeParts.Count;
        }
        Destroy(collision.gameObject);
    }

    private void RestoreTail()
    {
        if (SavedSnakeParts <= 0) return;
        else
        {
            for (int i = 0; i < SavedSnakeParts - 1 ; i++)
            {
                var part = Instantiate(SnakePartPrefab);
                snakeParts.Add(part.transform);
            }
        }
    }

    public void ResetTail()
    {
        PlayerPrefs.DeleteKey("Snake Parts");
    }

    public void RemoveTailPart()
    {
        snakeParts.RemoveAt(snakeParts.Count - 1);
        ResetTail();
        //SavedSnakeParts = snakeParts.Count;
    }
}
