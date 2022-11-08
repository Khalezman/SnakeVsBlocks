using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] ParticleSystem WinParticle1;
    [SerializeField] ParticleSystem WinParticle2;
    [SerializeField] SnakeController _snake;

    [Header("Sounds")]
    [SerializeField] AudioSource LoseSound;
    [SerializeField] AudioSource WinSound;
    [SerializeField] AudioSource BackMusic;

    bool canProceed = false;

    public enum State
    {
        Menu,
        Paused,
        Starting,
        Playing,
        Win,
        Dead
    }

    private const string LevelIndexKey = "LevelIndex";

    public State CurrentState { get; private set; }

    public int LevelIndex
    {
        get => PlayerPrefs.GetInt(LevelIndexKey, 1);
        private set
        {
            PlayerPrefs.SetInt(LevelIndexKey, value);
            PlayerPrefs.Save();    
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentState = State.Starting;
        BackMusic.Play();
    }

    private void Update()
    {
        if(CurrentState == State.Playing) Cursor.lockState = CursorLockMode.Locked;
        if (CurrentState == State.Win)
        {
            if (Input.GetKeyDown(KeyCode.Space) && canProceed)
            {
                ToNextLevel();
            }
        }
        else if (CurrentState == State.Dead && canProceed)
        {
            if(Input.anyKeyDown) RestartGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && CurrentState == State.Playing)
        {
            CurrentState = State.Paused;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
    public void OnSnakeDeath()
    {
        Invoke("CanProceed", 2f);
        LoseSound.Play();
        CurrentState = State.Dead;
        BackMusic.Stop();
    }


    public void StartGame()
    {
        CurrentState = State.Playing;
    }

    private void RestartGame()
    {
        _snake.ResetScore();
        _snake.ResetTail();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void CanProceed()
    {
        canProceed = true;
    }
    public  void FinishGame()
    {
        BackMusic.Stop();
        CurrentState = State.Win;
    }

    public void ResumeGame()
    {
        CurrentState = State.Playing;
    }

    public void QuitGame()
    {
        PlayerPrefs.DeleteKey("Snake Parts");
        PlayerPrefs.DeleteKey("Score");
        Application.Quit();
    }

    public void Win()
    {
        BackMusic.Stop();
        WinParticle1.Play();
        WinParticle2.Play();
        LevelIndex++;
        WinSound.Play();
        Invoke("CanProceed", 2f);
        CurrentState = State.Win;
    }

    public void ToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
