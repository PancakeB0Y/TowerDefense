using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [Header("Collision Layers")]
    [SerializeField] LayerMask ObstacleLayer;
    [SerializeField] LayerMask GroundLayer;

    [Header("Grid Objects")]
    [SerializeField] Grid grid;
    [SerializeField] GameObject gridPlane;

    [Header("Grid Properties")]
    [SerializeField] Color gridColor = Color.white;
    [SerializeField] Color unplacableGridColor = Color.red;

    Material gridShaderMaterial;

    Camera playerCamera;

    GameObject currentPlacingTower;
    DrawRange currentDrawRange;

    void Start()
    {
        playerCamera = Camera.main;

        if (gridPlane != null)
        {
            gridShaderMaterial = gridPlane.GetComponent<Renderer>().material;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            DeselectTower();
        }

        if (currentPlacingTower == null || currentDrawRange == null)
        {
            return;
        }

        gridPlane.SetActive(true);
        gridShaderMaterial.SetColor("_Color", gridColor);

        Ray cameraRay = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if(Physics.Raycast(cameraRay, out hitInfo, 100f, GroundLayer))
        {
            //Map tower position to grid position
            Vector3Int gridPosition = grid.WorldToCell(hitInfo.point);
            Vector3 worldPosition = grid.CellToWorld(gridPosition);
            worldPosition = new Vector3(worldPosition.x + 0.6f, worldPosition.y, worldPosition.z + 0.6f);
            currentPlacingTower.transform.position = worldPosition;

            currentDrawRange.RenderRange();
        }

        BoxCollider towerCollider = currentPlacingTower.gameObject.GetComponent<BoxCollider>();
        Vector3 halfExtends = towerCollider.size / 2;
        towerCollider.isTrigger = true;

        if (Physics.CheckBox(currentPlacingTower.transform.position, halfExtends, Quaternion.identity, ObstacleLayer, QueryTriggerInteraction.Ignore))
        {
            gridShaderMaterial.SetColor("_Color", unplacableGridColor);
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            TowerController currentTowerController = currentPlacingTower.GetComponent<TowerController>();

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
        currentDrawRange.DisableRange();

        //Stop showing the placement grid
        gridPlane.SetActive(false);

        //Set state of the tower
        if (towerController != null) {
            towerController.isPlaced = true;
        }

        //Stop holding the tower
        currentPlacingTower = null;
    }

    void DeselectTower()
    {
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
        if(currentPlacingTower == null)
        {
            currentPlacingTower = Instantiate(tower, new Vector3(1000, 0, 0), Quaternion.identity);
            currentDrawRange = currentPlacingTower.GetComponent<DrawRange>();
        }
    }
}
