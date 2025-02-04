using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class DrawRange : MonoBehaviour
{
    [SerializeField] bool renderRange = true;
    [SerializeField] int subdivisions = 20;
    LineRenderer lineRenderer;
    float radius;
    float yPos;

    void Awake()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Transparent/Diffuse"));
        lineRenderer.material.color = UnityEngine.Color.blue;
        lineRenderer.startColor = UnityEngine.Color.blue;
        lineRenderer.endColor = UnityEngine.Color.blue;
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
        lineRenderer.positionCount = subdivisions;
    }

    private void Start()
    {
        radius = GetComponent<TargetingBehaviour>().range;

        Mesh mesh = GetComponentInChildren<MeshFilter>().mesh;
        Bounds bounds = mesh.bounds;
        yPos = bounds.extents.y - bounds.size.y;

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

    void DrawCircle()
    {
        float angleStep = 2f * Mathf.PI / 10;

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            float xPos = radius * Mathf.Cos(angleStep * i);
            float zPos = radius * Mathf.Sin(angleStep * i);

            Vector3 pointInCircle = new Vector3(xPos, yPos, zPos);

            lineRenderer.SetPosition(i, pointInCircle + transform.position);
        }
    }
}
