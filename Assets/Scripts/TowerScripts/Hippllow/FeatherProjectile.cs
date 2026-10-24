using UnityEngine;

public class FeatherProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 5f;
    private CurrencyManager currencyManager;
    private EntityStatsManager entityStatsManager;
    private Vector3 startPosition;
    private Vector3 direction;
    private float ProjectileDistance = 0.5f;

    private Transform target;

    void Start()
    {
        entityStatsManager = FindFirstObjectByType<EntityStatsManager>();
        currencyManager = FindFirstObjectByType<CurrencyManager>();
        startPosition = transform.position;
        direction = transform.right;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (target == null || !target.gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;

        if (direction.magnitude <= ProjectileDistance)
        {
            EntityStatsManager enemyStats = target.GetComponent<EntityStatsManager>();

            if (enemyStats != null)
            {
                enemyStats.damageEntity(2);
                currencyManager.AddTalas(10);
            }

            Destroy(gameObject);
            return;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        transform.position += direction.normalized * speed * Time.deltaTime;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}