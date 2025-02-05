using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveNumberPresenter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        PresentWaveNumber();
    }

    void PresentWaveNumber()
    {
        if (textMesh != null)
        {
            textMesh.text = "Wave: " + WaveManager.instance.currentWave + "/" + WaveManager.instance.numberOfWaves;
        }
    }
}
