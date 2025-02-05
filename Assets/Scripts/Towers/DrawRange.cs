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

    private void Awake()
    {
        SetupLineRenderer();
    }

    private void Start()
    {
        radius = GetComponent<TargetingBehaviour>().range;

        //Draw the range of the tower
        DrawCircle();
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
        lineRenderer.material = new Material(Shader.Find("Transparent/Diffuse"));
        lineRenderer.material.color = UnityEngine.Color.blue;
        lineRenderer.startColor = UnityEngine.Color.blue;
        lineRenderer.endColor = UnityEngine.Color.blue;
        lineRenderer.startWidth = 0.15f;
        lineRenderer.endWidth = 0.15f;
        lineRenderer.positionCount = subdivisions;
        lineRenderer.enabled = true;
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
