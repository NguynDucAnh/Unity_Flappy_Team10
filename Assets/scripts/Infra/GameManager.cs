using UnityEngine;
using UnityEngine.SceneManagement;
using Game.Infra;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public MapId LastSelectedMap { get; private set; } = MapId.Winter;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // ---- Menu flow ----
    public void LoadMapSelect() => SceneManager.LoadScene(SceneNames.UpdateMap);

    public void SelectMap(MapId map)
    {
        LastSelectedMap = map;
        LoadSelectedMap();
    }

    public void LoadSelectedMap()
    {
        string scene =
            LastSelectedMap == MapId.Winter ? SceneNames.Game_Winter :
            LastSelectedMap == MapId.MushroomNight ? SceneNames.Game_MushroomNight :
            SceneNames.Game_Lava;

        SceneManager.LoadScene(scene);
    }

    public void LoadStart() => SceneManager.LoadScene(SceneNames.Start);
}
