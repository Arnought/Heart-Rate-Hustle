using TMPro;
using UnityEngine;

public class OxygenTankSystem : MonoBehaviour
{
    [Header("Oxygen Settings")]
    [SerializeField] private int currentOxygen = 0;
    [SerializeField] private int totalDelivered = 0;
    [SerializeField] private int requiredToDeliver = 2;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI carryingText;
    [SerializeField] private TextMeshProUGUI deliveredText;
    [SerializeField] private DeliveryStatus deliveryStatus;

    private void Update()
    {
        UpdateUI();
    }

    public void AddOxygen(int amount)
    {
        currentOxygen += amount;
        UpdateUI();
    }

    public void DeliverOxygen()
    {
        totalDelivered += currentOxygen;
        currentOxygen = 0;
        UpdateUI();
    }

    public void CheckLevelEnd()
    {
        if(totalDelivered < requiredToDeliver)
        {
            Debug.Log("Delivery Failed!");
            deliveryStatus.ShowFailureScreen();
        }
        else
        {
            Debug.Log("Delivery Success!");
            deliveryStatus.ShowSuccessScreen();
        }
    }

    public void UpdateUI()
    {
        if(carryingText != null)
        {
            carryingText.text = currentOxygen + "";
        }
        if(deliveredText != null)
        {
            deliveredText.text = totalDelivered  + " / " + requiredToDeliver;
        }
    }
}
