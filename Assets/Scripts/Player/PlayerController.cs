using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Button tower1Button;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            tower1Button.onClick.Invoke();
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            //tower2Button.onClick.Invoke();
        }

        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            //tower3Button.onClick.Invoke();
        }
    }
}
