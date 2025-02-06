using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] Tower towerInfo;

    [SerializeField] Material outlineShaderMaterial;

    [Header("Behaviours")]
    [SerializeField] AttackBehaviour attackBehaviour;
    [SerializeField] TargetingBehaviour targetingBehaviour;
    [SerializeField] DrawRange towerRangeVisual;

    [Header("Properties")]
    public int cost = 1;
    public int upgradeCost = 3;

    [HideInInspector] public bool isPlaced = false;

    public System.Action onAttack;

    List<Renderer> renderers;

    void Awake()
    {
        attackBehaviour = GetComponent<AttackBehaviour>();
        targetingBehaviour = GetComponent<TargetingBehaviour>();
        towerRangeVisual = GetComponent<DrawRange>();

        renderers = GetComponentsInChildren<Renderer>().ToList();
    }

    private void FixedUpdate()
    {
        if (!isPlaced) {
            return;
        }

        GetTargets();

        for (int i = targetingBehaviour.targets.Count - 1; i >= 0; i--)
        {
            Attack(targetingBehaviour.targets[i]);
        }
    }

    public void Attack(GameObject target)
    {
        if (attackBehaviour != null)
        {
            attackBehaviour.Attack(target);
        }
    }

    public void GetTargets()
    {
        if (targetingBehaviour != null)
        {
            targetingBehaviour.GetTargets();
        }
    }

    public string GetTowerInfo()
    {
        if (towerInfo == null)
        {
            return "Missing Tower Info";
        }

        if(attackBehaviour == null)
        {
            attackBehaviour = GetComponent<AttackBehaviour>();
        }

        if (targetingBehaviour == null)
        {
            targetingBehaviour = GetComponent<TargetingBehaviour>();
        }

        float attackSpeedF = 1 / attackBehaviour.GetAttackSpeed();
        string attackSpeed = String.Format("{0:0.#}", attackSpeedF);

        string stringInfo = 
            towerInfo.towerName 
            + "\n----------------\n\n" +
            "Attack: " + attackBehaviour.GetAttack() +
            "\nAttack Speed: " + attackSpeed + "/s" +
            "\nRange: " + targetingBehaviour.GetRange() +
            "\nAttack Type: " + towerInfo.attackType +
            "\nTargeting Type: " + towerInfo.targetingType;

        return stringInfo;
    }

    public bool HasUpgrade()
    {
        return towerInfo.upgradePrefab != null;
    }

    public void UpgradeTower()
    {
        if (MoneyManager.instance.PurchaseTowerUpgrade(this))
        {
            GameObject upgradedTower = Instantiate(towerInfo.upgradePrefab, transform.position, transform.rotation);
            TowerController upgradedTowerController = upgradedTower.GetComponent<TowerController>();
            upgradedTowerController.isPlaced = true;
            TowerSelector.instance.DeselectPreviousTower();
            TowerSelector.instance.SelectTower(upgradedTowerController);

            Destroy(gameObject);
        }
    }

    //Tower selection functions
    public void SelectTower()
    {
        ApplyOutlineShader();
        ShowTowerRange();
    }

    public void DeselectTower()
    {
        RemoveOutlineShader();
        HideTowerRange();
    }

    public void ShowTowerRange()
    {
        if(towerRangeVisual == null || targetingBehaviour == null)
        {
            return;
        }

        towerRangeVisual.RenderRange(targetingBehaviour.GetRange());
    }

    public void HideTowerRange() 
    {
        if (towerRangeVisual == null)
        {
            return;
        }

        towerRangeVisual.DisableRange();
    }

    void ApplyOutlineShader()
    {
        if (outlineShaderMaterial == null)
        {
            return;
        }

        for (int i = 0; i < renderers.Count; i++)
        {
            List<Material> matArray = renderers[i].materials.ToList();
            if (matArray.Count == 1)
            {
                matArray.Add(outlineShaderMaterial);
            }
            else if (matArray.Count > 1)
            {
                matArray[1] = outlineShaderMaterial;
            }

            renderers[i].materials = matArray.ToArray();
        }
    }

    void RemoveOutlineShader()
    {
        if (outlineShaderMaterial == null)
        {
            return;
        }

        for (int i = 0; i < renderers.Count; i++)
        {
            if (renderers[i].materials.Count() > 1)
            {
                Destroy(renderers[i].materials[1]);
            }
        }
    }


}
