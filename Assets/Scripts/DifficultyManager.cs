using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance;

    public GameDifficulty currentDifficulty;

    public string selectedSceneName;
    public int ballCount;
    public float ballSpeed;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetLayout(string sceneName)
    {
        selectedSceneName = sceneName;
    }

    public void SetDifficulty(GameDifficulty difficulty)
    {
        currentDifficulty = difficulty;

        switch (difficulty)
        {
            case GameDifficulty.Easy:
                ballCount = 65;
                ballSpeed = 0.03f;
                break;

            case GameDifficulty.Medium:
                ballCount = 70;
                ballSpeed = 0.04f;
                break;

            case GameDifficulty.Hard:
                ballCount = 75;
                ballSpeed = 0.05f;
                break;
        }

        SceneManager.LoadScene(selectedSceneName);
    }
}