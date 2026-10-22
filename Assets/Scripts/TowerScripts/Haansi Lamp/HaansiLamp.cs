using UnityEngine;

public class HaansiLamp : MonoBehaviour
{
    [SerializeField] private Transform hidden_entity;
    [SerializeField] private float range = 8f;
    [SerializeField] private float sightTolerance = 0.98f;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject zapPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float radius = 4f;
    [SerializeField] private int circlePoints = 14;
    public TurrentAim TurrentAim;
    private bool hiddenentityWasInSight;

    void Update()
    {
        if (hidden_entity == null) return;

        DrawSightLine();

        bool inSights = IsHiddenEntityInSight();

        if (inSights && !hiddenentityWasInSight)
        {
            TurrentAim.TurrentRotate();
            Shoot();
        }
        hiddenentityWasInSight = inSights;
    }

    bool IsHiddenEntityInSight()
    {
        Vector2 direction = (Vector2)hidden_entity.position - (Vector2)transform.position;

        if (direction.magnitude > range)
            return false;

        direction.Normalize();
        Vector2 forward = transform.right;
        float dot = Vector2.Dot(forward, direction);
        return dot >= sightTolerance;
    }

    void Shoot()
    {
        if (zapPrefab == null || firePoint == null)
            return;

        GameObject zap = Instantiate(zapPrefab, firePoint.position,firePoint.rotation);
        ZapProjectile projectile = zap.GetComponent<ZapProjectile>();

        if (projectile != null)
        {
            projectile.SetTarget(hidden_entity);
        }
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
