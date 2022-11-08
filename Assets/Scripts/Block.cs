using UnityEngine;
using TMPro;
public class Block : MonoBehaviour
{
    [Header("Block Health Settings")]
    private int minHP;
    private int maxHP;

    [Header("Links")]
    public GameObject block;
    private GameController gameController;
    [SerializeField] GameObject destroyParticle;

    [Header("UI Settings")]
    public TextMeshPro hpText;

    Color blockColor;

    public int hpCount { get; private set; }

    private void Start()
    {

    }

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        //Random HP Count for Block
        minHP = Random.Range(1,gameController.LevelIndex + 2);
        maxHP = gameController.LevelIndex;
        hpCount = Random.Range(minHP, maxHP);
        hpText.text = $"{hpCount}";

        //Setting color in dependence of HP Count
        //Color 
        blockColor = new Color(0.2f * hpCount / 1.5f, 1f / hpCount, 0f, 1f);
        var foodRenderer = block.GetComponent<MeshRenderer>();
        foodRenderer.material.color = blockColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out SnakeController snake))
        {
            if (snake.snakeParts.Count <= hpCount - 1) snake.Die(); // Die if you don't have enough parts
            else
            {
                for (int i = 0; i < hpCount; i++)
                {
                        snake.RemoveTailPart();
                        Debug.Log("Block was hitted");
                        Debug.Log(hpCount);
                    
                }
                snake.GetScore(hpCount);
                BlockDestroy();
            }
        }
        else return;
    }

    private void BlockDestroy()
    {
        Instantiate(destroyParticle, transform.position, transform.rotation);
        ParticleSystem ps = destroyParticle.GetComponent<ParticleSystem>();
        var main = ps.main;
        main.startColor = blockColor;
        Destroy(block);
       // Destroy(destroyParticle, 0.1f);
        Debug.Log("Block destroyed");
    }
}
