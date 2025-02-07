using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextMeshMoneyPresenter : AbstractMoneyPresenter
{
    [SerializeField] TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    public override void PresentMoney(int money)
    {
        if (textMesh != null)
        {
            textMesh.text = "Money: $" + money.ToString();
        }
    }
}
