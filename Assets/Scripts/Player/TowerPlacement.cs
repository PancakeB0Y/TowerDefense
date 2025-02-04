using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] LayerMask CheckMask;
    [SerializeField] LayerMask CollideMask;

    Camera playerCamera;
    GameObject currentPlacingTower;
    DrawRange currentDrawRange;

    void Start()
    {
        playerCamera = Camera.main;
    }

    void Update()
    {
        if(currentPlacingTower == null || currentDrawRange == null)
        {
            return;
        }

        Ray cameraRay = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if(Physics.Raycast(cameraRay, out hitInfo, 100f, CollideMask))
        {
            currentPlacingTower.transform.position = hitInfo.point;
            currentDrawRange.RenderRange();
        }

        if (Input.GetMouseButtonDown(0) && hitInfo.collider.gameObject != null) {

            //BoxCollider towerCollider = currentPlacingTower.gameObject.GetComponent<BoxCollider>();
            //towerCollider.isTrigger = true;

            //Vector3 boxCenter = towerCollider.center;
            //Vector3 halfExtends = towerCollider.size / 2;

            //if (!Physics.CheckBox(boxCenter, halfExtends, Quaternion.identity, CheckMask, QueryTriggerInteraction.Ignore))
            //{
            //    towerCollider.isTrigger = false;
            //    currentPlacingTower = null;
            //}

            currentDrawRange.DisableRange();
            currentPlacingTower = null;
        }
    }

    public void SetTowerToPlace(GameObject tower)
    {
        currentPlacingTower = Instantiate(tower, Vector3.zero, Quaternion.identity);
        currentDrawRange = currentPlacingTower.GetComponent<DrawRange>();
    }
}
