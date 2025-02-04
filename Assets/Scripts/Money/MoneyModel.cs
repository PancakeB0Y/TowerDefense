using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyModel : MonoBehaviour
{
    public static MoneyModel instance { get; private set; }

    [SerializeField] AbstractMoneyPresenter moneyPresenter;

    [SerializeField] int startingMoney = 0;

    int money = 0;

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
    }

    private void Start()
    {
        moneyPresenter = GetComponentInChildren<AbstractMoneyPresenter>();

        GainMoney(startingMoney);
    }

    public void GainMoney(int money)
    {
        this.money += money;
        moneyPresenter.PresentMoney(this.money);
    }

    public void LoseMoney(int money)
    {
        this.money -= money;
        moneyPresenter.PresentMoney(this.money);
    }
}
