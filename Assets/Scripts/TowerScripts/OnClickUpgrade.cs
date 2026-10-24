using UnityEngine;

public class OnClickUpgrade : MonoBehaviour
{
    private void OnMouseDown()
    {
        TowerUpgradeUI.Instance.OpenForTower(gameObject);
    }
}