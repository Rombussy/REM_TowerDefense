using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    public int talas = 100;
    public TextMeshProUGUI talasText;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        UpdateTalasUI();
    }

    public void AddTalas(int amount)
    {
        talas += amount;
        UpdateTalasUI();
    }

    public bool SpendTalas(int amount)
    {
        if (talas < amount)
        {
            Debug.Log("Not enough Talas!");
            return false;
        }

        talas -= amount;
        UpdateTalasUI();
        return true;
    }

    public void UpdateTalasUI()
    {
        if (talasText != null)
        {
            talasText.text = "Talas: " + talas;
        }
    }
}
