using UnityEngine;

public class MusicNoteProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float hitDistance = 0.5f;

    private Vector3 direction;
    private Transform target;
    private EntityHP entityHp;

    public void LaunchNoteDirection(Vector3 newDirection, float newSpeed, Transform newTarget)
    {
        direction = newDirection.normalized;
        speed = newSpeed;
        target = newTarget;

        if (target != null)
        {
            entityHp = target.GetComponent<EntityHP>();
        }
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (target == null)
            return;

        if (Vector3.Distance(transform.position, target.position) <= hitDistance)
        {
            if (entityHp != null)
            {
                entityHp.Die();
            }

            Destroy(gameObject);
        }
    }
}