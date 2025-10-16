using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ScoreEntry
{
    public string playerName;
    public int score;
}

[System.Serializable]
public class ScoreData
{
    public List<ScoreEntry> scores = new List<ScoreEntry>();
}

public class ScoreManager : MonoBehaviour
{
    private const string KEY = "LeaderboardData";
    private ScoreData scoreData;

    void Awake()
    {
        LoadScores();
    }

    public void AddScore(string playerName, int score)
    {
        scoreData.scores.Add(new ScoreEntry { playerName = playerName, score = score });
        scoreData.scores = scoreData.scores.OrderByDescending(s => s.score).Take(10).ToList();
        SaveScores();
    }

    public List<ScoreEntry> GetScores()
    {
        return scoreData.scores;
    }

    private void SaveScores()
    {
        string json = JsonUtility.ToJson(scoreData);
        PlayerPrefs.SetString(KEY, json);
        PlayerPrefs.Save();
    }

    private void LoadScores()
    {
        if (PlayerPrefs.HasKey(KEY))
        {
            string json = PlayerPrefs.GetString(KEY);
            scoreData = JsonUtility.FromJson<ScoreData>(json);
        }
        else
        {
            scoreData = new ScoreData();
        }
    }
}
