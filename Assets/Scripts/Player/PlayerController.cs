using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Button tower1Button;
    [SerializeField] Button tower2Button;
    [SerializeField] Button tower3Button;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (tower1Button != null)
            {
                tower1Button.onClick.Invoke();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (tower2Button != null)
            {
                tower2Button.onClick.Invoke();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (tower3Button != null)
            {
                tower3Button.onClick.Invoke();
            }
        }
    }
}
