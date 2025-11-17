using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public MapData CurrentMapData { get; private set; }

    public static event System.Action OnGameStarted;
    public static event System.Action OnGameOver;
    public static event System.Action OnScorePoint;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectMap(MapData mapData)
    {
        CurrentMapData = mapData;
        SceneManager.LoadScene("GameScene"); // Luôn tải 1 Scene game duy nhất
    }

    public void StartGame()
    {
        OnGameStarted?.Invoke();
    }

    public void EndGame()
    {
        OnGameOver?.Invoke();
    }

    public void AddScore()
    {
        OnScorePoint?.Invoke();
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}