using UnityEngine;

public class ZapProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 5f;
    private EntityStatsManager entityStatsManager;
    private Transform target;
    private float ProjectileDistance = 0.5f;

    void Start()
    {
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
            if (entityStatsManager != null)
            {
                entityStatsManager.damageEntity(2);
            }
            Destroy(gameObject);
            return;
        }
        transform.position += direction.normalized * speed * Time.deltaTime;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        entityStatsManager = null;

        if (target != null)
        {
            entityStatsManager = target.GetComponent<EntityStatsManager>();
        }
    }
}