using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIImageHPPresenter : AbstractHPPresenter
{
    [SerializeField] Image hpBar;
    [SerializeField] TextMeshProUGUI textMesh;

    private void Awake()
    {
        hpBar = GetComponent<Image>();
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
    }

    public override void PresentHP(HPData hpData)
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = (float)(hpData.currentHP) / (float)(hpData.maxHP);
        }

        if (textMesh != null)
        {
            textMesh.text = hpData.currentHP + "/" + hpData.maxHP;
        }
    }
}
