using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    public static PlayerStat Instance;

    public Slider healthSlider;
    public Slider easeSlider;

    public float maxHealth = 100f;
    public float currentHealth;
    public float ghostBarDelayDuration = 0.04f;
    public float ghostBarEaseDuration = 0.05f;

    private float timer = 0f;
    private float startingGhostValue;
    private bool isEasing = false;

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

    private void Start()
    {
        if (healthSlider != null && easeSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            easeSlider.maxValue = maxHealth;

            healthSlider.value = currentHealth;
            easeSlider.value = currentHealth;
        }
    }

    private void Update()
    {
        if (healthSlider != null && healthSlider.value != currentHealth)
        {
         healthSlider.value = currentHealth;
        }

        if (isEasing && easeSlider != null)
        {
            timer += Time.deltaTime;
            if (timer < ghostBarDelayDuration) return;

            float progress = (timer - ghostBarDelayDuration) / ghostBarEaseDuration;
            float easedT = Ease.EaseOutQuad(progress);

            easeSlider.value = Mathf.Lerp(startingGhostValue, currentHealth, easedT);

            if (progress >= 1f)
            {
                easeSlider.value = currentHealth;
                isEasing = false;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0f, maxHealth);

        if (easeSlider != null)
        {
            startingGhostValue = easeSlider.value;
            timer = 0f;
            isEasing = true;
        }

        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
        }
    }
}