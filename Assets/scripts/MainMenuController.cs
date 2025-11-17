using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Panels")]
    [Tooltip("Panel chứa các nút Play, Rank, Rate")]
    public GameObject mainPanel; 
    [Tooltip("Panel hiển thị bảng xếp hạng")]
    public GameObject rankPanel;
    [Tooltip("Panel để người dùng đánh giá")]
    public GameObject ratePanel;

    [Header("Main Buttons")]
    [Tooltip("Nút Start/Play để chuyển sang Scene chọn map")]
    public Button startButton; // Đổi tên từ playButton
    [Tooltip("Nút để mở panel bảng xếp hạng")]
    public Button rankButton;
    [Tooltip("Nút để mở panel đánh giá")]
    public Button rateButton;

    [Header("Close Buttons")]
    [Tooltip("Nút 'X' hoặc 'Back' trên Rank Panel")]
    public Button closeRankButton;
    [Tooltip("Nút 'X' hoặc 'Back' trên Rate Panel")]
    public Button closeRateButton;

    // (Bạn có thể thêm các nút trong Rate Panel ở đây, ví dụ: 5 nút sao)

    void Start()
    {
        // Gán chức năng cho các nút bấm
        startButton.onClick.AddListener(GoToMapSelect);
        rankButton.onClick.AddListener(ShowRankPanel);
        rateButton.onClick.AddListener(ShowRatePanel);
        
        closeRankButton.onClick.AddListener(ShowMainPanel);
        closeRateButton.onClick.AddListener(ShowMainPanel);
        
        // Luôn bắt đầu bằng việc hiển thị Main Panel
        ShowMainPanel();
    }

    /// <summary>
    /// Được gọi bởi 'startButton' (start_btn).
    /// Chuyển đến Scene chọn map.
    /// </summary>
    public void GoToMapSelect()
    {
        // ✅ ĐÃ CẬP NHẬT: Tải Scene "UpdateMap" theo yêu cầu của bạn
        SceneManager.LoadScene("UpdateMap");
    }

    /// <summary>
    /// Được gọi bởi 'rankButton' (rank_btn).
    /// Hiển thị Panel bảng xếp hạng.
    /// </summary>
    public void ShowRankPanel()
    {
        mainPanel.SetActive(false);
        ratePanel.SetActive(false);
        rankPanel.SetActive(true);
        
        // Yêu cầu LeaderboardUI cập nhật lại danh sách điểm
        LeaderboardUI leaderboard = rankPanel.GetComponentInChildren<LeaderboardUI>();
        if (leaderboard != null)
        {
            leaderboard.ForceUpdate();
        }
    }

    /// <summary>
    /// Được gọi bởi 'rateButton' (rate_btn).
    /// Hiển thị Panel đánh giá.
    /// </summary>
    public void ShowRatePanel()
    {
        mainPanel.SetActive(false);
        rankPanel.SetActive(false);
        ratePanel.SetActive(true);
    }

    /// <summary>
    /// Được gọi bởi các nút 'Close' (X).
    /// Quay trở lại màn hình menu chính.
    /// </summary>
    public void ShowMainPanel()
    {
        ratePanel.SetActive(false);
        rankPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    // --- Chức năng cho Rate Panel ---
    // Bạn có thể thêm các hàm public ở đây và kết nối chúng
    // với các nút 1 sao, 2 sao, v.v. trong Unity Editor.
    
    // public void OnStarClicked(int starCount)
    // {
    //     Debug.Log($"Người dùng đã đánh giá {starCount} sao!");
    //     // Thêm logic gửi đánh giá ở đây...
    //     
    //     // Sau đó quay lại main menu
    //     ShowMainPanel();
    // }
}