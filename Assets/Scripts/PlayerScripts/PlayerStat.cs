using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public static PlayerStat Instance;

    public int maxHealth = 100;
    public int currentHealth;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"sIR BENN took {damage} damage Current Health: {currentHealth}");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Aww loser");
        }
    }
}