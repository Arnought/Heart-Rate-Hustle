using UnityEngine;
using System.Collections;

public class DroneDamageSystem : MonoBehaviour
{
    [Header("Damage Settings")]
    [SerializeField] private int bloodClotDamage = 3;
    [SerializeField] private int whiteBloodCellDamage = 1;
    private DroneHealthSystem droneHealth;
    private DroneMovement droneMovement;

    [Header("Speed Penalyty Settings")]
    [SerializeField] private float speedPenalty = 2f;
    [SerializeField] private float penaltyDuration = 1.5f;
    private bool isSlowed = false;

    [Header("Visual Feedback")]
    [SerializeField] private Renderer droneRenderer;
    [SerializeField] private float blinkDuration = 0.5f;
    [SerializeField] private int blinkCount = 4;
    [SerializeField] private Color blinkColor = new Color(1, 1, 1, 0.3f);

    private Material originalMaterial;
    private Color originalColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        droneHealth = GetComponent<DroneHealthSystem>();
        droneMovement = GetComponent<DroneMovement>();

        originalMaterial = droneRenderer.material;
        originalColor = originalMaterial.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BloodClot"))
        {
            droneHealth.Damage(bloodClotDamage);
            if (!isSlowed)
            {
                StartCoroutine(ApplySpeedPenalty());
            }
            StartCoroutine(BlinkEffect());
        }
        else if (other.CompareTag("WhiteBloodCell"))
        {
            droneHealth.Damage(whiteBloodCellDamage);
            if (!isSlowed)
            {
                StartCoroutine(ApplySpeedPenalty());
            }
            StartCoroutine(BlinkEffect());
            Destroy(other.gameObject);
        }
    }

    private IEnumerator ApplySpeedPenalty()
    {
        isSlowed = true;
        float originalSpeed = droneMovement.ForwardSpeed;
        droneMovement.ForwardSpeed = Mathf.Max(0, originalSpeed - speedPenalty);

        yield return new WaitForSeconds(speedPenalty);

        droneMovement.ForwardSpeed = originalSpeed;
        isSlowed = false;
    }

    private IEnumerator BlinkEffect()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            SetOpacity(blinkColor.a);
            yield return new WaitForSeconds(blinkDuration);

            SetOpacity(originalColor.a);
            yield return new WaitForSeconds(blinkDuration);
        }
    }

    private void SetOpacity(float alpha)
    {
        Color color = droneRenderer.material.color;
        color.a = alpha;
        droneRenderer.material.color = color;
    }

}
