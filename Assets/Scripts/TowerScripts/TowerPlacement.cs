using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class TowerPlacement : MonoBehaviour
{
    public Tilemap placableMap;
    public Tilemap nonPlacableMap;
    public GameObject ghostPrefab;

    private HashSet<Vector3Int> occupiedTiles = new HashSet<Vector3Int>();
    private GameObject ghostInstance;

    void Update()
    {
        HandlePlacementHover();
        HandlePlacementClick();
    }

    void HandlePlacementHover()
    {
        if (placableMap == null || ghostPrefab == null) return;

        if (TowerSelectionUI.selectedTower == null)
        {
            if (ghostInstance != null)
                Destroy(ghostInstance);
            return;
        }

        if (ghostInstance == null)
            ghostInstance = Instantiate(ghostPrefab);

        SpriteRenderer towerSr = TowerSelectionUI.selectedTower.GetComponentInChildren<SpriteRenderer>();
        SpriteRenderer ghostSr = ghostInstance.GetComponentInChildren<SpriteRenderer>();
        if (towerSr != null && ghostSr != null)
        {
            ghostSr.sprite = towerSr.sprite;
        }

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        Vector3Int cellPos = placableMap.WorldToCell(mouseWorldPos);

        Vector3 worldCenter = placableMap.GetCellCenterWorld(cellPos);
        worldCenter.z = 0;

        ghostInstance.transform.position = worldCenter + new Vector3(0, placableMap.cellSize.y * 0.25f);

        bool _afford = CurrencyManager.instance != null && CurrencyManager.instance.talas >= towerCost();
        bool valid = placableMap.HasTile(cellPos) &&
                     (nonPlacableMap == null || !nonPlacableMap.HasTile(cellPos)) &&
                     !occupiedTiles.Contains(cellPos);

        GhostTower ghostComp = ghostInstance.GetComponent<GhostTower>();
        if (ghostComp != null)
        {
            ghostComp.setValid(valid);
        }
    }



    void HandlePlacementClick()
    {
        if (placableMap == null) return;
        if (!Input.GetMouseButtonDown(0)) return;
        if (TowerSelectionUI.selectedTower == null) return;

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        Vector3Int cellPos = placableMap.WorldToCell(mouseWorldPos);

        if (!placableMap.HasTile(cellPos)) return;
        if (nonPlacableMap != null && nonPlacableMap.HasTile(cellPos)) return;
        if (occupiedTiles.Contains(cellPos)) return;

        int cost = towerCost();
        if (CurrencyManager.instance != null && CurrencyManager.instance.SpendTalas(cost))
        {
            Instantiate(TowerSelectionUI.selectedTower, ghostInstance.transform.position, Quaternion.identity);
            TowerSelectionUI.selectedTower = null;
            occupiedTiles.Add(cellPos);
        }

    }
    int towerCost()
    {
        if (TowerSelectionUI.selectedTower == null)
            return 0;
        TowerData tower = TowerSelectionUI.selectedTower.GetComponentInChildren<TowerData>();
        if (tower != null)
        {
            return tower.cost;
        }
        return 0;
    }
}