using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerUpgradeUI : MonoBehaviour
{
    public Hippllow hippllow;
    public Button upgradeButton;
    public TextMeshProUGUI priceText;
    public int upgrade2Price = 50;
    public int upgrade3Price = 100;
    private bool justOpened = true;

    private void Awake()
    {
        if (upgradeButton != null)
            upgradeButton.onClick.AddListener(TryUpgrade);
    }

    private void TryUpgrade()
    {
        if (hippllow == null)
            return;

        int price;

        if (hippllow.upgradeLevel == 0)
        {
            price = upgrade2Price;
        } else if (hippllow.upgradeLevel == 1)
        {
            price = upgrade3Price;
        } else
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
        Destroy(gameObject);
    }

    private void Update()
    {
        if (justOpened)
        {
            justOpened = false;
            return;
        }

        if (hippllow == null)
        {
            Destroy(gameObject);
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!RectTransformUtility.RectangleContainsScreenPoint(
                GetComponent<RectTransform>(),
                Input.mousePosition))
            {
                Destroy(gameObject);
                return;
            }
        }

        if (hippllow.upgradeLevel >= 2)
        {
            Destroy(gameObject);
            return;
        }

        int price;

        if (hippllow.upgradeLevel == 0)
        {
            price = upgrade2Price;
        }
        else
        {
            price = upgrade3Price;
        }

        priceText.text = price.ToString();
    }

    private void OnDestroy()
    {
        if (upgradeButton != null)
            upgradeButton.onClick.RemoveListener(TryUpgrade);
    }
}
