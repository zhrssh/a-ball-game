using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Trajectory : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private void Start()
    {
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();
    }

    public void RenderLine(Vector3 startPoint, Vector3 endPoint)
    {
        lineRenderer.positionCount = 2;
        Vector3[] positions = new Vector3[2];
        positions[0] = startPoint;
        positions[1] = endPoint;

        lineRenderer.SetPositions(positions);
    }

    public void EndLine()
    {
        lineRenderer.positionCount = 0;
    }

}
