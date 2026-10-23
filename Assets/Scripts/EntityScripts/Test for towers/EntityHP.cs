using UnityEngine;

public class EntityHP : MonoBehaviour
{
    public float maxHealth = 1f;
    public float currentHealth;
    public bool isStealth = false;

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}