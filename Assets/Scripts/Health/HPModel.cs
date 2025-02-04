using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HPModel : MonoBehaviour
{
    [SerializeField] HPData hpData;
    [SerializeField] AbstractHPPresenter hpPresenter;

    public System.Action onDeath;
    private void Start()
    {
        hpPresenter = GetComponentInChildren<AbstractHPPresenter>();

        LoseHP(0);
    }

    public void LoseHP(float value)
    {
        if (hpData.currentHP > 0)
        {
            hpData.currentHP -= value;
            hpPresenter.PresentHP(hpData);

            if (hpData.currentHP <= 0)
            {
                onDeath?.Invoke();
            }
        }
    }    
}

//Serializable makes the class show up and be able to be modified in Unity Inspector 
[System.Serializable]
public class HPData
{
    public float maxHP;
    public float currentHP;

    public HPData(float pCurrentHP, float pMaxHP)
    {
        currentHP = pCurrentHP;
        maxHP = pMaxHP;
    }
}
