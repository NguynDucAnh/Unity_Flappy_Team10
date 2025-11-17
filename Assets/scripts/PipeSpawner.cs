using UnityEngine;
using System.Collections;

public class PipeSpawner : MonoBehaviour {

    public float spawnDelay = 3f;		
    public float[] heights;

    // ❌ XÓA: public GameObject pipe;	
    // ❌ XÓA: public float spawnTime = 5f;
    private MapData currentMap;

    void Awake()
    {
        // Lấy dữ liệu map và đăng ký sự kiện
        currentMap = GameManager.Instance.CurrentMapData;
        GameManager.OnGameStarted += StartSpawning;
        GameManager.OnGameOver += StopSpawning;
    }

    void OnDestroy()
    {
        // Hủy đăng ký sự kiện để tránh lỗi
        GameManager.OnGameStarted -= StartSpawning;
        GameManager.OnGameOver -= StopSpawning;
    }

    public void StartSpawning()
    {
        // Dùng spawn rate từ MapData
        InvokeRepeating("Spawn", spawnDelay, currentMap.pipeSpawnRate);
    }

    void Spawn ()
    {
        int heightIndex = Random.Range(0, heights.Length);
        Vector2 pos = new Vector2(transform.position.x, heights[heightIndex]);

        // ✅ TẠO ĐÚNG ỐNG NƯỚC:
        Instantiate(currentMap.pipePrefab, pos, transform.rotation);
    }

    public void StopSpawning() // Đổi tên từ GameOver()
    {
        CancelInvoke("Spawn");
    }
}