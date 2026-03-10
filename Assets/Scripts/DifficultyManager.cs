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
                ballCount = 25;
                ballSpeed = 2f;
                break;

            case GameDifficulty.Medium:
                ballCount = 35;
                ballSpeed = 3f;
                break;

            case GameDifficulty.Hard:
                ballCount = 50;
                ballSpeed = 4.5f;
                break;
        }
    }
}