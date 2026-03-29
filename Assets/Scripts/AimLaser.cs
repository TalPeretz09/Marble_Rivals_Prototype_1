using UnityEngine;

public class AimLaser : MonoBehaviour
{
    public float maxDistance = 15f;
    public LayerMask hitLayers;

    private LineRenderer lr;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        ShootLaser();
    }

    void ShootLaser()
    {
        
        Vector2 direction = transform.right; // forward direction
        Vector2 origin = (Vector2)transform.position + direction * 2.1f;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance, hitLayers);

        Vector2 endPoint;

        if (hit.collider != null)
        {
            endPoint = hit.point;
        }
        else
        {
            endPoint = origin + direction * maxDistance;
        }

        lr.SetPosition(0, origin);
        lr.SetPosition(1, endPoint);
    }
}