using UnityEngine;

public class FeatherProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 5f;
    private EntityHP entityHp;
    private Vector3 startPosition;
    private Vector3 direction;
    private float ProjectileDistance = 0.5f;

    private Transform target;

    void Start()
    {
        entityHp = FindFirstObjectByType<EntityHP>();
        startPosition = transform.position;
        direction = transform.right;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;

        if (direction.magnitude <= ProjectileDistance)
        {
            Destroy(gameObject);
            entityHp.TakeDamage(entityHp.dmg);
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