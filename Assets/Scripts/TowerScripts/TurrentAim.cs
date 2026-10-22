using UnityEngine;

public class TurrentAim : MonoBehaviour
{
    [SerializeField] Transform target;

    public void TurrentRotate()
    {
        if (target == null) return;
        Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}