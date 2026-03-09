using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    Camera cam;
    Vector2 mousePosition;

    [Header("References")]
    [SerializeField] Transform firingPoint;
    [SerializeField] Transform storedPoint;
    [SerializeField] GameObject ballPrefab;

    [Header("Shooting")]
    [SerializeField] float shootForce = 12f;
    [SerializeField] float shootCooldown = 0.3f;

    Ball firingBall;
    Ball storedBall;

    bool canShoot = true;

    void Start()
    {
        cam = Camera.main;
        SpawnInitialBalls();
    }

    void Update()
    {
        RotateTowardsMouse();
    }

    void SpawnInitialBalls()
    {
        firingBall = SpawnBallAtPoint(firingPoint);
        storedBall = SpawnBallAtPoint(storedPoint);
    }

    Ball SpawnBallAtPoint(Transform point)
    {
        GameObject ballObj = Instantiate(ballPrefab, point.position, Quaternion.identity);
        ballObj.transform.SetParent(point);

        Ball ball = ballObj.GetComponent<Ball>();

        BallColor randomColor = (BallColor)Random.Range(0, 4);
        ball.SetColor(randomColor);

        return ball;
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed && canShoot)
        {
            StartCoroutine(ShootRoutine());
        }
    }

    public void OnSwapBall(InputAction.CallbackContext context)
    {
        if (context.performed && canShoot)
        {
            SwapBalls();
        }
    }

    void RotateTowardsMouse()
    {
        Vector3 mouseWorld = cam.ScreenToWorldPoint(mousePosition);
        mouseWorld.z = 0;

        Vector3 direction = mouseWorld - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    IEnumerator ShootRoutine()
    {
        canShoot = false;

        if (firingBall == null)
        {
            canShoot = true;
            yield break;
        }

        // STEP 1: Detach firing ball
        Rigidbody2D rb = firingBall.GetComponent<Rigidbody2D>();
        firingBall.transform.SetParent(null);

        yield return new WaitForSeconds(0.02f);

        // STEP 2: Shoot the ball
        rb.linearVelocity = firingPoint.right * shootForce;

        yield return new WaitForSeconds(0.05f);

        // STEP 3: Move stored ball to firing position
        firingBall = storedBall;

        firingBall.transform.SetParent(firingPoint);
        firingBall.transform.localPosition = Vector3.zero;

        yield return new WaitForSeconds(0.05f);

        // STEP 4: Spawn new stored ball
        storedBall = SpawnBallAtPoint(storedPoint);

        // STEP 5: Cooldown before next shot
        yield return new WaitForSeconds(shootCooldown);

        canShoot = true;
    }

    void SwapBalls()
    {
        if (firingBall == null || storedBall == null) return;

        Ball temp = firingBall;
        firingBall = storedBall;
        storedBall = temp;

        firingBall.transform.SetParent(firingPoint);
        storedBall.transform.SetParent(storedPoint);

        firingBall.transform.localPosition = Vector3.zero;
        storedBall.transform.localPosition = Vector3.zero;
    }
}