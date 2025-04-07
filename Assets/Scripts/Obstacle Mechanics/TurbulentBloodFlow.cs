using UnityEngine;

public class TurbulentBloodFlow : MonoBehaviour
{
    [Header("Blood Flow Settings")]
    [SerializeField] private float turbulenceStrength;
    [SerializeField] private float turbulenceDuration;
    [SerializeField] private float minTurbulenceInterval;
    [SerializeField] private float maxTurbulenceInterval;

    [Header("Drone")]
    [SerializeField] private DroneHealthSystem healthSystem;

    private Rigidbody rb;
    private Vector3 turbulenceDirection;
    private float turbulenceEndTime;
    private bool isTurbulenceActive = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = FindAnyObjectByType<DroneMovement>().GetComponent<Rigidbody>();
        InvokeTurbulence();
    }

    // Update is called once per frame
    void Update()
    {
        ApplyTurbulence();
    }

    private void InvokeTurbulence()
    {
        float nextInterval = Random.Range(minTurbulenceInterval, maxTurbulenceInterval);
        Invoke("TriggerTurbulence", nextInterval);
    }

    private void TriggerTurbulence()
    {
        turbulenceDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        isTurbulenceActive = true;
        turbulenceEndTime = Time.time + turbulenceDuration;
        InvokeTurbulence();
    }

    private void ApplyTurbulence()
    {
        if (isTurbulenceActive)
        {
            rb.AddForce(turbulenceDirection * turbulenceStrength, ForceMode.Acceleration);

            if(Time.time >= turbulenceEndTime)
            {
                isTurbulenceActive = false;
            }
        }

        if(healthSystem.currentHealth <= 0)
        {
            isTurbulenceActive = false;
        }
    }
}
