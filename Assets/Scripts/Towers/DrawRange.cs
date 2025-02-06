using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class DrawRange : MonoBehaviour
{
    [SerializeField] Material lineRendererMaterial;

    [Header("Parameters")]
    [SerializeField] bool renderRange = false;
    [SerializeField] int subdivisions = 20;

    LineRenderer lineRenderer;

    private void Awake()
    {
        SetupLineRenderer();
    }

    void Update()
    {
        if(lineRenderer == null)
        {
            return;
        }

        if (renderRange)
        {
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    //Set up the parameters of the line renderer
    void SetupLineRenderer()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = lineRendererMaterial;
        lineRenderer.material.color = UnityEngine.Color.blue;
        lineRenderer.startColor = UnityEngine.Color.blue;
        lineRenderer.endColor = UnityEngine.Color.blue;
        lineRenderer.startWidth = 0.15f;
        lineRenderer.endWidth = 0.15f;
        lineRenderer.positionCount = subdivisions;
        lineRenderer.enabled = true;
    }

    void DrawCircle(float range)
    {
        if(lineRenderer == null)
        {
            return;
        }

        range *= 0.9f;

        float angleStep = 2f * Mathf.PI / (subdivisions - 1);

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            float xPos = range * Mathf.Cos(angleStep * i);
            float zPos = range * Mathf.Sin(angleStep * i);

            Vector3 pointInCircle = new Vector3(xPos, transform.position.y, zPos);

            lineRenderer.SetPosition(i, pointInCircle + transform.position);
        }
    }

    public void RenderRange(float range)
    {
        renderRange = true;

        DrawCircle(range);
    }

    public void DisableRange()
    {
        renderRange = false;
    }
}
