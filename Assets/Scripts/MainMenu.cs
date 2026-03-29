using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject layoutCanvas;
    public GameObject difficultyCanvas;

    public void SelectLayout1()
    {
        DifficultyManager.Instance.SetLayout("Level1");
        layoutCanvas.SetActive(false);
        difficultyCanvas.SetActive(true);
    }

    public void SelectLayout2()
    {
        DifficultyManager.Instance.SetLayout("Level2");
        layoutCanvas.SetActive(false);
        difficultyCanvas.SetActive(true);
    }

    public void SelectLayout3()
    {
        DifficultyManager.Instance.SetLayout("Level3");
        layoutCanvas.SetActive(false);
        difficultyCanvas.SetActive(true);
    }

    public void StartEasy()
    {
        DifficultyManager.Instance.SetDifficulty(GameDifficulty.Easy);
    }

    public void StartMedium()
    {
        DifficultyManager.Instance.SetDifficulty(GameDifficulty.Medium);
    }

    public void StartHard()
    {
        DifficultyManager.Instance.SetDifficulty(GameDifficulty.Hard);
    }

    public void BackToLayoutSelect()
    {
        layoutCanvas.SetActive(true);
        difficultyCanvas.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1.0f;
    }
}