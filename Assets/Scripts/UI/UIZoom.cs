using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIZoom : MonoBehaviour
{
    public float zoomSize;
    public float zoomDuration;

    private Vector3 originalSize;

    private void Start()
    {
        originalSize = transform.localScale;
    }

    public IEnumerator StartZoom()
    {
        float zoomTime = zoomDuration;
        while (zoomTime > 0)
        {
            float percent = zoomTime / zoomDuration;
            transform.localScale = Vector3.Lerp(originalSize, transform.localScale * zoomSize, percent);
            zoomTime = zoomTime - 0.1f;
            yield return null;
        }
    }
}
