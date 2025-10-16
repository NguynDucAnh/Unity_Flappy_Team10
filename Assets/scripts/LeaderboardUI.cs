using UnityEngine;
using UnityEngine.UI;
using System.Text;
using TMPro;

public class LeaderboardUI : MonoBehaviour
{
    public LeaderboardManager leaderboardManager;
    public TMP_Text leaderboardText;

    void OnEnable()
    {
        UpdateLeaderboard();
    }

    public void UpdateLeaderboard()
    {
        if (leaderboardManager == null || leaderboardText == null)
        {
            Debug.LogWarning("LeaderboardUI: Missing reference!");
            return;
        }

        var entries = leaderboardManager.GetTopScores();
        Debug.Log($"[LeaderboardUI] Found {entries.Count} entries.");

        StringBuilder sb = new StringBuilder();

        // Giảm 1 dòng trống cho sát hơn
        sb.AppendLine("<align=center><size=24><b><color=#FFD700>TOP SCORES</color></b></size>");

        int rank = 1;
        foreach (var e in entries)
        {
            string color = rank switch
            {
                1 => "#FFD700",
                2 => "#C0C0C0",
                3 => "#CD7F32",
                _ => "#FFFFFF"
            };

            sb.AppendLine($"<size=20><b><color={color}>{rank}. {e.playerName}</color></b> " +
                          $"<color=#FFA500>{e.score}</color></size>");
            rank++;
        }

        if (entries.Count == 0)
            sb.AppendLine("<size=18><b><color=#00FF00>No scores yet!</color></b></size>");

        sb.AppendLine("</align>");
        leaderboardText.text = sb.ToString();
    }
}