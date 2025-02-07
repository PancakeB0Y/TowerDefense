using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoneyDrop : MonoBehaviour
{

    private void Update()
    {
        if(Time.timeScale == 0)
        {
            Destroy(gameObject);
        }    
    }

    public void ShowMoneyDrop()
    {
        StartCoroutine(ShowMoneyDropCoroutine());
    }

    IEnumerator ShowMoneyDropCoroutine()
    {
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);    
    }
}
