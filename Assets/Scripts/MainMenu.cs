using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ResetStats()
    {
        PlayerPrefs.DeleteKey("Score");
        PlayerPrefs.DeleteKey("Best Score");
        PlayerPrefs.DeleteKey("Snake Parts");
        PlayerPrefs.DeleteKey("LevelIndex");
    }
    public void QuitGame()
    {
        PlayerPrefs.DeleteKey("Snake Parts");
        PlayerPrefs.DeleteKey("Score");
        Application.Quit();
    }
}
