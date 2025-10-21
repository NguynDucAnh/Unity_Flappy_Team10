using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameOverPanel : MonoBehaviour
{
    public GameObject panel;
    public Text currentScoreText;
    public Text bestScoreText;
    public Button playButton;
    public Button homeButton;
    
    private const string BEST_SCORE_KEY = "BestScore";
    
    void Start()
    {
        // Ẩn panel khi bắt đầu
        if (panel != null)
        {
            panel.SetActive(false);
        }
        
        // Gán sự kiện cho các button
        if (playButton != null)
        {
            playButton.onClick.AddListener(OnPlayButtonClick);
        }
        
        if (homeButton != null)
        {
            homeButton.onClick.AddListener(OnHomeButtonClick);
        }
    }
    
    public void ShowGameOver(int currentScore)
    {
        // Lấy điểm cao nhất
        int bestScore = PlayerPrefs.GetInt(BEST_SCORE_KEY, 0);
        
        // Cập nhật điểm cao nhất nếu điểm hiện tại cao hơn
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt(BEST_SCORE_KEY, bestScore);
            PlayerPrefs.Save();
        }
        
        // Hiển thị điểm số
        if (currentScoreText != null)
        {
            currentScoreText.text = currentScore.ToString();
        }
        
        if (bestScoreText != null)
        {
            bestScoreText.text = bestScore.ToString();
        }
        
        // Hiển thị panel với animation
        if (panel != null)
        {
            panel.SetActive(true);
            panel.transform.localScale = Vector3.zero;
            panel.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
        }
    }
    
    private void OnPlayButtonClick()
    {
        // Reload lại GameScene
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }
    
    private void OnHomeButtonClick()
    {
        // Về StartScene
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
    }
}