using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBallMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float multiplier;
    [SerializeField] private float limit = 1f;

    private bool isSelected = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected == true)
        {
            float value = (Mathf.Sin(Time.time) * multiplier / 1000f);
            float transpose = Mathf.Clamp(value, -limit, limit);
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + transpose, transform.position.z);
            rectTransform.position = pos;
        }
    }

    public void SetSelected(bool b)
    {
        isSelected = b;
    }
}
