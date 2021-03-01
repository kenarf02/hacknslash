using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class CircleIndicator : MonoBehaviour
{
    public Material mat;

    [Range(0.1f, 100f)]
    public float radius = 1.0f;

    [Range(3, 256)]
    public int numSegments = 128;


    public void DoRenderer()
    {
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        Color c1 = new Color(0.937f, 0.278f, 0.435f, 1);
        lineRenderer.material = mat;
        lineRenderer.SetColors(c1, c1);
        lineRenderer.SetWidth(0.05f, 0.05f);
        lineRenderer.SetVertexCount(numSegments + 1);
        lineRenderer.useWorldSpace = false;

        float deltaTheta = (float)(2.0 * Mathf.PI) / numSegments;
        float theta = 0f;

        for (int i = 0; i < numSegments + 1; i++)
        {
            float x = radius * Mathf.Cos(theta);
            float z = radius * Mathf.Sin(theta);
            Vector3 pos = new Vector3(x, 0, z);
            lineRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }
    }
}
