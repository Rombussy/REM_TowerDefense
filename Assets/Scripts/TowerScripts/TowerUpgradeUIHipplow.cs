using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerUpgradeUIHipplow : MonoBehaviour
{
    public static TowerUpgradeUIHipplow currentPanel;
    public Hippllow hippllow;
    public Button upgradeButton;
    public TextMeshProUGUI upgradeButtonText;
    public TextMeshProUGUI priceText;
    public int upgrade2Price = 1;
    public int upgrade3Price = 2;
    public GameObject upgradePanel;
    private bool justOpened = true;

    private void Awake()
    {
        if (hippllow == null)
        {
            hippllow = GetComponent<Hippllow>();
            if (hippllow == null)
            {
                hippllow = GetComponentInParent<Hippllow>();
            }
        }

        if (upgradeButton != null)
        {
            upgradeButton.onClick.AddListener(TryUpgrade);
        }

        if (upgradePanel != null)
        {
            upgradePanel.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        OpenForTower(gameObject);
    }

    public void OpenForTower(GameObject selectedTower)
    {
        Hippllow selectedHippllow = selectedTower.GetComponent<Hippllow>();


        if (selectedHippllow == null)
            return;

        if (currentPanel != null && currentPanel != this)
        {
            currentPanel.ClosePanel();
        }
        currentPanel = this;
        hippllow = selectedHippllow;
        justOpened = true;

        if (upgradePanel != null)
        {
            upgradePanel.SetActive(true);
        }

        UpdatePrice();
    }

    public void ClosePanel()
    {
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(false);
        }

        if (currentPanel == this)
        {
            currentPanel = null;
        }
    }

    public void TryUpgrade()
    {
        if (hippllow == null)
            return;

        int price;

        if (hippllow.upgradeLevel == 0)
        {
            price = upgrade2Price;
        }
        else if (hippllow.upgradeLevel == 1)
        {
            price = upgrade3Price;
        }
        else
        {
            return;
        }

        if (!CurrencyManager.instance.SpendTalas(price))
            return;

        if (hippllow.upgradeLevel == 0)
        {
            hippllow.Upgrade2_Hippllow();
        }
        else
        {
            hippllow.Upgrade3_Hippllow();
        }

        hippllow.upgradeLevel++;

        UpdatePrice();

        if (hippllow.upgradeLevel >= 2)
        {
            ClosePanel();
        }
    }

    private void Update()
    {
        if (upgradePanel == null || !upgradePanel.activeSelf)
            return;

        if (justOpened)
        {
            justOpened = false;
            return;
        }

        if (hippllow == null)
        {
            ClosePanel();
            return;
        }
        UpdatePrice();
    }

    private void UpdatePrice()
    {
        if (hippllow == null || priceText == null)
            return;

        if (hippllow.upgradeLevel == 0)
        {
            upgradeButtonText.text = "Upgrade 2";
            priceText.text = "Tala: " + upgrade2Price.ToString();
        }
        else if (hippllow.upgradeLevel == 1)
        {
            upgradeButtonText.text = "Upgrade 3";
            priceText.text = "Tala: " + upgrade3Price.ToString();
        }
        else
        {
            priceText.text = "MAX";
        }
    }

    private void OnDestroy()
    {
        if (upgradeButton != null)
        {
            upgradeButton.onClick.RemoveListener(TryUpgrade);
        }
    }
}
