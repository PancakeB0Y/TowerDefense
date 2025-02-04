using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

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
        if (BaseController.instance != null)
        {
            BaseController.instance.onBaseDeath += GameOver;
        }
    }

    private void OnDisable()
    {
        if (BaseController.instance != null)
        {
            BaseController.instance.onBaseDeath -= GameOver;
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over");
    }
}
