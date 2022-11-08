using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] SnakeController _snakeController;
    [SerializeField] Transform _snakeHead;
    [SerializeField] Transform _finishLine;
    [SerializeField] Transform _startLine;
    [SerializeField] GameController GC;

    [Header("UI Links")]
    [SerializeField] Text _scoreText;
    [SerializeField] GameObject _winScreen;
    [SerializeField] GameObject _mainScreen;
    [SerializeField] GameObject _loseScreen;
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] GameObject _mainScreenBack;
    [SerializeField] Text _bestScore;
    [SerializeField] Text _currentScore;
    [SerializeField] Text _lvlPassed;
    [SerializeField] Slider _levelProgressBar;
    [SerializeField] Text _currentLVLText;
    [SerializeField] Text _nextLvlText;
    [SerializeField] Animator WinScreen_Open;
    [SerializeField] Animator MainScreen_FadeIn;
    [SerializeField] Animator LoseScreen_Open;

    private bool isFaded = true;


    private float _startZ;
    private float _acceptedSnakeDistance = 0.8f;

    private void Start()
    {
        _startZ = _startLine.position.z;
        _currentLVLText.text = $"{GC.LevelIndex}";
        _nextLvlText.text = $"{GC.LevelIndex + 1}";
        _winScreen.SetActive(false);
        _loseScreen.SetActive(false);
        _mainScreen.SetActive(true);
        _pauseMenu.SetActive(false);
        MainScreen_FadeIn.SetBool("Faded", !isFaded);
    }
    // Update is called once per frame
    private void Update()
    {
        LevelProgressUpdate();
        _scoreText.text = $"Score: {_snakeController._score}";
        _currentScore.text = $"Score: {_snakeController._score}";
        // _lvlPassed.text = 
        _bestScore.text = $"Best score: {_snakeController.BestScore}";
        if (GC.CurrentState == GameController.State.Win) WinScreen();
        if (GC.CurrentState == GameController.State.Dead) LoseScreen();
        if (GC.CurrentState == GameController.State.Paused)
        {
            _pauseMenu.SetActive(true);
            _mainScreen.SetActive(false);
        }
        else
        {
            _pauseMenu.SetActive(false);
            _mainScreen.SetActive(true);
        }

        if(GC.CurrentState != GameController.State.Starting) _mainScreenBack.SetActive(false);

    }

    private void LevelProgressUpdate()
    {
        float _currentZ = _snakeHead.position.z;
        float _finishZ = _finishLine.transform.position.z;
        float t = Mathf.InverseLerp(_startZ, _finishZ + _acceptedSnakeDistance, _currentZ);
        _levelProgressBar.value = t;

    }

    public void WinScreen()
    {
        _winScreen.SetActive(true);
        bool isOpen = true;
        WinScreen_Open.SetBool("opened", isOpen);
    }

    public void LoseScreen()
    {
        _loseScreen.SetActive(true);
        bool isOpen = true;
        LoseScreen_Open.SetBool("opened", isOpen);

    }
}
