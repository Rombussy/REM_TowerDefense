using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerUpgradeUIBlobify : MonoBehaviour
{
    public static TowerUpgradeUIBlobify currentPanel;
    public Blobify blobify;
    public Button upgradeButton;
    public TextMeshProUGUI upgradeButtonText;
    public TextMeshProUGUI priceText;
    public int upgrade2Price = 1;
    public int upgrade3Price = 2;
    public GameObject upgradePanel;
    private bool justOpened = true;

    private void Awake()
    {
        if (blobify == null)
        {
            blobify = GetComponent<Blobify>();
            if (blobify == null )
            {
                blobify = GetComponentInParent<Blobify>();
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
        Blobify selectedBlobify = selectedTower.GetComponent<Blobify>();


        if (selectedBlobify == null)
            return;

        if (currentPanel != null && currentPanel != this)
        {
            currentPanel.ClosePanel();
        }
        currentPanel = this;
        blobify = selectedBlobify;
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
        if (upgradePanel == null ||!upgradePanel.activeSelf)
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
        UpdatePrice();
    }

    private void UpdatePrice()
    {
        if (blobify == null || priceText == null)
            return;

        if (blobify.upgradeLevel == 0)
        {
            upgradeButtonText.text = "Upgrade 2";
            priceText.text = "Tala: " + upgrade2Price.ToString();
        }
        else if (blobify.upgradeLevel == 1)
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
