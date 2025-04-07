using UnityEngine;
using UnityEngine.UI;

public class DroneHealthSystem : MonoBehaviour
{
    [Header("Health Settings")]
    public int currentHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private float damageSmoothSpeed = 5f;

    [Header("UI Elements")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private Gradient healthGradient;
    [SerializeField] private DeliveryStatus deliveryStatus;

    private void Start()
    {
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);

    }

    public void Update()
    {
        CurrentHealth();

        Debug.Log(Time.timeScale);
    }

    public void SetMaxHealth(int health)
    {
        healthBar.maxValue = health;
        healthBar.value = health;

        healthBarFill.color = healthGradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        healthBar.value = health;
        healthBarFill.color = healthGradient.Evaluate(healthBar.normalizedValue);
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
        SetHealth(currentHealth);
    }

    public void CurrentHealth()
    {
        if (currentHealth <= 0)
        {
            deliveryStatus.ShowFailureScreen();
            Time.timeScale = 1f;
        }
        
    }
}
