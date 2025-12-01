using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    public GameObject panelRoot;
    public Text scoreText;
    public Text bestText;

    void Start()
    {
        if(panelRoot != null) panelRoot.SetActive(false);
    }

    public void ShowGameOver(int score)
    {
        if(panelRoot != null) panelRoot.SetActive(true);
        
        if(scoreText != null) scoreText.text = score.ToString();
        
        // Lấy best score (đơn giản hóa để chạy được ngay)
        int best = PlayerPrefs.GetInt("BestScore", 0);
        if(score > best)
        {
            best = score;
            PlayerPrefs.SetInt("BestScore", best);
        }
        if(bestText != null) bestText.text = best.ToString();
    }

    public void OnRestartClick()
    {
        // Load lại chính Scene hiện tại
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMenuClick()
    {
        SceneManager.LoadScene("StartScene");
    }
}