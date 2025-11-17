using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro; // Sử dụng TextMeshPro

public class GameOverPanel : MonoBehaviour
{
    [Header("UI References")]
    public GameObject panelRoot; // Kéo GameObject "GameOverPanel" vào đây
    public TextMeshProUGUI scoreText; // Kéo TextMeshPro "ScoreText" con của panel vào
    public TextMeshProUGUI bestScoreText; // Kéo TextMeshPro "BestScoreText" con của panel vào

    [Header("Buttons")]
    public Button restartButton;
    public Button menuButton;

    void Awake()
    {
        // Thêm listener cho các nút bấm
        restartButton.onClick.AddListener(OnRestart);
        menuButton.onClick.AddListener(OnGoToMenu);

        // Đăng ký lắng nghe sự kiện từ GameManager
        GameManager.OnGameOver += ShowPanel;

        // Ẩn panel khi bắt đầu
        panelRoot.SetActive(false);
    }

    void OnDestroy()
    {
        // Hủy đăng ký sự kiện khi đối tượng bị hủy
        GameManager.OnGameOver -= ShowPanel;
    }

    private void ShowPanel()
    {
        // Lấy điểm số từ ScoreMgr
        int currentScore = ScoreMgr.Instance.GetScore();

        // Cập nhật Leaderboard
        LeaderboardMgr.Instance.AddScore("Player", currentScore); // "Player" có thể thay bằng tên người chơi

        // Lấy điểm cao nhất
        int bestScore = LeaderboardMgr.Instance.GetLeaderboard().scores[0].score;

        // Hiển thị điểm
        scoreText.text = currentScore.ToString();
        bestScoreText.text = bestScore.ToString();

        // Hiển thị panel
        panelRoot.SetActive(true);
    }

    private void OnRestart()
    {
        // Ẩn panel (để tránh nhấp đúp)
        panelRoot.SetActive(false);
        
        // Yêu cầu GameManager tải lại map hiện tại
        // Đây là cách tải lại map mà vẫn giữ được data-driven
        if (GameManager.Instance != null && GameManager.Instance.CurrentMapData != null)
        {
            GameManager.Instance.SelectMap(GameManager.Instance.CurrentMapData);
        }
        else
        {
            // Phòng trường hợp chạy trực tiếp GameScene mà không có GameManager
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnGoToMenu()
    {
        // Ẩn panel
        panelRoot.SetActive(false);
        
        // Yêu cầu GameManager tải StartScene
        GameManager.Instance.LoadStartScene();
    }
}