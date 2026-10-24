using UnityEngine;

public class MusicNoteProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float hitDistance = 0.5f;
    private Vector3 direction;
    private Transform target;
    private CurrencyManager currencyManager;
    private EntityStatsManager entityStatsManager;

    void Start()
    {
        currencyManager = FindFirstObjectByType<CurrencyManager>();
    }

    public void LaunchNoteDirection(Vector3 newDirection, float newSpeed, Transform newTarget)
    {
        direction = newDirection.normalized;
        speed = newSpeed;
        target = newTarget;

        if (target != null)
        {
            entityStatsManager = target.GetComponent<EntityStatsManager>();
        }
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (target == null || !target.gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
            return;
        }

        if (Vector3.Distance(transform.position, target.position) <= hitDistance)
        {
            if (entityStatsManager != null)
            {
                entityStatsManager.damageEntity(2);
                currencyManager.AddTalas(10);
            }

            Destroy(gameObject);
        }
    }
}