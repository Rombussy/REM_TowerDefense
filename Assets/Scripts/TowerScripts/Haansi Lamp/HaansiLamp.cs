using UnityEngine;

[System.Serializable]
public class HaansiLampUpgrade
{
    public float rangeIncrease;
    public float zapRateDecrease;
    public float radiusIncrease;
    public float zapPerBurstIncrease;
    public Sprite upgradeSprite;
    public int price;
}

public class HaansiLamp : MonoBehaviour
{
    [SerializeField] private float range = 8f;
    [SerializeField] private float rotSpeed = 5f;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject zapPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float radius = 4f;
    [SerializeField] private int circlePoints = 14;
    [SerializeField] private float ZapPerBurst = 1f;
    [SerializeField] private float ZapInterval = 0.3f;
    [SerializeField] private Sprite upgrade2;
    [SerializeField] private Sprite upgrade3;
    public HaansiLampUpgrade[] upgrades;
    public int upgradeLevel = 0;
    public GameObject towerUpgradePreFab;
    private SpriteRenderer spriteRenderer;
    public float zapRate = 1f;
    private float nextZapTime = 0f;
    private int zapsShot = 0;


    Transform FindTarget()
    {
        GameObject[] hiddenEntities = GameObject.FindGameObjectsWithTag("HiddenEntity");

        foreach (GameObject hidden in hiddenEntities)
        {
            if (Vector2.Distance(transform.position, hidden.transform.position) <= range)
            {
                return hidden.transform;
            }
        }

        GameObject[] entities = GameObject.FindGameObjectsWithTag("Entity");

        foreach (GameObject normal in entities)
        {
            if (Vector2.Distance(transform.position, normal.transform.position) <= range)
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
            zapsShot = 0;
            nextZapTime = 0f;
            return;
        }

        Vector2 direction = (Vector2)target.position - (Vector2)transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotSpeed * 100f * Time.deltaTime);

        if (Time.time >= nextZapTime)
        {
            Shoot(target);
            zapsShot++;

            if (zapsShot >= ZapPerBurst)
            {
                zapsShot = 0;
                nextZapTime = Time.time + zapRate;
            }
            else
            {
                nextZapTime = Time.time + ZapInterval;
            }
        }
    }

    void Shoot(Transform target)
    {
        if (zapPrefab == null || firePoint == null || target == null)
            return;

        GameObject zap = Instantiate(zapPrefab, firePoint.position, firePoint.rotation);
        ZapProjectile projectile = zap.GetComponent<ZapProjectile>();

        if (projectile != null)
        {
            projectile.SetTarget(target);
        }
    }

    public void Upgrade2_HaansiLamp()
    {
        range += 0.5f;
        zapRate -= 0.2f;
        radius += 0.5f;
        ZapPerBurst += 1f;
        spriteRenderer.sprite = upgrade2;
    }

    public void Upgrade3_HaansiLamp()
    {
        range += 0.5f;
        zapRate -= 0.2f;
        radius += 0.5f;
        ZapPerBurst += 1f;
        spriteRenderer.sprite = upgrade3;
    }

    void DrawSightLine()
    {
        if (lineRenderer == null || circlePoints < 1)
            return;

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
