using UnityEngine;

public class WhiteBloodCell : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float lifetime;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.linearVelocity = Vector3.left * moveSpeed;
        
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, 0f, rotationSpeed * Time.fixedDeltaTime));
    }
}
