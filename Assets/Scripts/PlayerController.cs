using UnityEngine;
using DevZhrssh.Singletons;

public class PlayerController : MonoBehaviour
{
    [Header("Player Properties")]
    [SerializeField] private float power;
    [SerializeField] private Vector2 minPower;
    [SerializeField] private Vector2 maxPower;

    // Drag and Shoot
    private Vector2 force;
    public Vector3 startPoint { get; private set; }
    public Vector3 endPoint { get; private set; }
    public Vector3 currentPoint { get; private set; }

    // Trajectory Line
    private TrajectoryLine trajectoryLine;

    // Time Control
    [Header("Time Control")]
    [SerializeField] private float timeSlowAmount = 0.5f;
    private TimeControl timeControl;

    // Body Stretch and Squash
    [Header("Stretch and Squash")]
    [SerializeField] private Vector3 stretchAmount;
    [SerializeField] private float speedThreshold;
    [SerializeField] private float speedOfScaling = 0.2f;
    private Vector3 originalScale;
    private float current, target;


    // System
    Camera cam;
    private Rigidbody2D rb;

    private void Start()
    {
        if (cam == null)
            cam = Camera.main;

        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        if (trajectoryLine == null)
            trajectoryLine = GetComponent<TrajectoryLine>();

        if (timeControl == null)
            timeControl = GetComponent<TimeControl>();

        originalScale = transform.localScale;
    }

    private void Update()
    {
        // Stretching of the ball
        HandleStretching();

        // Handle rotation when moving
        if (Input.touchCount <= 0)
        {
            Vector2 travelDirection = rb.velocity;
            HandleRotation(travelDirection);
        }

        if (Input.touchCount > 1) return;
        for (int i = 0; i < Input.touchCount; i++)
        {
            // On start touch
            if (Input.touches[i].phase == TouchPhase.Began)
            {
                OnStartTouch(Input.touches[0].position);
                timeControl.SlowTime(timeSlowAmount);
            }

            // On hold touch
            if (Input.touches[i].phase == TouchPhase.Moved)
            {
                OnHoldTouch(Input.touches[0].position);
            }

            // On end touch
            if (Input.touches[i].phase == TouchPhase.Ended)
            {
                OnEndTouch(Input.touches[0].position);
                timeControl.EndSlowTime();
            }
        }
    }

    private void OnStartTouch(Vector2 position)
    {
        startPoint = cam.ScreenToWorldPoint(new Vector3(position.x, position.y, 15));
    }

    private void OnHoldTouch(Vector2 position)
    {
        currentPoint = cam.ScreenToWorldPoint(new Vector3(position.x, position.y, 15));
        trajectoryLine.RenderLine(startPoint, currentPoint);

        // rotate towards the direction of movement
        HandleRotation(startPoint - currentPoint);
    }

    private void OnEndTouch(Vector2 position)
    {
        endPoint = cam.ScreenToWorldPoint(new Vector3(position.x, position.y, 15));

        force = new Vector2(
            Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x),
            Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y)
        );

        trajectoryLine.EndLine();

        // Play when making new move
        AudioManager.instance.Play("Move");
    }

    private void FixedUpdate()
    {
        if (force.magnitude > 0)
        {
            rb.velocity = force * power;
            force = Vector2.zero;
        }
    }

    private void HandleRotation(Vector2 direction)
    {
        Vector2 moveDirection = direction;
        moveDirection.Normalize();

        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, moveDirection);
        transform.rotation = toRotation;
    }

    private void HandleStretching()
    {
        // Handle scale when moving
        if (rb.velocity.magnitude > speedThreshold)
            target = 1;
        else
            target = 0;

        current = Mathf.MoveTowards(current, target, speedOfScaling * Time.deltaTime);
        transform.localScale = Vector3.Lerp(originalScale, stretchAmount, current);
    }
}
