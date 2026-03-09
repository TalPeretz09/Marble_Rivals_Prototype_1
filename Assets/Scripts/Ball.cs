using UnityEngine;

public class Ball : MonoBehaviour
{
    public BallColor ballColor;

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
}

public enum BallColor
{
    Red,
    Green,
    Yellow,
    Blue
}