using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerUpgradeUI : MonoBehaviour
{
    public Blobify blobify;
    public Button upgradeButton;
    public TextMeshProUGUI upgradeButtonText;
    public TextMeshProUGUI priceText;
    public int upgrade2Price = 1;
    public int upgrade3Price = 2;
    public GameObject upgradePanel;
    public static TowerUpgradeUI Instance;
    private bool justOpened = true;

    private void Awake()
    {
        Instance = this;

        if (upgradeButton != null)
        {
            upgradeButton.onClick.AddListener(TryUpgrade);
        }

        upgradePanel.SetActive(false);
    }

    public void OpenForTower(GameObject selectedTower)
    {
        blobify = selectedTower.GetComponent<Blobify>();

        if (blobify == null)
            return;

        upgradePanel.SetActive(true);
        justOpened = true;

        UpdatePrice();
    }

    public void ClosePanel()
    {
        upgradePanel.SetActive(false);
        blobify = null;
    }

    private void TryUpgrade()
    {
        if (blobify == null)
            return;

        int price;

        if (blobify.upgradeLevel == 0)
        {
            price = upgrade2Price;
        }
        else if (blobify.upgradeLevel == 1)
        {
            price = upgrade3Price;
        }
        else
        {
            return;
        }

        if (!CurrencyManager.instance.SpendTalas(price))
            return;

        if (blobify.upgradeLevel == 0)
        {
            blobify.Upgrade2_Blobify();
        }
        else
        {
            blobify.Upgrade3_Blobify();
        }

        blobify.upgradeLevel++;

        UpdatePrice();

        if (blobify.upgradeLevel >= 2)
        {
            ClosePanel();
        }
    }

    private void Update()
    {
        if (!upgradePanel.activeSelf)
            return;

        if (justOpened)
        {
            justOpened = false;
            return;
        }

        if (blobify == null)
        {
            ClosePanel();
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!RectTransformUtility.RectangleContainsScreenPoint(
                upgradePanel.GetComponent<RectTransform>(),
                Input.mousePosition))
            {
                ClosePanel();
                return;
            }
        }

        UpdatePrice();
    }

    private void UpdatePrice()
    {
        if (blobify == null || priceText == null)
            return;

        if (blobify.upgradeLevel == 0)
        {
            upgradeButtonText.text = "Upgrade 2";
            priceText.text = upgrade2Price.ToString();
        }
        else if (blobify.upgradeLevel == 1)
        {
            upgradeButtonText.text = "Upgrade 3";
            priceText.text = upgrade3Price.ToString();
        }
        else
        {
            priceText.text = "MAX";
        }

        int price = blobify.upgradeLevel == 0
            ? upgrade2Price
            : upgrade3Price;
    }

    private void OnDestroy()
    {
        if (upgradeButton != null)
        {
            upgradeButton.onClick.RemoveListener(TryUpgrade);
        }
    }
}
