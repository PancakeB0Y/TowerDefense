using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HPModel : MonoBehaviour
{
    [SerializeField] HPData hpData;
    [SerializeField] AbstractHPPresenter hpPresenter;

    public static System.Action<GameObject> onDeath;
    private void Start()
    {
        hpPresenter = GetComponentInChildren<AbstractHPPresenter>();

        ChangeHP(0);
    }

    private void OnEnable()
    {
        EnemyController.onHit += ChangeHP;
    }

    private void OnDisable()
    {
        EnemyController.onHit -= ChangeHP;
    }

    public void ChangeHP(float value)
    {
        if (hpData.currentHP > 0)
        {
            hpData.currentHP -= value;
            if (hpData.currentHP == 0)
            {
                onDeath?.Invoke(gameObject);
            }
            else
            {
                hpPresenter.PresentHP(hpData);
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
