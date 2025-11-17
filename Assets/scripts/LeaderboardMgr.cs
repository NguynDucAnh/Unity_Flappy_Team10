using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LeaderboardMgr : MonoBehaviour
{
    private static LeaderboardMgr _instance;
    public static LeaderboardMgr Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LeaderboardMgr>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("LeaderboardMgr");
                    _instance = go.AddComponent<LeaderboardMgr>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    private LeaderboardData leaderboard;
    private const string LEADERBOARD_KEY = "FlappyLeaderboard";

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLeaderboard();
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void LoadLeaderboard()
    {
        string json = PlayerPrefs.GetString(LEADERBOARD_KEY, "{}");
        leaderboard = JsonUtility.FromJson<LeaderboardData>(json);
        if (leaderboard == null || leaderboard.scores == null)
        {
            leaderboard = new LeaderboardData();
            leaderboard.scores = new List<LeaderboardEntry>();
        }
        SortLeaderboard();
    }

    private void SaveLeaderboard()
    {
        SortLeaderboard();
        // Chỉ giữ 10 điểm cao nhất
        if (leaderboard.scores.Count > 10)
        {
            leaderboard.scores = leaderboard.scores.GetRange(0, 10);
        }
        string json = JsonUtility.ToJson(leaderboard);
        PlayerPrefs.SetString(LEADERBOARD_KEY, json);
        PlayerPrefs.Save();
    }

    public void AddScore(string playerName, int score)
    {
        if (score <= 0) return; // Không lưu điểm 0

        leaderboard.scores.Add(new LeaderboardEntry { name = playerName, score = score });
        SaveLeaderboard();
    }

    public LeaderboardData GetLeaderboard()
    {
        return leaderboard;
    }

    private void SortLeaderboard()
    {
        leaderboard.scores = leaderboard.scores.OrderByDescending(s => s.score).ToList();
    }
}

[System.Serializable]
public class LeaderboardData
{
    public List<LeaderboardEntry> scores;
}

[System.Serializable]
public class LeaderboardEntry
{
    public string name;
    public int score;
}