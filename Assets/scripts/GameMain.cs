using UnityEngine;
using DG.Tweening;

public class GameMain : MonoBehaviour
{
    public GameObject bird;
    public GameObject readyPic;
    public GameObject tipPic;
    public GameObject scoreMgr;
    public GameObject pipeSpawner;
    public GameObject leaderboardCanvas; // 👉 Kéo LeaderboardCanvas vào đây trong Inspector
    public LeaderboardManager leaderboardManager;

    private bool gameStarted = false;

    void Start()
    {
        // 👉 Khi mới vào game, hiển thị bảng rank
        if (leaderboardCanvas != null)
        {
            leaderboardCanvas.SetActive(true);
            Debug.Log("LeaderboardCanvas is visible at start!");
        }
    }

    void Update()
    {
        // 👉 Khi nhấn chuột lần đầu
        if (!gameStarted && Input.GetButtonDown("Fire1"))
        {
            gameStarted = true;
            StartGame();
        }
    }

    private void StartGame()
    {
        BirdControl control = bird.GetComponent<BirdControl>();
        control.inGame = true;
        control.JumpUp();

        // Ẩn hình "Ready" và "Tap"
        readyPic.GetComponent<SpriteRenderer>().material.DOFade(0f, 0.2f);
        tipPic.GetComponent<SpriteRenderer>().material.DOFade(0f, 0.2f);

        // Reset điểm và bắt đầu spawn ống
        scoreMgr.GetComponent<ScoreMgr>().SetScore(0);
        pipeSpawner.GetComponent<PipeSpawner>().StartSpawning();

        // 👉 Ẩn bảng rank khi bắt đầu chơi
        if (leaderboardCanvas != null)
        {
            leaderboardCanvas.SetActive(false);
            Debug.Log("LeaderboardCanvas hidden when game starts.");
        }
    }

    // 👉 Hàm này được gọi khi chim chết
    public void OnGameOver()
    {
        // Hiện lại bảng rank khi chết
        if (leaderboardCanvas != null)
        {
            leaderboardCanvas.SetActive(true);
            Debug.Log("LeaderboardCanvas shown after game over.");



            // 👉 Lưu và hiển thị điểm mới
            var scoreMgrComp = scoreMgr.GetComponent<ScoreMgr>();
            int finalScore = scoreMgrComp.GetScore(); // lấy điểm hiện tại
            leaderboardManager.AddScore("Player", finalScore); // thêm vào rank
        }

    }
}