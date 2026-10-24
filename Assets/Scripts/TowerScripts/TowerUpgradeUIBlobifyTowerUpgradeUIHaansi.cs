using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerUpgradeUIHaansi : MonoBehaviour
{
    public static TowerUpgradeUIHaansi currentPanel;
    public HaansiLamp haansi;
    public Button upgradeButton;
    public TextMeshProUGUI upgradeButtonText;
    public TextMeshProUGUI priceText;
    public int upgrade2Price = 1;
    public int upgrade3Price = 2;
    public GameObject upgradePanel;
    private bool justOpened = true;

    private void Awake()
    {
        if (haansi == null)
        {
            haansi = GetComponent<HaansiLamp>();
            if (haansi == null)
            {
                haansi = GetComponentInParent<HaansiLamp>();
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
        HaansiLamp selectedHaansi = selectedTower.GetComponent<HaansiLamp>();


        if (selectedHaansi == null)
            return;

        if (currentPanel != null && currentPanel != this)
        {
            currentPanel.ClosePanel();
        }
        currentPanel = this;
        haansi = selectedHaansi;
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
        if (haansi == null)
            return;

        int price;

        if (haansi.upgradeLevel == 0)
        {
            price = upgrade2Price;
        }
        else if (haansi.upgradeLevel == 1)
        {
            price = upgrade3Price;
        }
        else
        {
            return;
        }

        if (!CurrencyManager.instance.SpendTalas(price))
            return;

        if (haansi.upgradeLevel == 0)
        {
            haansi.Upgrade2_HaansiLamp();
        }
        else
        {
            haansi.Upgrade3_HaansiLamp();
        }

        haansi.upgradeLevel++;

        UpdatePrice();

        if (haansi.upgradeLevel >= 2)
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

        if (haansi == null)
        {
            ClosePanel();
            return;
        }
        UpdatePrice();
    }

    private void UpdatePrice()
    {
        if (haansi == null || priceText == null)
            return;

        if (haansi.upgradeLevel == 0)
        {
            upgradeButtonText.text = "Upgrade 2";
            priceText.text = "Tala: " + upgrade2Price.ToString();
        }
        else if (haansi.upgradeLevel == 1)
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
