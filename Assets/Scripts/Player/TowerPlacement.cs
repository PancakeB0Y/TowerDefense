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
        if(currentPlacingTower == null || currentDrawRange == null)
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

        if (!Physics.CheckBox(currentPlacingTower.transform.position, halfExtends, Quaternion.identity, ObstacleLayer, QueryTriggerInteraction.Ignore))
        {
            if (Input.GetMouseButtonDown(0))
            {
                towerCollider.isTrigger = false;

                //Stop showing the range of the tower
                currentDrawRange.DisableRange();

                //Stop showing the placement grid
                gridPlane.SetActive(false);

                //Stop holding the tower
                currentPlacingTower = null;
            }
        }
        else
        {
            gridShaderMaterial.SetColor("_Color", unplacableGridColor);
        }


        //if (Input.GetMouseButtonDown(0)) {

        //    BoxCollider towerCollider = currentPlacingTower.gameObject.GetComponent<BoxCollider>();
        //    towerCollider.isTrigger = true;

        //    Vector3 boxCenter = currentPlacingTower.transform.position;
        //    Vector3 halfExtends = towerCollider.size / 2;

        //    if (!Physics.CheckBox(boxCenter, halfExtends, Quaternion.identity, TowerLayer, QueryTriggerInteraction.Ignore))
        //    {
        //        towerCollider.isTrigger = false;

        //        currentDrawRange.DisableRange();
        //        currentPlacingTower = null;
        //        gridPlane.SetActive(false);
        //    }
        //}
    }

    void OnDrawGizmos()
    {
        if(currentPlacingTower == null)
        {
            return;
        }

        BoxCollider towerCollider = currentPlacingTower.gameObject.GetComponent<BoxCollider>();

        Vector3 halfExtends = towerCollider.size / 2;

        Gizmos.matrix = currentPlacingTower.transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, towerCollider.size);
    }

    public void SetTowerToPlace(GameObject tower)
    {
        currentPlacingTower = Instantiate(tower, Vector3.zero, Quaternion.identity);
        currentDrawRange = currentPlacingTower.GetComponent<DrawRange>();
    }
}
