using UnityEngine;

public class EntityStatsManager : MonoBehaviour
{
    public int maxHealth = 1;
    public bool _Stealth = false;
    private int currentHealth;
    private void OnEnable()
    {
        currentHealth = maxHealth;
    }
    public void damageEntity(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            EnemyDie();
        }
    }
    public void EnemyDie()
    {
        gameObject.SetActive(false);
    }
}
