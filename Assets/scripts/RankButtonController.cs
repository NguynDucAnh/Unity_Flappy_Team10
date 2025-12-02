using UnityEngine;

public class RankButtonController : MonoBehaviour


{
    [Header("Leaderboard Panel (kéo vô đây)")]
    public GameObject leaderboardPanel;

    private bool isOpen = false;

    private void Start()
    {
        // Ẩn bảng khi bắt đầu
        if (leaderboardPanel != null)
            leaderboardPanel.SetActive(false);
    }

   
   public void ToggleLeaderboard()
{
    if (leaderboardPanel != null)
    {
        bool isActive = leaderboardPanel.activeSelf;
        leaderboardPanel.SetActive(!isActive);
        Debug.Log("📊 Leaderboard " + (!isActive ? "Opened" : "Closed"));
    }
}


    // 🟢 Hàm này sẽ được gọi từ LeaderboardUI khi Back
    public void ResetState()
    {
        isOpen = false;
        Debug.Log("🔄 RankButtonController state reset!");
    }
}

