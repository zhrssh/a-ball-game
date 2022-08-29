using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRandomizer : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float force = 1f;

    private float time;
    private bool addForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Quaternion direction = GetRandDirection();

        transform.rotation = direction;
        rb.AddForce(transform.up * force, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 5f && time != 0)
        {
            Quaternion direction = GetRandDirection();

            transform.rotation = direction;

            addForce = true;
        }
    }

    private void FixedUpdate()
    {
        if (addForce == true)
        {
            time = 0;
            addForce = false;
            rb.velocity = Vector2.zero;
            rb.AddForce(transform.up * force, ForceMode2D.Impulse);
        }
    }

    private Quaternion GetRandDirection()
    {
        Quaternion toRotation = Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f));
        toRotation = Quaternion.RotateTowards(transform.rotation, toRotation, 360.0f);
        return toRotation;
    }
}
