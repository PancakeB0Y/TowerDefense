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
    public int sellCost = 1;
    public int upgradeCost = 3;

    public bool isPlaced = false;

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

        UpdateTargets();

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

    public void UpdateTargets()
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
            "\n\n\nType: " + towerInfo.attackType +
            "\n\nTarget: " + towerInfo.targetingType;

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
            //Instantiate upgraded tower
            GameObject upgradedTower = Instantiate(towerInfo.upgradePrefab, transform.position, transform.rotation);
            TowerController upgradedTowerController = upgradedTower.GetComponent<TowerController>();
            upgradedTowerController.isPlaced = true;

            //Show the info for the new tower
            TowerSelector.instance.DeselectPreviousTower();
            TowerSelector.instance.SelectTower(upgradedTowerController);

            //Destroy the old tower
            Destroy(gameObject);
        }
    }

    public void SellTower()
    {
        TowerSelector.instance.DeselectPreviousTower();
        MoneyManager.instance.GainMoney(sellCost);
        Destroy(gameObject);
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
            //Create a copy of the materials and add the shader material
            List<Material> matArray = renderers[i].sharedMaterials.ToList();

            if (matArray[matArray.Count - 1] != outlineShaderMaterial)
            {
                matArray.Add(outlineShaderMaterial);
            }

            //Assign the new material list
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
                //Create a copy of the materials and remove the last material
                List<Material> matArray = renderers[i].materials.ToList();
                matArray.RemoveAt(matArray.Count - 1);

                Destroy(renderers[i].materials[renderers[i].materials.Count() - 1]);

                //Assign the new material list
                renderers[i].materials = matArray.ToArray();
            }
        }
    }
}
