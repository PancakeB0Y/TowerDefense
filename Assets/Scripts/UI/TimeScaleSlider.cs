using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeScaleSlider : MonoBehaviour
{
    [Header("Debug field")]
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI textMesh;

    void Start()
    {
        slider = GetComponentInChildren<Slider>();
        textMesh = GetComponentInChildren<TextMeshProUGUI>();

        if (slider != null)
        {
            slider.onValueChanged.AddListener((sliderValue) =>
            {
                textMesh.text = "TimeScale: " + sliderValue.ToString("0");
                Time.timeScale = sliderValue;
            });
        }
    }
}
