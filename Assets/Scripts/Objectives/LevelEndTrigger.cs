using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    [SerializeField] private WhiteBloodCellSpawner spawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Drone"))
        {
            other.GetComponent<OxygenTankSystem>().CheckLevelEnd();

            spawner.StopSpawning();
        }
    }
}
