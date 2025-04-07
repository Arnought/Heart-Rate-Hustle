using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Drone"))
        {
            other.GetComponent<OxygenTankSystem>().DeliverOxygen();
        }
    }
}
