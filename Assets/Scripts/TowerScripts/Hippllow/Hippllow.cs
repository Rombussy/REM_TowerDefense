using UnityEngine;

public class Hippllow : MonoBehaviour
{
    [SerializeField] private Transform entity;
    [SerializeField] private float radius = 4f;
    [SerializeField] private GameObject featherPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int circlePoints = 14;
    [SerializeField] private float FeatherSpawnrate = 1.4f;
    [SerializeField] private float FeatherPerBurst = 1f;
    [SerializeField] private float FeatherInterval = 0.3f;

    private bool entityInRadius;
    private float nextFeatherTime = 0f;
    private int feathersShot = 0;

    void FindEntities()
    {
        if (entity == null)
        {
            GameObject normal = GameObject.FindGameObjectWithTag("Entity");

            if (normal != null)
                entity = normal.transform;
        }
    }

    void Update()
    {
        FindEntities();
        if (entity == null) return;

        DrawSightLine();

        bool inRadius = IsEntityInRadius();

        if (inRadius && Time.time > nextFeatherTime)
        {
            Shoot();
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

        entityInRadius = inRadius;
    }

    bool IsEntityInRadius()
    { 
        float distance = Vector2.Distance(transform.position,entity.position);
        return distance <= radius;
    }

    void Shoot()
    {
        if (featherPrefab == null || firePoint == null)
            return;

        GameObject feather = Instantiate(featherPrefab,firePoint.position,firePoint.rotation);

        FeatherProjectile projectile = feather.GetComponent<FeatherProjectile>();

        if (projectile != null)
        {
            projectile.SetTarget(entity);
        }
    }

    public void Upgrade2_Hippllow()
    {
        FeatherPerBurst += 1f;
        radius += 1f;
        FeatherSpawnrate -= 0.2f;
    }

    public void Upgrade3_Hippllow()
    {
        FeatherPerBurst += 1f;
        radius += 1f;
        FeatherSpawnrate -= 0.2f;
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
