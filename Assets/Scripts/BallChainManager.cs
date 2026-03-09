using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class BallChainManager : MonoBehaviour
{
    [SerializeField] private SplineContainer spline;
    [SerializeField] private GameObject ballPrefab;

    [SerializeField] private int ballCount = 10;

    [Header("Movement Settings")]
    [SerializeField] private float normalSpeed = 0.02f;
    [SerializeField] private float introSpeed = 0.08f;
    [SerializeField] private float introDuration = 1f;
    [SerializeField] private float spacing = 0.015f;

    [SerializeField] private List<Transform> balls = new List<Transform>();
    private float frontDistance = 0f;

    private float currentSpeed;
    private float introTimer;

    void Start()
    {
        SpawnBalls();

        currentSpeed = introSpeed;
        introTimer = introDuration;
    }

    void Update()
    {
        HandleIntroSpeed();
        MoveChain();
    }

    void SpawnBalls()
    {
        int ballsSpawned = 0;
        BallColor previousColor = BallColor.Red;
        bool firstGroup = true;

        while (ballsSpawned < ballCount)
        {
            BallColor randomColor;

            // Ensure new group color is different from previous
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

                balls.Add(ballObj.transform);
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
            balls[i].position = position;
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
}