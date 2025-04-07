using UnityEngine;

public class BloodClotObstacle : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveDistance;
    [SerializeField] private float rotationSpeed;

    private Vector3 startPosition;
    private Rigidbody rb;
    private int direction = 1;
    private float randomStartOffset;
    private float elapsedTime;

    private float randomRotationMultiplier;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;

        randomStartOffset = Random.Range(0f, 1.5f);

        randomRotationMultiplier = Random.Range(0.8f, 1.2f) * (Random.value > 0.5f ? 1 : -1);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime = Time.deltaTime;

        if (elapsedTime < randomStartOffset) return;

        
    }

    private void FixedUpdate()
    {
        float newY = rb.position.y + direction * moveSpeed * Time.deltaTime;

        if (Mathf.Abs(newY - startPosition.y) >= moveDistance)
        {
            direction *= -1;
        }

        rb.position = new Vector3(rb.position.x, newY, rb.position.z);


        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, 0f, rotationSpeed * randomRotationMultiplier * Time.fixedDeltaTime));
    }
}
