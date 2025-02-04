using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class DrawRange : MonoBehaviour
{
    [SerializeField] bool renderRange = false;
    [SerializeField] int subdivisions = 20;
    LineRenderer lineRenderer;
    float radius;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        radius = GetComponent<TargetingBehaviour>().range;
        
        SetupLineRenderer();

        //Draw the range of the tower
        DrawCircle();
    }

    void Update()
    {
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
        lineRenderer.material = new Material(Shader.Find("Transparent/Diffuse"));
        lineRenderer.material.color = UnityEngine.Color.blue;
        lineRenderer.startColor = UnityEngine.Color.blue;
        lineRenderer.endColor = UnityEngine.Color.blue;
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
        lineRenderer.positionCount = subdivisions;
    }

    void DrawCircle()
    {
        float angleStep = 2f * Mathf.PI / 10;

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            float xPos = radius * Mathf.Cos(angleStep * i);
            float zPos = radius * Mathf.Sin(angleStep * i);

            Vector3 pointInCircle = new Vector3(xPos, transform.position.y, zPos);

            lineRenderer.SetPosition(i, pointInCircle + transform.position);
        }
    }

    public void RenderRange()
    {
        renderRange = true;

        DrawCircle();
    }

    public void DisableRange()
    {
        renderRange = false;
    }
}
