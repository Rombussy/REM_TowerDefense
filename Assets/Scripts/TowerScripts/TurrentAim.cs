using UnityEngine;

public class TurrentAim : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] private float rotSpeed = 5;

    public void TurrentRotate()
    {
        if (target == null) return;
        Vector2 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed);
    }
}