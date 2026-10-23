using UnityEngine;

public class TowerSelectionUI : MonoBehaviour
{
    public static GameObject selectedTower;
 
    public void SelectTower(GameObject towerPrefab)
    {
        if (towerPrefab == selectedTower)
        {
            selectedTower = null;
            return;
        }

        selectedTower = towerPrefab;
    }
}
