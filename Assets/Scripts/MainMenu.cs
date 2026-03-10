using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartEasy()
    {
        DifficultyManager.Instance.SetDifficulty(GameDifficulty.Easy);
        SceneManager.LoadScene("Level1");
    }

    public void StartMedium()
    {
        DifficultyManager.Instance.SetDifficulty(GameDifficulty.Medium);
        SceneManager.LoadScene("Level1");
    }

    public void StartHard()
    {
        DifficultyManager.Instance.SetDifficulty(GameDifficulty.Hard);
        SceneManager.LoadScene("Level1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}