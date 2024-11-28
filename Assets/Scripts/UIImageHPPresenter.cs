using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIImageHPPresenter : AbstractHPPresenter
{
    [SerializeField] Image hpBar;

    private void Start()
    {
        hpBar = GetComponent<Image>();
    }

    public override void PresentHP(HPData hpData)
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = (float)(hpData.currentHP) / (float)(hpData.maxHP);
        }
    }
}
