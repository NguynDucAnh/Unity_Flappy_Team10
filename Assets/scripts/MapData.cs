using UnityEngine;

// Cho phép tạo file Map Data từ menu Assets -> Create -> FlappyBird/Map Data
[CreateAssetMenu(fileName = "Map_", menuName = "FlappyBird/Map Data")]
public class MapData : ScriptableObject
{
    [Header("Map Info")]
    public string mapName = "New Map";
    public Sprite mapThumbnail; // Ảnh hiển thị ở màn hình chọn map

    [Header("Game Assets (Kéo Prefab vào đây)")]
    public GameObject pipePrefab; // PREFAB ống nước cho map này
    public GameObject backgroundPrefab; // PREFAB background cho map này
    public GameObject landPrefab; // PREFAB mặt đất cho map này
    public AudioClip scoreSound;
    public AudioClip hitSound;
    public AudioClip jumpSound;

    [Header("Game Settings")]
    public float scrollSpeed = 5f;
    public float pipeSpawnRate = 3f;
}