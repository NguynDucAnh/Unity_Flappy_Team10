using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameMain : MonoBehaviour
{
    public GameObject bird;
    public GameObject readyPic;
    public GameObject tipPic;
    public ScoreMgr scoreMgr; // Đảm bảo kéo script ScoreMgr vào đây
    public PipeSpawner pipeSpawner; // Đảm bảo kéo PipeSpawner vào đây

    private bool gameStarted = false;

    void Start()
    {
        Time.timeScale = 1; // Đảm bảo game chạy
        if(scoreMgr != null) scoreMgr.SetScore(0);
    }

    void Update()
    {
        if (!gameStarted && Input.GetMouseButtonDown(0))
        {
            gameStarted = true;
            StartGame();
        }
    }

    private void StartGame()
    {
        if(bird != null)
        {
            BirdControl control = bird.GetComponent<BirdControl>();
            if(control != null)
            {
                control.inGame = true;
                control.JumpUp();
            }
        }

        // Ẩn hướng dẫn
        if(readyPic != null) readyPic.SetActive(false);
        if(tipPic != null) tipPic.SetActive(false);

        if(pipeSpawner != null) pipeSpawner.StartSpawning();
    }

    public void GameOver()
    {
        // Dừng sinh ống
        if(pipeSpawner != null) pipeSpawner.GameOver();

        // Lưu điểm
        if(scoreMgr != null)
        {
            int finalScore = scoreMgr.GetScore();
            if(LeaderboardMgr.Instance != null)
                LeaderboardMgr.Instance.AddScore(finalScore);
            
            Debug.Log("Game Over! Score: " + finalScore);
        }
        
        // Tìm và hiện bảng Game Over (nếu có trong scene)
        GameOverPanel panel = FindObjectOfType<GameOverPanel>();
        if(panel != null) panel.ShowGameOver(scoreMgr.GetScore());
    }
}