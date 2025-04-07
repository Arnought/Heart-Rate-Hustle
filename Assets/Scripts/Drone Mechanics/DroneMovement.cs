using UnityEngine;
using TMPro;

public class DroneMovement : MonoBehaviour
{
    public float ForwardSpeed
    {
        get { return forwardSpeed; }
        set { forwardSpeed = value; }
    }

    [Header("Configs")]

    [Header("Movement")]
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float upwardForce, maxUpwardSpeed;
    [SerializeField] private float downwardSpeed;
    [SerializeField] private float gravityScale;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float verticalSpeed, normalSpeed;
    [SerializeField] private float minYLimit, maxYLimit;

    [Header("Tilt")]
    [SerializeField] private float tiltAngle;
    [SerializeField] private float tiltSpeed;

    [Header("Heartbeat")]
    [SerializeField] private float heartbeatBoost;
    [SerializeField] private float heartbeatDuration;
    [SerializeField] private float minHeartbeatInterval, maxHeartbeatInterval;
    [SerializeField] private float speedReturnDuration;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI speedTxt;
    [SerializeField] private DroneHealthSystem healthSystem;

    [Header("Don't Touch")]
    [SerializeField] private float heartbeatEndTime;
    [SerializeField] private bool isHeartBeating = false;
    [SerializeField] private Quaternion normalRot, boostedRot;

    private void Start()
    {
        
        normalSpeed = forwardSpeed;
        normalRot = transform.rotation;
        rb = GetComponent<Rigidbody>();
        boostedRot = Quaternion.Euler(0f, 0f, tiltAngle);
        Heartbeat();
    }

    private void FixedUpdate()
    {
        //MoveForward();
    }

    private void Update()
    {
        MoveForward();
        PlayerInput();
        Gravity();
        CheckHeartbeatEnd();
        Tilt();
        ClampPosition();
        SpeedText();
    }

    public void MoveForward()
    {
        float currSpeed = forwardSpeed;
        rb.linearVelocity = new Vector3(currSpeed, rb.linearVelocity.y, 0f);

        if(healthSystem.currentHealth <= 0)
        {
            forwardSpeed = 0f;
            isHeartBeating = false;
        }
    }

    public void PlayerInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (rb.linearVelocity.y < maxUpwardSpeed)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, upwardForce, 0f);
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, -downwardSpeed, 0f);
        }
    }

    public void ClampPosition()
    {
        Vector3 clampedPos = rb.position;
        clampedPos.y = Mathf.Clamp(clampedPos.y, minYLimit, maxYLimit);
        rb.position = clampedPos;
    }

    public void Gravity()
    {
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector3.down * gravityScale, ForceMode.Acceleration);
        }
    }

    public void TriggerHeartbeat()
    {
        if (rb.position.y > minYLimit)
        {
            isHeartBeating = true;
            forwardSpeed += heartbeatBoost;
            heartbeatEndTime = Time.time + heartbeatDuration;
        }
        Heartbeat();
    }

    public void CheckHeartbeatEnd()
    {
        if (isHeartBeating && Time.time >= heartbeatEndTime)
        {
            StartCoroutine(ReturnToNormalSpeed());
            isHeartBeating = false;
        }
    }

    private System.Collections.IEnumerator ReturnToNormalSpeed()
    {
        float elapsedTime = 0f;
        float startSpeed = forwardSpeed;
        while (elapsedTime < speedReturnDuration)
        {
            forwardSpeed = Mathf.Lerp(startSpeed, normalSpeed, elapsedTime / speedReturnDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        forwardSpeed = normalSpeed;
    }

    public void Heartbeat()
    {
        float nextInterval = Random.Range(minHeartbeatInterval, maxHeartbeatInterval);
        Invoke("TriggerHeartbeat", nextInterval);
    }

    public void Tilt()
    {
        if (isHeartBeating && rb.position.y > minYLimit)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, boostedRot, Time.deltaTime * tiltSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, normalRot, Time.deltaTime * tiltSpeed);
        }
    }

    public void SpeedText()
    {
        if (speedTxt != null)
        {
            speedTxt.text = "Speed: " + Mathf.Abs(rb.linearVelocity.x).ToString("F2");
        }
    }

    
}
