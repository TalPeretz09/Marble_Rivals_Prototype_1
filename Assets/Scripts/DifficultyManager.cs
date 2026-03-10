using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance;

    public GameDifficulty currentDifficulty;

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

    public void SetDifficulty(GameDifficulty difficulty)
    {
        currentDifficulty = difficulty;

        switch (difficulty)
        {
            case GameDifficulty.Easy:
                ballCount = 60;
                ballSpeed = 0.015f;
                break;

            case GameDifficulty.Medium:
                ballCount = 65;
                ballSpeed = 0.02f;
                break;

            case GameDifficulty.Hard:
                ballCount = 70;
                ballSpeed = 0.025f;
                break;
        }
    }
}