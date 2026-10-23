using UnityEngine;

public class EntityHP : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public float dmg = 50f;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }
}
