using UnityEngine;
using System.Collections.Generic;

public class LeaderboardMgr : MonoBehaviour
{
    public static LeaderboardMgr Instance { get; private set; }

    [System.Serializable] public class Entry { public string name; public int score; }
    [System.Serializable] public class Board { public List<Entry> scores = new List<Entry>(); }

    private Board board = new Board();
    private const string KEY = "LEADERBOARD_JSON";

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadLeaderboard();
    }

    public Board GetLeaderboard() => board;

    public void AddScore(string name, int score)
    {
        board.scores.Add(new Entry { name = name, score = score });
        board.scores.Sort((a, b) => b.score.CompareTo(a.score));
        if (board.scores.Count > 10) board.scores.RemoveRange(10, board.scores.Count - 10);
        SaveLeaderboard();
    }

    private void LoadLeaderboard()
    {
        var json = PlayerPrefs.GetString(KEY, "");
        if (!string.IsNullOrEmpty(json)) board = JsonUtility.FromJson<Board>(json);
        if (board == null) board = new Board();
    }
    private void SaveLeaderboard()
    {
        PlayerPrefs.SetString(KEY, JsonUtility.ToJson(board));
        PlayerPrefs.Save();
    }
}
