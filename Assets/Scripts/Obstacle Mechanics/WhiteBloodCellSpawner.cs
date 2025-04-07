using UnityEngine;

public class WhiteBloodCellSpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnPoint
    {
        public Transform spawnTransform;
        public float spawnRate;
    }

    [Header("Spawn Settings")]
    [SerializeField] private GameObject whiteBloodCellPrefab;
    [SerializeField] private SpawnPoint[] spawnPoints;
    [SerializeField] private float minSpawnDelay, maxSpawnDelay;

    private bool spawningActive = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartSpawning();
    }

    private void StartSpawning()
    {
        Invoke("SpawnWhiteBloodCell", Random.Range(minSpawnDelay, maxSpawnDelay));
    }

    private void SpawnWhiteBloodCell()
    {
        if (!spawningActive) return;

        if (spawnPoints.Length == 0 || whiteBloodCellPrefab == null) return;

        SpawnPoint selectedSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(whiteBloodCellPrefab, selectedSpawn.spawnTransform.position, Quaternion.identity);

        Invoke("SpawnWhiteBloodCell", Random.Range(minSpawnDelay, maxSpawnDelay));
    }

    public void StopSpawning()
    {
        spawningActive = false;
        CancelInvoke("SpawnWhiteBloodCell");
    }
}
