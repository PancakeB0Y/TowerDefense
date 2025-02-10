using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    public static TowerPlacement instance { get; private set; }

    [Header("Collision Layers")]
    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] LayerMask groundLayer;

    [Header("Grid Objects")]
    [SerializeField] Grid grid;
    [SerializeField] GameObject gridPlane;

    [Header("Grid Properties")]
    [SerializeField] Color gridColor = Color.white;
    [SerializeField] Color unplacableGridColor = Color.red;

    [Header("UI Elements")]
    [SerializeField] GameObject towerInfoUI;
    TextMeshProUGUI towerInfoText;

    Material gridShaderMaterial;

    Camera playerCamera;

    GameObject currentPlacingTower;

    void Awake()
    {
        //Ensure singleton
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        playerCamera = Camera.main;

        if (gridPlane != null)
        {
            gridShaderMaterial = gridPlane.GetComponent<Renderer>().material;
        }

        if (towerInfoUI != null)
        {
            towerInfoText = towerInfoUI.GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            DeselectTower();
        }

        if (currentPlacingTower == null)
        {
            return;
        }

        gridPlane.SetActive(true);
        gridShaderMaterial.SetColor("_Color", gridColor);

        TowerController currentTowerController = currentPlacingTower.GetComponent<TowerController>();

        Ray cameraRay = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        //Move tower to mouse position
        if (Physics.Raycast(cameraRay, out hitInfo, 100f, groundLayer))
        {
            //Map tower position to grid position
            Vector3Int gridPosition = grid.WorldToCell(hitInfo.point);
            Vector3 worldPosition = grid.CellToWorld(gridPosition);
            worldPosition = new Vector3(worldPosition.x + 0.6f, worldPosition.y, worldPosition.z + 0.6f);
            currentPlacingTower.transform.position = worldPosition;

            if (currentTowerController != null)
            {
                currentTowerController.ShowTowerRange();
            }
        }

        BoxCollider towerCollider = currentPlacingTower.gameObject.GetComponent<BoxCollider>();
        Vector3 halfExtends = towerCollider.size / 2;
        towerCollider.isTrigger = true;

        //Check if tower can be placed on the current position
        if (Physics.CheckBox(currentPlacingTower.transform.position, halfExtends, Quaternion.identity, obstacleLayer, QueryTriggerInteraction.Ignore))
        {
            gridShaderMaterial.SetColor("_Color", unplacableGridColor);
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (MoneyManager.instance.PurchaseTower(currentTowerController))
            {
                towerCollider.isTrigger = false;

                PlaceTower(currentTowerController);
            }
        }
  
    }

    void PlaceTower(TowerController towerController)
    {
        //Stop showing the range of the tower
        towerController.HideTowerRange();

        //Stop showing the placement grid
        gridPlane.SetActive(false);

        //Set state of the tower
        if (towerController != null) {
            towerController.isPlaced = true;
        }

        //Stop holding the tower
        currentPlacingTower = null;

        if(towerInfoUI != null)
        {
            //Hide tower info
            towerInfoUI.SetActive(false);
        }
    }

    void DeselectTower()
    {
        if (towerInfoUI != null)
        {
            //Hide tower info
            towerInfoUI.SetActive(false);
        }

        if (currentPlacingTower == null)
        {
            return;
        }

        //Stop showing the placement grid
        gridPlane.SetActive(false);

        //Destroy tower gameObject
        Destroy(currentPlacingTower);

        currentPlacingTower = null;
    }

    public void SetTowerToPlace(GameObject tower)
    {
        TowerSelector.instance.DeselectPreviousTower();

        if(currentPlacingTower == null)
        {
            currentPlacingTower = Instantiate(tower, new Vector3(1000, 0, 0), Quaternion.identity);
        }
        else
        {
            Destroy(currentPlacingTower);
            currentPlacingTower = Instantiate(tower, new Vector3(1000, 0, 0), Quaternion.identity);
        }

        TowerController towerController = tower.GetComponent<TowerController>();

        if (towerController == null) {
            return;
        }

        if(towerInfoUI == null || towerInfoText == null)
        {
            return;
        }

        //Show tower info
        towerInfoText.text = towerController.GetTowerInfo();
        towerInfoUI.SetActive(true);
    }

    public bool IsPlacingTower()
    {
        return currentPlacingTower != null;
    }
}
