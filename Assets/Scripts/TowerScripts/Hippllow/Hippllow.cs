using UnityEngine;

public class Hippllow : MonoBehaviour
{
    [SerializeField] private float radius = 4f;
    [SerializeField] private GameObject featherPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int circlePoints = 14;
    [SerializeField] private float FeatherSpawnrate = 1.4f;
    [SerializeField] private float FeatherPerBurst = 1f;
    [SerializeField] private float FeatherInterval = 0.3f;
    [SerializeField] private Sprite upgrade2;
    [SerializeField] private Sprite upgrade3;
    public int upgradeLevel = 0;
    private SpriteRenderer spriteRenderer;
    private float nextFeatherTime = 0f;
    private int feathersShot = 0;

    Transform FindTarget()
    {
        GameObject[] entities = GameObject.FindGameObjectsWithTag("Entity");

        foreach (GameObject normal in entities)
        {
            if (Vector2.Distance(transform.position, normal.transform.position) <= radius)
            {
                return normal.transform;
            }
        }
        return null;
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        DrawSightLine();
        Transform target = FindTarget();

        if (target == null)
        {
            feathersShot = 0;
            nextFeatherTime = 0f;
            return;
        }

        if (Time.time > nextFeatherTime)
        {
            Shoot(target);
            feathersShot++;
            
            if (feathersShot >= FeatherPerBurst)
            {
                feathersShot = 0;
                nextFeatherTime = Time.time + FeatherSpawnrate;
            }
            else
            {
                nextFeatherTime = Time.time + FeatherInterval;
            }
        }
    }

    void Shoot(Transform target)
    {
        if (featherPrefab == null || firePoint == null)
            return;

        GameObject feather = Instantiate(featherPrefab,firePoint.position,firePoint.rotation);

        FeatherProjectile projectile = feather.GetComponent<FeatherProjectile>();

        if (projectile != null)
        {
            projectile.SetTarget(target);
        }
    }

    public void Upgrade2_Hippllow()
    {
        FeatherPerBurst += 1f;
        radius += 1f;
        FeatherSpawnrate -= 0.2f;
        spriteRenderer.sprite = upgrade2;
    }

    public void Upgrade3_Hippllow()
    {
        FeatherPerBurst += 1f;
        radius += 1f;
        FeatherSpawnrate -= 0.2f;
        spriteRenderer.sprite = upgrade3;
    }

    void DrawSightLine()
    {
        if (lineRenderer == null || circlePoints < 1) return;
        lineRenderer.positionCount = circlePoints + 1;
        Vector3 center = transform.position;

        for (int i = 0; i <= circlePoints; i++)
        {
            float angle = (360f / circlePoints) * i;
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

            Vector3 point = center + new Vector3(x, y, 0f);

            lineRenderer.SetPosition(i, point);
        }
    }
}
