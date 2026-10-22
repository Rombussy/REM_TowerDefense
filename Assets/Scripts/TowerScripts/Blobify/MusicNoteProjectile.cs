using UnityEngine;

public class MusicNoteProjectile : MonoBehaviour
{
    private float speed = 10f;
    private Vector3 direction;
    //private float ProjectileDistance = 0.5f;

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void LaunchNoteDirection(Vector3 newDirection, float newSpeed)
    {
        direction = newDirection;
        speed = newSpeed;
    }
}