using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject pipePrefab;

    [Header("Timing")]
    [SerializeField] private float spawnDelay = 1f;
    [SerializeField] private float spawnInterval = 1.5f;

    [Header("Heights (Y)")]
    [SerializeField] private float[] heights = new float[] { -1f, -0.5f, 0f, 0.5f, 1f };

    private bool spawning = false;

    void OnEnable()
    {
        GameManager.OnGameStarted += StartSpawning;
        GameManager.OnGameOver += StopSpawning;
    }
    void OnDisable()
    {
        GameManager.OnGameStarted -= StartSpawning;
        GameManager.OnGameOver -= StopSpawning;
    }

    void Start()
    {
        if (!spawnPoint) spawnPoint = transform;
    }

    private void StartSpawning()
    {
        if (spawning) return;
        if (!pipePrefab) { Debug.LogError("[PipeSpawner] pipePrefab missing."); return; }
        if (heights == null || heights.Length == 0) { Debug.LogError("[PipeSpawner] heights empty"); return; }
        spawning = true;
        InvokeRepeating(nameof(Spawn), spawnDelay, Mathf.Max(0.1f, spawnInterval));
    }

    private void StopSpawning()
    {
        spawning = false;
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        int i = Random.Range(0, heights.Length);
        Vector3 pos = spawnPoint.position;
        pos.y = heights[i];
        Instantiate(pipePrefab, pos, Quaternion.identity);
    }
}
