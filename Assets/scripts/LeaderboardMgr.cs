using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LeaderboardMgr : MonoBehaviour
{
    public static LeaderboardMgr Instance;
    public List<(string playerName, int score)> leaderboard = new List<(string playerName, int score)>();

    public GameObject leaderboardPanel;
    public Text[] scoreTexts; // 5 dòng text
    private List<int> topScores = new List<int>();
    private const string PREF_KEY = "TopScores";

    void Start()
    {
        LoadScores();
        UpdateLeaderboardUI();
        leaderboardPanel.SetActive(false);
    }

    public void ShowLeaderboard()
    {
        LoadScores();
        UpdateLeaderboardUI();
        leaderboardPanel.SetActive(true);
    }

    public void HideLeaderboard()
    {
        leaderboardPanel.SetActive(false);
    }

    public void AddNewScore(int newScore)
    {
        LoadScores();
        topScores.Add(newScore);
        topScores.Sort((a, b) => b.CompareTo(a)); // Sắp xếp giảm dần

        if (topScores.Count > 5)
            topScores = topScores.GetRange(0, 5);

        SaveScores();
    }

    private void SaveScores()
    {
        string data = string.Join(",", topScores);
        PlayerPrefs.SetString(PREF_KEY, data);
        PlayerPrefs.Save();
    }

    private void LoadScores()
    {
        topScores.Clear();
        string data = PlayerPrefs.GetString(PREF_KEY, "");
        if (!string.IsNullOrEmpty(data))
        {
            string[] parts = data.Split(',');
            foreach (string p in parts)
            {
                if (int.TryParse(p, out int val))
                    topScores.Add(val);
            }
        }
    }

    public void UpdateLeaderboardUI()
    {
        for (int i = 0; i < scoreTexts.Length; i++)
        {
            if (i < topScores.Count)
                scoreTexts[i].text = $"{i + 1}. {topScores[i]}";
            else
                scoreTexts[i].text = $"{i + 1}. ---";
        }
    }
}
