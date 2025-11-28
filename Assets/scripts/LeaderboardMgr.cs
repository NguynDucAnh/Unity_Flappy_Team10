using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LeaderboardMgr : MonoBehaviour
{
    public static LeaderboardMgr Instance;
    public List<(string playerName, int score)> leaderboard = new List<(string playerName, int score)>();

    public GameObject leaderboardPanel;
    public LeaderboardUI ui;

    // Dữ liệu
    public List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLeaderboard();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        if (ui == null)
            ui = FindObjectOfType<LeaderboardUI>();

        if (leaderboardPanel != null)
            leaderboardPanel.SetActive(false);
        else
            Debug.LogWarning("Leaderboard Panel chưa được gán!");
    }

    public void ShowLeaderboard()
    {
        if (leaderboardPanel != null)
            leaderboardPanel.SetActive(true);

        if (ui != null)
            ui.ForceUpdate();
        else
            Debug.LogWarning("Leaderboard UI chưa được gán!");
    }

    public void HideLeaderboard()
    {
        if (leaderboardPanel != null)
            leaderboardPanel.SetActive(false);
    }

    public void AddScore(string playerName, int score)
    {
        leaderboard.Add(new LeaderboardEntry(playerName, score));

        // Sắp xếp giảm dần theo điểm
        leaderboard.Sort((a, b) => b.score.CompareTo(a.score));

        // Giới hạn số lượng
        if (leaderboard.Count > maxEntries)
            leaderboard = leaderboard.GetRange(0, maxEntries);

        SaveLeaderboard();

        // Cập nhật UI nếu nó đang mở
        if (ui != null && leaderboardPanel.activeSelf)
            ui.ForceUpdate();
    }

    private void SaveLeaderboard()
    {
        LeaderboardData data = new LeaderboardData(leaderboard);
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(PREF_KEY, json);
        PlayerPrefs.Save();
        Debug.Log("✅ Leaderboard saved!");
    }

    private void LoadLeaderboard()
    {
        string json = PlayerPrefs.GetString(PREF_KEY, "");
        if (!string.IsNullOrEmpty(json))
        {
            LeaderboardData data = JsonUtility.FromJson<LeaderboardData>(json);
            leaderboard = data.entries;
            Debug.Log($"✅ Leaderboard loaded with {leaderboard.Count} entries.");
        }
        else
        {
            Debug.Log("No leaderboard data found. Initializing new list.");
            leaderboard = new List<LeaderboardEntry>();
        }
    }

    // Hàm reset (để test)
    [ContextMenu("Reset Leaderboard")]
    public void ResetLeaderboard()
    {
        leaderboard.Clear();
        PlayerPrefs.DeleteKey(PREF_KEY);
        SaveLeaderboard();
        if (ui != null) ui.ForceUpdate();
        Debug.Log("Reset Leaderboard!");
    }
}

// Lớp để lưu trữ
[System.Serializable]
public class LeaderboardEntry
{
    public string playerName;
    public int score;

    public LeaderboardEntry(string name, int s)
    {
        playerName = name;
        score = s;
    }
}

// Lớp wrapper để JsonUtility có thể
[System.Serializable]
public class LeaderboardData
{
    public List<LeaderboardEntry> entries;
    public LeaderboardData(List<LeaderboardEntry> list)
    {
        entries = list;
    }
}