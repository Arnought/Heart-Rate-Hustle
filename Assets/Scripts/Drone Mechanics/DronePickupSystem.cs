using UnityEngine;
using TMPro;

public class DronePickupSystem : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI pickupTxt;

    [Header("Pickup Settings")]
    [SerializeField] private string pickupTag = "BloodCell";
    private int pickupCount = 0;
    [SerializeField] private OxygenTankSystem oxygenTank;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdatePickupUI();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag(pickupTag))
        {
            oxygenTank.AddOxygen(1);
            CollectPickup(collision.gameObject);
        }
    }

    private void CollectPickup(GameObject pickup)
    {
        pickupCount++;
        UpdatePickupUI();
        Destroy(pickup);
    }

    private void UpdatePickupUI()
    {
        if(pickupTxt != null)
        {
            pickupTxt.text = "" + pickupCount;
        }
    }
}
