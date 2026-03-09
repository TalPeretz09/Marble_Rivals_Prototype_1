using UnityEngine;

public class Ball : MonoBehaviour
{
    public BallColor ballColor;

    public bool isShotBall = false;

    public void SetColor(BallColor color)
    {
        ballColor = color;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        switch (color)
        {
            case BallColor.Red:
                sr.color = Color.red;
                break;
            case BallColor.Green:
                sr.color = Color.green;
                break;
            case BallColor.Yellow:
                sr.color = Color.yellow;
                break;
            case BallColor.Blue:
                sr.color = Color.blue;
                break;
        }
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