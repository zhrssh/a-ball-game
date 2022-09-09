using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBallMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float multiplier;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + (Mathf.Sin(Time.time) * multiplier / 1000f), transform.position.z);
        rectTransform.position = pos;
    }
}
