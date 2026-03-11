using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Splines;

public class BallChainManager : MonoBehaviour
{
    public static BallChainManager instance;

    [Header("Objects")]
    [SerializeField] private SplineContainer spline;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Canvas winLossScreen;
    [SerializeField] private TMP_Text winLossText;

    [SerializeField] private int ballCount;

    [Header("Movement Settings")]
    [SerializeField] private float normalSpeed;
    [SerializeField] private float introSpeed = 0.2f;
    [SerializeField] private float introDuration = 1f;
    [SerializeField] private float spacing = 0.01f;
    [SerializeField] private float loseDistance = 1f;

    [SerializeField] private List<Ball> balls = new List<Ball>();

    private float frontDistance = 0f;

    private float currentSpeed;
    private float introTimer;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ballCount = DifficultyManager.Instance.ballCount;
        normalSpeed = DifficultyManager.Instance.ballSpeed;

        winLossScreen.enabled = false;

        SpawnBalls();

        currentSpeed = introSpeed;
        introTimer = introDuration;

        Time.timeScale = 1f;
    }

    void Update()
    {
        HandleIntroSpeed();
        MoveChain();
        CheckLoseCondition();
        CheckWinCondition();
    }

    void SpawnBalls()
    {
        int ballsSpawned = 0;
        BallColor previousColor = BallColor.Red;
        bool firstGroup = true;

        while (ballsSpawned < ballCount)
        {
            BallColor randomColor;

            do
            {
                randomColor = (BallColor)Random.Range(0, 4);
            }
            while (!firstGroup && randomColor == previousColor);

            firstGroup = false;
            previousColor = randomColor;

            int groupSize = Random.Range(1, 4);

            for (int i = 0; i < groupSize; i++)
            {
                if (ballsSpawned >= ballCount)
                    break;

                GameObject ballObj = Instantiate(ballPrefab);

                Ball ballScript = ballObj.GetComponent<Ball>();
                ballScript.SetColor(randomColor);

                balls.Add(ballScript);

                ballsSpawned++;
            }
        }
    }

    void MoveChain()
    {
        frontDistance += currentSpeed * Time.deltaTime;

        for (int i = 0; i < balls.Count; i++)
        {
            float distance = frontDistance - (i * spacing);

            Vector3 position = spline.EvaluatePosition(distance);

            balls[i].transform.position = position;
        }
    }

    void HandleIntroSpeed()
    {
        if (introTimer > 0)
        {
            introTimer -= Time.deltaTime;

            if (introTimer <= 0)
            {
                currentSpeed = normalSpeed;
            }
        }
    }

    public void InsertBall(Ball shotBall, Ball hitBall)
    {
        int index = balls.IndexOf(hitBall);

        if (index == -1) return;

        Rigidbody2D rb = shotBall.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;

        shotBall.isShotBall = false;

        balls.Insert(index + 1, shotBall);

        CheckForMatches(index + 1);
    }

    void CheckForMatches(int index)
    {
        BallColor color = balls[index].ballColor;

        List<Ball> matchedBalls = new List<Ball>();
        matchedBalls.Add(balls[index]);

        // Check left
        for (int i = index - 1; i >= 0; i--)
        {
            if (balls[i].ballColor == color)
                matchedBalls.Add(balls[i]);
            else
                break;
        }

        // Check right
        for (int i = index + 1; i < balls.Count; i++)
        {
            if (balls[i].ballColor == color)
                matchedBalls.Add(balls[i]);
            else
                break;
        }

        if (matchedBalls.Count >= 3)
        {
            DestroyMatch(matchedBalls);
        }
    }

    void DestroyMatch(List<Ball> matchedBalls)
    {
        foreach (Ball ball in matchedBalls)
        {
            balls.Remove(ball);
            Destroy(ball.gameObject);
        }

        StartCoroutine(CollapseChain());
    }

    System.Collections.IEnumerator CollapseChain()
    {
        yield return new WaitForSeconds(0.15f);

        // Check if new matches formed after collapse
        for (int i = 0; i < balls.Count; i++)
        {
            CheckForMatches(i);
        }
    }

    void CheckLoseCondition()
    {
        if (frontDistance >= loseDistance)
        {
            Debug.Log("Game Over!");

            currentSpeed = 0f;

            winLossScreen.enabled = true;
            winLossText.text = "You Lose!";

            Time.timeScale = 0f;
        }
    }

    void CheckWinCondition()
    {
        if(balls.Count == 0)
        {
            Debug.Log("You Win!");

            currentSpeed = 0f;

            winLossScreen.enabled = true;
            winLossText.text = "You Win!";

            Time.timeScale = 0f;
        }
    }
}