using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoneyDrop : MonoBehaviour
{


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
