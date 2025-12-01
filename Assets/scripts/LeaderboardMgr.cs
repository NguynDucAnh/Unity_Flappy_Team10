using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq; 

public class LeaderboardMgr : MonoBehaviour
{
    public static LeaderboardMgr Instance;
    
    [Header("Cài đặt UI")]
    public GameObject leaderboardPanel; // Kéo Panel Rank vào đây
    public Text[] scoreTexts; // Kéo 5 text điểm vào đây (nếu dùng Text thường)

    private List<int> topScores = new List<int>();
    private const string PREF_KEY = "Flappy_BestScores";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ script này sống qua các scene
            LoadScores();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowLeaderboard()
    {
        if (leaderboardPanel != null)
        {
            leaderboardPanel.SetActive(true);
            UpdateUI();
        }
    }

    public void HideLeaderboard()
    {
        if (leaderboardPanel != null) leaderboardPanel.SetActive(false);
    }

    // Hàm thêm điểm mới (Sửa lỗi CS1061)
    public void AddScore(int newScore)
    {
        if (newScore <= 0) return;

        LoadScores();
        topScores.Add(newScore);
        
        // Sắp xếp giảm dần (điểm cao nhất lên đầu)
        topScores = topScores.OrderByDescending(x => x).ToList();

        // Chỉ giữ top 5
        if (topScores.Count > 5)
            topScores = topScores.Take(5).ToList();

        SaveScores();
        UpdateUI();
    }

    // Hàm hỗ trợ gọi từ các script cũ
    public void AddScore(string playerName, int score)
    {
        AddScore(score);
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

    private void UpdateUI()
    {
        if (scoreTexts == null) return;

        for (int i = 0; i < scoreTexts.Length; i++)
        {
            if (scoreTexts[i] != null)
            {
                if (i < topScores.Count)
                    scoreTexts[i].text = topScores[i].ToString();
                else
                    scoreTexts[i].text = "---";
            }
        }
    }
}