using UnityEngine;

public class Ball : MonoBehaviour
{
    public BallColor ballColor;

    public bool isShotBall = false;

    private SpriteRenderer sr;

    [Header("Ball Sprites (match order of BallColor enum)")]
    public Sprite[] ballSprites;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetColor(BallColor color)
    {
        ballColor = color;

        // Assign sprite based on enum index
        sr.sprite = ballSprites[(int)color];
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isShotBall) return;

        Ball hitBall = collision.gameObject.GetComponent<Ball>();

        if (hitBall != null)
        {
            BallChainManager.instance.InsertBall(this, hitBall);
        }
    }
}

public enum BallColor
{
    Red,
    Green,
    Yellow,
    Blue
}