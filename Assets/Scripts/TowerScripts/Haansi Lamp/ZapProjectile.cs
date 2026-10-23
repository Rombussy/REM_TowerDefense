using UnityEngine;

public class ZapProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 5f;
    private EntityHP entityHp;
    private Transform target;
    private float ProjectileDistance = 0.5f;

    void Start()
    {
        entityHp = FindFirstObjectByType<EntityHP>();
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= ProjectileDistance)
        {
            Destroy(gameObject);
            entityHp.Die();
        }
        Vector3 direction = target.position - transform.position;
        transform.position += direction.normalized * speed * Time.deltaTime;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}