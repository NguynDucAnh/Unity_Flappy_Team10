using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    private const string Key = "LeaderboardData";

    [System.Serializable]
    public class Entry
    {
        public string playerName;
        public int score;
    }

    [System.Serializable]
    public class LeaderboardData
    {
        public List<Entry> entries = new List<Entry>();
    }

    private LeaderboardData data = new LeaderboardData();

    [Header("UI References")]
    public TextMeshProUGUI leaderboardText;


    private void Awake()
    {
        Load();
        UpdateLeaderboardUI();
    }

    public void AddScore(string name, int score)
    {
        data.entries.Add(new Entry { playerName = name, score = score });

        data.entries = data.entries
            .OrderByDescending(e => e.score)
            .Take(5)
            .ToList();

        Save();
        UpdateLeaderboardUI();

        Debug.Log($"[LeaderboardManager] Added {name} - {score}. Total entries: {data.entries.Count}");
    }

    public List<Entry> GetTopScores() => data.entries;

    private void Save()
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(Key, json);
        PlayerPrefs.Save();
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey(Key))
        {
            string json = PlayerPrefs.GetString(Key);
            data = JsonUtility.FromJson<LeaderboardData>(json);
        }
        else
        {
            data = new LeaderboardData();
        }
    }

    public void ClearAll()
    {
        PlayerPrefs.DeleteKey(Key);
        data = new LeaderboardData();
        UpdateLeaderboardUI();
    }
    public void UpdateLeaderboardUI()
    {
        if (leaderboardText == null) return;

        // Giảm khoảng cách giữa tiêu đề và bảng xuống 1 dòng thay vì 2
        leaderboardText.text = "<align=center><size=24><b><color=#FFD700>TOP SCORES</color></b></size>\n";

        int rank = 1;

        foreach (var entry in data.entries)
        {
            string color = rank switch
            {
                1 => "#FFD700", // vàng
                2 => "#C0C0C0", // bạc
                3 => "#CD7F32", // đồng
                _ => "#FFFFFF"  // trắng
            };

            leaderboardText.text += $"<size=20><b><color={color}>{rank}. {entry.playerName}</color></b> " +
                                    $"<color=#FFA500>{entry.score}</color></size>\n";
            rank++;
        }

        if (data.entries.Count == 0)
            leaderboardText.text += "<size=18><b><color=#00FF00>No scores yet!</color></b></size>";

        leaderboardText.text += "</align>";
    }
}