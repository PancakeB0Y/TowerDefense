using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance { get; private set; }

    [SerializeField] AbstractMoneyPresenter moneyPresenter;

    [SerializeField] int startingMoney = 0;

    int currentMoney = 0;

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
            //DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        moneyPresenter = GetComponentInChildren<AbstractMoneyPresenter>();

        GainMoney(startingMoney);
    }

    public void GainMoney(int money)
    {
        currentMoney += money;
        moneyPresenter.PresentMoney(this.currentMoney);
    }

    public void LoseMoney(int money)
    {
        currentMoney -= money;
        moneyPresenter.PresentMoney(this.currentMoney);
    }

    public bool CanPurchaseTower(TowerController tower)
    {
        return tower.cost <= currentMoney;
    }

    public bool PurchaseTower(TowerController tower)
    {
        if (CanPurchaseTower(tower))
        {
            LoseMoney(tower.cost);
            return true;
        }

        return false;
    }
}
