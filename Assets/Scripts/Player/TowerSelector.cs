using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerSelector : MonoBehaviour
{
    public static TowerSelector instance { get; private set; }

    [Header("Collision Layers")]
    [SerializeField] LayerMask towerLayer;

    [Header("UI Elements")]
    [SerializeField] GameObject towerInfoUI;
    [SerializeField] GameObject towerUpgradeButtonUI;

    //Elements for detecting UI
    GraphicRaycaster graphicRaycaster;
    PointerEventData pointerEventData;
    EventSystem eventSystem;

    TextMeshProUGUI towerInfoText;
    TextMeshProUGUI towerUpgradeButtonText;
    Button towerUpgradeButton;

    Camera playerCamera;

    TowerController selectedTower;

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

        graphicRaycaster = GetComponentInChildren<GraphicRaycaster>();
        eventSystem = GetComponentInChildren<EventSystem>();
    }

    private void Start()
    {
        playerCamera = Camera.main;

        if (towerInfoUI != null)
        {
            towerInfoText = towerInfoUI.GetComponentInChildren<TextMeshProUGUI>();
        }

        if (towerUpgradeButtonUI != null)
        {
            towerUpgradeButtonText = towerUpgradeButtonUI.GetComponentInChildren<TextMeshProUGUI>();
            towerUpgradeButton = towerUpgradeButtonUI.GetComponentInChildren<Button>();
        }
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        if (TowerPlacement.instance.IsPlacingTower())
        {
            return;
        }

        if(graphicRaycaster == null)
        {
            return;
        }

        if (IsMouseOverUI())
        {
            return;
        }

        Ray cameraRay = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(cameraRay, out hitInfo, 100f, towerLayer, QueryTriggerInteraction.Ignore))
        {
            DeselectPreviousTower();

            if (hitInfo.collider == null) {
                return;
            }

            GameObject tower = hitInfo.collider.gameObject;

            if (tower == null)
            {
                return;
            }

            TowerController towerController = tower.GetComponent<TowerController>();

            if (towerController == null)
            {
                return;
            }

            if (towerController == selectedTower)
            {
                return;
            }

            SelectTower(towerController);
        }
        else
        {
            DeselectPreviousTower();
        }
    }

    bool IsMouseOverUI()
    {
        //Set up the new Pointer Event
        pointerEventData = new PointerEventData(eventSystem);
        //Set the Pointer Event Position to that of the mouse position
        pointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        graphicRaycaster.Raycast(pointerEventData, results);

        return results.Count > 0;
    }

    public void DeselectPreviousTower()
    {
        if (selectedTower == null)
        {
            return;
        }

        selectedTower.DeselectTower();
        selectedTower = null;

        if (towerUpgradeButtonUI == null || towerUpgradeButton == null || towerUpgradeButtonText == null)
        {
            return;
        }

        //Hide tower info and upgrade button
        towerInfoUI.SetActive(false);
        towerUpgradeButtonUI.SetActive(false);
        towerUpgradeButton.onClick.RemoveAllListeners();
    }

    public void SelectTower(TowerController tower)
    {
        selectedTower = tower;
        selectedTower.SelectTower();

        if (towerInfoUI == null || towerInfoText == null)
        {
            return;
        }

        //Show tower info
        towerInfoText.text = tower.GetTowerInfo();
        towerInfoUI.SetActive(true);

        //Check if the tower has a possible upgrade
        if (!tower.HasUpgrade() || towerUpgradeButtonUI == null || towerUpgradeButton == null || towerUpgradeButtonText == null)
        {
            return;
        }

        //Show upgrade button
        towerUpgradeButtonText.text = tower.upgradeCost.ToString();
        towerUpgradeButton.onClick.AddListener(delegate { tower.UpgradeTower(); });
        towerUpgradeButtonUI.SetActive(true);
    }
}
